using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int itemCounter = 0;
    public bool bussy;
    public bool full;



    private void Start()
    {
        IsFull();
    }

    public bool IsBusy()
    {
        if (this.transform.childCount == 1)
            bussy = true;
        else
            bussy = false;

        return bussy;
    }

    public bool IsFull()
    {
        if (IsBusy())
        {
            if (this.transform.GetComponentInChildren<Item>().numStack <= itemCounter)
                full = true;
            else
                full = false;
        }
        else
            full = false;

        return full;
    }

    public void StackUp()
    {
        itemCounter++;
        IsFull();
    }
}
