using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorld : MonoBehaviour
{
    public Dictionary<Vector2Int, ChunkData> ChunkDatas = new Dictionary<Vector2Int, ChunkData>(); 

    void Start()
    {
        for(int x = 0; x < 10; x++)
        {
            for(int z = 0; z < 10; z++)
            {
                var chunkData = new ChunkData();
                float posX = x * ChunkRenderer.Chunk_Width * ChunkRenderer.Block_Scale;
                float posZ = z * ChunkRenderer.Chunk_Width * ChunkRenderer.Block_Scale;
                
                chunkData.Blocks = TerrainGenerator.GenerateTerrain((int) posX, (int) posZ); // TODO: GenerateTerrain is FLOAT CORRECT THIS
                ChunkDatas.Add(new Vector2Int(x,z), chunkData);
            }
        }
    }
}
