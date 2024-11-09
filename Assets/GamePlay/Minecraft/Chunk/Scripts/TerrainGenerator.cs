using UnityEngine;

public class TerrainGenerator
{
    public static BlockType[,,] GenerateTerrain(int xOffset, int zOffset)
    {
        var result = new BlockType[ChunkRenderer.Chunk_Width, ChunkRenderer.Chunk_Height, ChunkRenderer.Chunk_Width];

        for(int  x = 0; x < ChunkRenderer.Chunk_Width; x++)
        {
            for(int z = 0; z < ChunkRenderer.Chunk_Width; z++)
            {
                float height = Mathf.PerlinNoise((x+xOffset) * .2f, (z+zOffset) * .2f) * 5 + 10;

                for(int y = 0; y < height; y++)
                {
                    result[x,y,z] = BlockType.Grass;
                }
            }
        }


        return result;
    }

}