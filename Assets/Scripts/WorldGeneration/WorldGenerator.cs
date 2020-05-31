using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldGenerator : MonoBehaviour
{
    private ChunkActivator chunkActivator;

    public GameObject chunk;
    public int numChunks;
    public int seed;
    public int chunkLength;
    public float timePerChunkGen = 0.5f;
    private int lastX;
    public Slider loadingSlider;
    public Text loadingText;

    void Start()
    {
        chunkActivator = GetComponent<ChunkActivator>();

        loadingSlider.maxValue = numChunks;
        loadingSlider.value = 0;
        seed = Random.Range(-100000, 100000);
        chunkLength = chunk.GetComponent<ChunkGenerator>().chunkLength;

        chunkActivator.ReserveSpace(numChunks);

        StartCoroutine(GenerateTimer());
    }

    private void GenerateWorld()
    {
        lastX = -chunkLength;
        for(int i = 0; i < numChunks; i++)
        {
            GameObject newChunk = Instantiate(chunk, new Vector3(lastX + chunkLength, 0f), Quaternion.identity) as GameObject;
            newChunk.GetComponent<ChunkGenerator>().seed = seed;
            lastX += chunkLength;
        }
    }

    private IEnumerator GenerateTimer()
    {
        lastX = -chunkLength;
        for (int i = 0; i < numChunks; i++)
        {
            GameObject newChunk = Instantiate(chunk, new Vector3(lastX + chunkLength, 0f), Quaternion.identity) as GameObject;
            newChunk.GetComponent<ChunkGenerator>().seed = seed;
            newChunk.SetActive(true);
            newChunk.transform.parent = this.gameObject.transform;
            lastX += chunkLength;
            loadingSlider.value += 1;
            loadingText.text = ((loadingSlider.value / numChunks) * 100).ToString() + " %";
            yield return new WaitForSeconds(timePerChunkGen);
            chunkActivator.GetChunkInArray(ref newChunk);
            newChunk.SetActive(false);
        }
        chunkActivator.ActivateChunks();
    }
}
