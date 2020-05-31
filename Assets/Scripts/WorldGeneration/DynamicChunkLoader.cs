using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicChunkLoader : MonoBehaviour             //Still in need of some love :( 
{
    private ChunkActivator chunkActivator;
    private WorldGenerator worldGenerator;
    public Transform player;
    public Text currChunkText;
    [Tooltip("Check every x sec")]
    public float checkRate = 0.5f;
    private float currTime = 0;

    [Tooltip("Active Chunks at the same time to x+ (left)")]
    public int numChunksActiveLeft = 5;
    [Tooltip("Active Chunks at the same time to x- (right)")]
    public int numChunksActiveRight = 5;
    public int currChunkIndex = 0;
    public int lastCurrChunkIndex = 0;
    public int[] activeChunksIds;
    public Transform world;
    int numChunks;

    private void Awake()
    {
        chunkActivator = GetComponent<ChunkActivator>();
        worldGenerator = GetComponent<WorldGenerator>();

        activeChunksIds = new int[numChunksActiveLeft + numChunksActiveRight + 1];
    }

    void Start()
    {
        currChunkIndex = (int)player.position.x / 32;
        numChunks = chunkActivator.chunks.Length;
        //InvokeRepeating("CheckCurrChunk", 0, checkRate);
    }

    private void Update()
    {
        currTime  -= Time.deltaTime;
        if(currTime < 0)
        {
            CheckCurrChunk();
            currTime = checkRate;
        }
    }

    private void CheckCurrChunk()
    { 
        lastCurrChunkIndex = currChunkIndex;
        currChunkIndex = (int) player.position.x / worldGenerator.chunkLength;
        currChunkText.text = "Chunk: " + currChunkIndex.ToString();

        if (lastCurrChunkIndex != currChunkIndex)
            RefreshChunkActivations();
    }

    public void RefreshChunkActivations()
    {
        StartCoroutine(SeperateExecutions());
    }
    IEnumerator SeperateExecutions()
    {
        ActivateChunk();
        yield return new WaitForSeconds(0.1f);
        DeactivateChunk();
    }

    private void ActivateChunk()
    {
        //if index of chunk is greater then -1 and lower then bounds of chunks[], then activate it, if activeIndexes matches chunk indexes
        int startval = currChunkIndex - numChunksActiveRight;

        for (int i = 0; i < activeChunksIds.Length; i++)
        {
            activeChunksIds[i] = startval + i;

            if (activeChunksIds[i] >= 0 && activeChunksIds[i] < chunkActivator.chunks.Length)
                world.transform.GetChild(activeChunksIds[i]).gameObject.SetActive(true);    //chunkActivator.chunks[activeChunksIds[i]].SetActive(true);
        }
    }
    private void DeactivateChunk()
    {
        int deactivateLeftIndex = currChunkIndex - (numChunksActiveLeft + 1);
        int deactivateRightIndex = currChunkIndex + (numChunksActiveRight + 1);

        if (deactivateLeftIndex < numChunks && deactivateLeftIndex >= 0)
            world.transform.GetChild(deactivateLeftIndex).gameObject.SetActive(false);      //chunkActivator.chunks[deactivateLeftIndex].SetActive(false);

        if (deactivateRightIndex < numChunks && deactivateRightIndex >= 0)
            world.transform.GetChild(deactivateRightIndex).gameObject.SetActive(false);

        Debug.Log("Deactivated Chunks: " + deactivateLeftIndex + ", " + deactivateRightIndex);
    }

    public void ActivateFirstChunks()
    {
        ActivateChunk();
    }
}
