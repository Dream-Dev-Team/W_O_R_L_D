using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public Transform[,] slots;
    int numLine;
    int numColumn;
    public GameObject slotHolder;
    public Sprite normalSlot;
    public Sprite highlSlot;
    public Transform selectedObj;
    int indexObj1 = -1;
    public Transform selectedObj2;
    int indexObj2 = -1;

    void Start()
    {
        numLine = slotHolder.transform.childCount / 8;
        numColumn = slotHolder.transform.childCount / 11;

        RefreshInventory();
    }

    public void SelectOnClick(int index)
    {
        if (selectedObj == null && indexObj1 == -1)
        {
            selectedObj = slotHolder.transform.GetChild(index);
            indexObj1 = index;

            selectedObj.GetComponent<Image>().sprite = highlSlot;
        }
        else if (selectedObj2 == null && indexObj2 == -1)
        {
            selectedObj2 = slotHolder.transform.GetChild(index);
            indexObj2 = index;

            selectedObj2.GetComponent<Image>().sprite = highlSlot;
        }
        else if (selectedObj != null && selectedObj2 != null && indexObj1 != -1 && indexObj2 != -1)
        {
            selectedObj.SetSiblingIndex(indexObj2);
            Debug.Log("SelectedObj1: " + selectedObj.name);
            selectedObj.name = "Slot (" + indexObj2 + ")";

            selectedObj2.SetSiblingIndex(indexObj1);
            Debug.Log("SelectedObj2: " + selectedObj2.name);
            selectedObj2.name = "Slot (" + indexObj1 + ")";

            indexObj1 = -1;
            indexObj2 = -1;

            selectedObj.GetComponent<Image>().sprite = normalSlot;
            selectedObj2.GetComponent<Image>().sprite = normalSlot;

            RefreshInventory();
        }
    }  

    public void RefreshInventory()
    {
        Transform[,] slots = new Transform[numColumn, numLine];

        int i = 0;
        for (int y = 0; y < numColumn; y++)
            for (int x = 0; x < numLine; x++)
            {
                int fixI = i;
                slots[y, x] = slotHolder.transform.GetChild(i).GetComponent<Transform>();
                slots[y, x].GetComponent<Button>().onClick.RemoveAllListeners();
                slots[y, x].GetComponent<Button>().onClick.AddListener(delegate { SelectOnClick(fixI); });
                i++;
            }

        selectedObj = null;
        selectedObj2 = null;
    }

    public void AddToInventory(int pickedIndex)
    {
        for (int y = 0; y < numColumn; y++)
            for (int x = 0; x < numLine; x++)
            {
                if (slots[y, x].GetComponent<Slot>().IsBusy())
                    if (slots[y, x].GetChild(0).GetComponent<Item>().index == pickedIndex)
                        if(!slots[y, x].GetComponent<Slot>().IsFull())
                            slots[y, x].GetComponent<Slot>().StackUp();
            }

        //if not already in inventory, find first slot to place it in
    }
}
