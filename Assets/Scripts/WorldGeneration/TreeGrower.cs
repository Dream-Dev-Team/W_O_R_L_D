using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TreeGrower : MonoBehaviour
{
    private SpriteRenderer saplingRenderer;
    private GrowCondition growCondition;

    DateTime startTime;
    DateTime endTime;
    TimeSpan timeSpan;
    public double totalTime = 0f;
    public double timeToGrow = 5.5f;
    public GameObject[] treeBlocks;
    public int maxHeight = 20;
    public int minHeight = 12;

    public bool isVisible = false;

    private void Awake()
    {
        startTime = DateTime.Now;
        saplingRenderer = GetComponent<SpriteRenderer>();
        growCondition = GetComponent<GrowCondition>();
    }

    private void Update()
    {
        if(isVisible)
            totalTime += Time.deltaTime / 60f;
        if (growCondition.isAllowedToGrow)
        {
            if (totalTime >= timeToGrow)
                GrowTree();
        }
        
    }


    private void OnBecameVisible()
    {
        isVisible = true;

        endTime = DateTime.Now;
        timeSpan = endTime - startTime;
        totalTime += timeSpan.TotalMinutes;
    }

    private void OnBecameInvisible()
    {
        isVisible = false;

        startTime = DateTime.Now;
    }

    private void GrowTree()
    {
        int height = UnityEngine.Random.Range(minHeight, maxHeight);
        var treeSaplingPos = this.transform.position;

        for (int dy = 0; dy < height; dy++)
        {
            int randomBlock = UnityEngine.Random.Range(0, treeBlocks.Length);
            GameObject clone = Instantiate(treeBlocks[randomBlock]);
            clone.transform.position = treeSaplingPos + new Vector3(0f, dy, 0f);
            clone.transform.parent = this.transform;
        }

        Debug.Log("Tree growed at: " + treeSaplingPos);

        saplingRenderer.enabled = false;
        this.enabled = false;
    }

}
