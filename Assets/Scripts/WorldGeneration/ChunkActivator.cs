using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkActivator : MonoBehaviour
{
    public SaveWorldData saveWorld;
    public DynamicChunkLoader dynamicChunkLoader;

    public int totalNumBlocks = 0;
    [Tooltip("Activision time bewtween two  chunks")]
    public float actTime = 0.5f;
    public GameObject[] chunks;
    public int index = 0;
    public bool activatedFirstChunk = false;
    public bool activatedAllChunks = false;

    public void ActivateChunks()
    {
        StartCoroutine(Timer());
    }

    public void ReserveSpace(int numChunks)
    {
        chunks = new GameObject[numChunks];
    }

    public void GetChunkInArray(ref GameObject chunk)
    {
        chunks[index] = chunk;
        index++;
    }


    private IEnumerator Timer()
    {
        foreach(GameObject chunk in chunks)
        {
            chunk.SetActive(true);

            totalNumBlocks += chunk.transform.GetComponent<ChunkGenerator>().countBlocks;

            yield return new WaitForSeconds(actTime);
            chunk.SetActive(false);
        }
        activatedAllChunks = true;

        System.GC.Collect();
        //Start first activation manuel
        dynamicChunkLoader.ActivateFirstChunks();
        activatedFirstChunk = true;
        //saveWorld.GetAllBlockInfos();
    }
}
