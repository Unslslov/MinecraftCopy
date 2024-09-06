using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class ChunkRenderer : MonoBehaviour
{
    private const int Chunk_Weight = 10;
    private const int Chunk_Height = 128;

    public int[,,] Blocks = new int[Chunk_Weight, Chunk_Height, Chunk_Weight];

    private List<Vector3> verticies = new();
    private List<int> triangles = new();

    void Start()
    {
        var chunkMesh = new Mesh();

        Blocks[0,0,0] = 1;
        Blocks[0,0,1] = 1;

        for(int y=0; y < Chunk_Height; y++)
        {
            for(int x=0; x < Chunk_Weight; x++)
            {
                for(int z=0; z < Chunk_Weight; z++)
                {
                    GenerateBlock(x,y,z);
                }

            }
        }

        chunkMesh.vertices = verticies.ToArray();
        chunkMesh.triangles = triangles.ToArray();

        chunkMesh.RecalculateNormals();
        chunkMesh.RecalculateBounds();

        GetComponent<MeshFilter>().mesh = chunkMesh;
    }

    private void GenerateBlock(int x, int y, int z)
    {
        if(Blocks[x, y, z] == 0) return;

        var blockSide = new Vector3Int(x,y,z);
        
        GenerateRightSide(blockSide);
        GenerateLeftSide(blockSide);
        GenerateFrontSide(blockSide);
        GenerateBackSide(blockSide);
        GenerateTopSide(blockSide);
        GenerateBottomSide(blockSide);
    }

    private void GenerateRightSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0,0,0) + blockPosition);
        verticies.Add(new Vector3(0,0,1) + blockPosition);
        verticies.Add(new Vector3(0,1,0) + blockPosition);
        verticies.Add(new Vector3(0,1,1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateLeftSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(1,0,0) + blockPosition);
        verticies.Add(new Vector3(1,1,0) + blockPosition);
        verticies.Add(new Vector3(1,0,1) + blockPosition);
        verticies.Add(new Vector3(1,1,1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateFrontSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0,0,1) + blockPosition);
        verticies.Add(new Vector3(1,0,1) + blockPosition);
        verticies.Add(new Vector3(0,1,1) + blockPosition);
        verticies.Add(new Vector3(1,1,1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateBackSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0,0,0) + blockPosition);
        verticies.Add(new Vector3(0,1,0) + blockPosition);
        verticies.Add(new Vector3(1,0,0) + blockPosition);
        verticies.Add(new Vector3(1,1,0) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateTopSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0,1,0) + blockPosition);
        verticies.Add(new Vector3(0,1,1) + blockPosition);
        verticies.Add(new Vector3(1,1,0) + blockPosition);
        verticies.Add(new Vector3(1,1,1) + blockPosition);

        AddLastVerticiesSquare();
    }

    private void GenerateBottomSide(Vector3Int blockPosition)
    {
        verticies.Add(new Vector3(0,0,0) + blockPosition);
        verticies.Add(new Vector3(1,0,0) + blockPosition);
        verticies.Add(new Vector3(0,0,1) + blockPosition);
        verticies.Add(new Vector3(1,0,1) + blockPosition);

        AddLastVerticiesSquare();
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
