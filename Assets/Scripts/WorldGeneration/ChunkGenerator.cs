using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkGenerator : MonoBehaviour
{
    [HideInInspector]
    public int seed = 0;
    public int countBlocks = 0;

    public GameObject grass;
    public GameObject grass_B;
    public GameObject dirt;
    public GameObject dirt_B;
    public int min_StoneH = 1;
    public int max_StoneH = 8;
    public GameObject stone;
    public GameObject stone_B;

    public int chunkLength = 16;
    public float smoothness;
    public int heightMult;
    public int additionalHeight;

    public float[] dirtChances;
    private float dirtChance;
    public float[] coalChances;
    private float coalChance;
    public float[] ironChances;
    private float ironChance;
    public float[] goldChances;
    private float goldChance;
    public float[] titanChances;
    private float titanChance;
    public float[] diamondChances;
    private float diamondChance;

    public int layer1;          //Bottom
    public int layer2;
    public int layer3;          //Top


    public GameObject coalOre;
    public GameObject ironOre;
    public GameObject goldOre;
    public GameObject titanOre;
    public GameObject diamondOre;

    private void Start()
    {
        GenerateChunk();
        AddOres();
    }

    private void GenerateChunk()
    {
        for(int x = 0; x < chunkLength; x++)
        {
            int h = Mathf.RoundToInt(Mathf.PerlinNoise(seed, (x + transform.position.x) / smoothness) * heightMult) + additionalHeight;

            int randomH_Stone = Random.Range(min_StoneH, max_StoneH);

            for (int y = 0; y < h; y++)
            {
                GameObject selectedTile;
                if (y < h - randomH_Stone)
                    selectedTile = stone;
                else if (y < h - 1)
                    selectedTile = dirt;
                else
                    selectedTile = grass;

                if (selectedTile != null)
                {
                    GameObject clone = Instantiate(selectedTile, new Vector3(x, y), Quaternion.identity);
                    clone.transform.parent = this.transform;
                    clone.transform.localPosition = new Vector3(x, y);
                    countBlocks++;
                }

            }
            for (int y = 0; y < h; y++)
            {
                GameObject selectedTile;
                if (y < h - randomH_Stone)
                    selectedTile = stone_B;
                else if (y < h - 1)
                    selectedTile = dirt_B;
                else
                    selectedTile = grass_B;

                if (selectedTile != null)
                {
                    GameObject clone = Instantiate(selectedTile, new Vector3(x, y), Quaternion.identity);
                    clone.transform.parent = this.transform;
                    clone.transform.localPosition = new Vector3(x, y);
                    countBlocks++;
                }

            }
        }
    }






    private void AddOres()
    {
        Transform[] tiles = this.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in tiles)
        {
            if (!t.CompareTag("Stone"))
                continue;

            float random = Random.Range(0f, 100f);
            GameObject selectedTile = null;

            if (t.position.y <= layer1)
            {
                dirtChance = dirtChances[2];
                coalChance = coalChances[2];
                ironChance = ironChances[2];
                goldChance = goldChances[2];
                titanChance = titanChances[2];
                diamondChance = diamondChances[2];
            }
            else if (t.position.y <= layer2)
            {
                dirtChance = dirtChances[1];
                coalChance = coalChances[1];
                ironChance = ironChances[1];
                goldChance = goldChances[1];
                titanChance = titanChances[1];
                diamondChance = diamondChances[1];
            }
            else if (t.position.y <= layer3)
            {
                dirtChance = dirtChances[0];
                coalChance = coalChances[0];
                ironChance = ironChances[0];
                goldChance = goldChances[0];
                titanChance = titanChances[0];
                diamondChance = diamondChances[0];
            }
          

            if (random < diamondChance)
                selectedTile = diamondOre;
            else if (random < titanChance)
                selectedTile = titanOre;
            else if (random < goldChance)
                selectedTile = goldOre;
            else if (random < ironChance)
                selectedTile = ironOre;
            else if (random < coalChance)
                selectedTile = coalOre;
            else if (random < dirtChance)
                selectedTile = dirt;

            if (selectedTile != null)
            {
                GameObject newT = Instantiate(selectedTile, t.transform.position, Quaternion.identity) as GameObject;
                newT.transform.parent = this.transform;
                Destroy(t.gameObject);
            }
        }
    }
}
