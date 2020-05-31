using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicChunkLoader : MonoBehaviour
{
    private ChunkActivator chunkActivator;
    private WorldGenerator worldGenerator;
    public Transform player;
    public Text currChunkText;
    [Tooltip("Check every x sec")]
    public float checkRate;
    [Tooltip("Active Chunks at the same time to x+ (left)")]
    public int numChunksActiveLeft = 2;
    [Tooltip("Active Chunks at the same time to x- (right)")]
    public int numChunksActiveRight = 2;
    public int currChunkIndex = 0;
    public int lastCurrChunkIndex = 0;
    public int[] activeChunksIds;
    public int[] deactiveChunksIds;

    private void Awake()
    {
        chunkActivator = GetComponent<ChunkActivator>();
        worldGenerator = GetComponent<WorldGenerator>();

        activeChunksIds = new int[numChunksActiveLeft + numChunksActiveRight + 1];
    }

    void Start()
    {
        currChunkIndex = (int)player.position.x / 32;
        InvokeRepeating("CheckCurrChunk", 0, checkRate);
    }

    private void CheckCurrChunk()
    {
        lastCurrChunkIndex = currChunkIndex;
        currChunkIndex = (int) player.position.x / 32;
        currChunkText.text = currChunkIndex.ToString();

        if (lastCurrChunkIndex != currChunkIndex)
            RefreshChunkActivations();
    }

    public void RefreshChunkActivations()
    {
        //if index of chunk is greater then -1 and lower then bounds of chunks[], then activate it, if activeIndexes matches chunk indexes
        int startval = currChunkIndex - numChunksActiveRight;
        for (int i = 0; i < activeChunksIds.Length; i++)
        {
            activeChunksIds[i] = startval + i;


            if (activeChunksIds[i] >= 0 && activeChunksIds[i] < chunkActivator.chunks.Length)
                chunkActivator.chunks[activeChunksIds[i]].SetActive(true);
        }


        int deactivateLeftIndex = currChunkIndex - (numChunksActiveLeft + 1);
        int deactivateRightIndex = currChunkIndex + (numChunksActiveRight + 1);

        if (deactivateLeftIndex < chunkActivator.chunks.Length && deactivateLeftIndex > 0)
            chunkActivator.chunks[deactivateLeftIndex].SetActive(false);
        if(deactivateRightIndex < chunkActivator.chunks.Length && deactivateRightIndex > 0)
            chunkActivator.chunks[deactivateRightIndex].SetActive(false);

        Debug.Log("Deactivated Chunks: " + deactivateLeftIndex + ", " + deactivateRightIndex);
    }

    public void ActivateFirstChunk()
    {
        chunkActivator.chunks[currChunkIndex].SetActive(true);
    }
}
