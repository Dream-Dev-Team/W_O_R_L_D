using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDetailsLoader : MonoBehaviour
{
    public float chanceForTree = 2f;
    public GameObject Tree;

    public void LoadDetails(GameObject chunk)
    {
        for(int i = 0; i < chunk.transform.childCount; i++)
        {
            if (chunk.transform.GetChild(i).CompareTag("Grass"))
            {
                 if(Random.Range(0, 100) <= chanceForTree)
                 {
                     GameObject clone = Instantiate(Tree, this.transform);
                     clone.transform.position = chunk.transform.GetChild(i).transform.position + new Vector3(0f, 1f, 0f);
                 }
            }
        }
    }
}
