using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveWorldData : MonoBehaviour
{
    private ChunkActivator chunkActivator;

    private string path;
    public string dataName;

    public int DIM;
    public int index = 0;
    public byte[] blockType;
    public int[] posX;
    public int[] posY;
    public GameObject[] allObjects;

    public void Awake()
    {
        chunkActivator = GetComponent<ChunkActivator>();

        path = Application.persistentDataPath + "/" + dataName;

        if(File.Exists(path))
            File.Delete(path);

        File.Create(path);
    }

    public void GetAllBlockInfos()
    {
        DIM = chunkActivator.totalNumBlocks;
        blockType = new byte[DIM];
        posX = new int[DIM];
        posY = new int[DIM];

        foreach (GameObject Chunk in chunkActivator.chunks)
        {
            int maxBlocks = Chunk.transform.childCount;
            for(int i = 0; i < maxBlocks; i++)
            {
                blockType[index] = (byte)Chunk.transform.GetChild(i).GetComponent<Block>().blockID;
                posX[index] = (int )Chunk.transform.GetChild(i).GetComponent<Transform>().position.x;
                posY[index] = (int) Chunk.transform.GetChild(i).GetComponent<Transform>().position.y;
                index++;
            }
        }
        //Save();
    }



    public void Save()
    {
        //FileStream fs = new FileStream();
        string sArrayPosX = "";
        string sArrayPosY = "";
        string delimeter = ":";

        int i = 0;
        foreach(int BlockposX in posX)
        {
            if(i == 0)
            {
                sArrayPosX += BlockposX.ToString();
                sArrayPosY += posY[i].ToString();
                continue;
            }
            sArrayPosX += "," + BlockposX.ToString();
            sArrayPosY += "," + posY[i].ToString();
            i++;
        }


        File.OpenWrite(path);
        File.WriteAllBytes(path, blockType);
        File.AppendAllText(path, delimeter);
        File.AppendAllText(path, sArrayPosX);
        File.AppendAllText(path, sArrayPosY);
    }
}
