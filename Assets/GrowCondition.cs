using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(TreeGrower))]
public class GrowCondition : MonoBehaviour
{
    public bool isSapling;
    public bool destroyIfNoPlace;
    public bool isAllowedToGrow = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Tree"))
        {
            if (destroyIfNoPlace && collision.GetComponent<GrowCondition>().isSapling == false)
            {
                Destroy(this.gameObject);
                Debug.Log("Destroyed Tree");
            }
            else
                isAllowedToGrow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)              //May not work as attempted
    {
        if (collision.transform.gameObject.CompareTag("Tree"))
            isAllowedToGrow = true;
    }
}
