using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class ChunkRenderer : MonoBehaviour
{
    public const int Chunk_Width = 10;
    public const int Chunk_Height = 128;
    public const float Block_Scale = 1.5f;

    public ChunkData ChunkData;

    private List<Vector3> verticies = new();
    private List<int> triangles = new();

    void Start()
    {
        var chunkMesh = new Mesh();

        // ChunkData.Blocks = TerrainGenerator.GenerateTerrain((int) transform.position.x, (int) transform.position.z);

        // ChunkData.Blocks[0,0,0] = BlockType.Grass;
        // ChunkData.Blocks[0,0,1] = BlockType.Grass;

        for(int y=0; y < Chunk_Height; y++)
        {
            for(int x=0; x < Chunk_Width; x++)
            {
                for(int z=0; z < Chunk_Width; z++)
                {
                    GenerateBlock(x,y,z);
                }
            }
        }

        chunkMesh.vertices = verticies.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        chunkMesh.Optimize();

        chunkMesh.RecalculateNormals();
        chunkMesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = chunkMesh;
        GetComponent<MeshCollider>().sharedMesh = chunkMesh;
    }

    private void GenerateBlock(int x, int y, int z)
    {
        var blockPosition = new Vector3Int(x,y,z);

        if(GetBlockAtPosition(blockPosition) == 0) return;

        if(GetBlockAtPosition(blockPosition + Vector3Int.right) == 0)GenerateRightSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.left) == 0)GenerateLeftSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.forward) == 0)GenerateFrontSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.back) == 0)GenerateBackSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.up) == 0)GenerateTopSide(blockPosition);
        if(GetBlockAtPosition(blockPosition + Vector3Int.down) == 0)GenerateBottomSide(blockPosition);
    }

    private BlockType GetBlockAtPosition(Vector3Int blockPosition)
    {
        if(blockPosition.x >= 0 && blockPosition.x < Chunk_Width && 
           blockPosition.y >= 0 && blockPosition.y < Chunk_Height && 
           blockPosition.z >= 0 && blockPosition.z < Chunk_Width)
        {
            return ChunkData.Blocks[blockPosition.x, blockPosition.y, blockPosition.z];
        }

        else
        {
            return BlockType.Air;
        }

    }

    private void GenerateSide(Vector3[] verticies)
    {
        foreach(var vert in verticies)
        {
            this.verticies.Add(vert * Block_Scale); 
        }

        AddLastVerticiesSquare();
    }

    private void GenerateRightSide(Vector3Int blockPosition)
    {
        var verticiesSide = new Vector3[]
        {
            new Vector3(0,0,0) + blockPosition,
            new Vector3(0,0,1) + blockPosition,
            new Vector3(0,1,0) + blockPosition,
            new Vector3(0,1,1) + blockPosition
        };

        GenerateSide(verticiesSide);
    }

    private void GenerateLeftSide(Vector3Int blockPosition)
    {
        var verticiesSide = new Vector3[]
        {
            new Vector3(1,0,0) + blockPosition,
            new Vector3(1,1,0) + blockPosition,
            new Vector3(1,0,1) + blockPosition,
            new Vector3(1,1,1) + blockPosition
        };

        GenerateSide(verticiesSide);
    }

    private void GenerateFrontSide(Vector3Int blockPosition)
    {
        var verticiesSide = new Vector3[]
        {
            new Vector3(0,0,1) + blockPosition,
            new Vector3(1,0,1) + blockPosition,
            new Vector3(0,1,1) + blockPosition,
            new Vector3(1,1,1) + blockPosition
        };

        GenerateSide(verticiesSide);
    }

    private void GenerateBackSide(Vector3Int blockPosition)
    {
        var verticiesSide = new Vector3[]
        {
            new Vector3(0,0,0) + blockPosition,
            new Vector3(0,1,0) + blockPosition,
            new Vector3(1,0,0) + blockPosition,
            new Vector3(1,1,0) + blockPosition
        };

        GenerateSide(verticiesSide);
    }

    private void GenerateTopSide(Vector3Int blockPosition)
    {
        var verticiesSide = new Vector3[]
        {
            new Vector3(0,1,0) + blockPosition,
            new Vector3(0,1,1) + blockPosition,
            new Vector3(1,1,0) + blockPosition,
            new Vector3(1,1,1) + blockPosition
        };

        GenerateSide(verticiesSide);
    }

    private void GenerateBottomSide(Vector3Int blockPosition)
    {
        var verticiesSide = new Vector3[]
        {
            new Vector3(0,0,0) + blockPosition,
            new Vector3(1,0,0) + blockPosition,
            new Vector3(0,0,1) + blockPosition,
            new Vector3(1,0,1) + blockPosition
        };

        GenerateSide(verticiesSide);
    }

    private void AddLastVerticiesSquare()
    {
        triangles.Add(verticies.Count - 4);
        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 2);

        triangles.Add(verticies.Count - 3);
        triangles.Add(verticies.Count - 1);
        triangles.Add(verticies.Count - 2);
    }
}
