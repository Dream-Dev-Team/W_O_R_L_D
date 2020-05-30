using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int blockID;         
    public int strength = 0;    //Strength, for pickaxepower
    public int health = 0;      //Health of Block

    public int DamageBlock(int pickaxePower, int damage)
    {
        if (pickaxePower >= strength)
            health -= damage;

        if (health <= 0)
        {
            Destroy(this.gameObject, 0.01f);        //fishy code, may throw errors sometimes
            return blockID;
        }
        return -1;
    }
}
