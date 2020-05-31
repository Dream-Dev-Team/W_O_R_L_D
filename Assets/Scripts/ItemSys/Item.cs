using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName;
    public int index;
    public int numStack = 99;
    public Texture2D image;
    [TextArea(5, 20)]
    public string description;

}
