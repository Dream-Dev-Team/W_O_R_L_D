using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldToPlayerMessenger : MonoBehaviour
{
    private ChunkActivator chunkActivator;
    private WorldGenerator worldGenerator;

    private void Start()
    {
        chunkActivator = GetComponent<ChunkActivator>();
        worldGenerator = GetComponent<WorldGenerator>();
    }



    //index: 0 -> still bgenerating; 1 -> first chunk activated; 2 -> all chunks activated;
    public int GenerationStatus()
    {
        if (chunkActivator.activatedFirstChunk)
            return 1;
        if (chunkActivator.activatedAllChunks)
            return 2;
        return 0;
    }
}
