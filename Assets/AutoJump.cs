using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoJump : MonoBehaviour
{
    public LayerMask layerMask;
    private CircleCollider2D[] colliders;
    private Rigidbody2D rb2D;
    public float jumpForce = 100f;
    public float lastPosX = -100f;
    public float dxMinCheckDist = 0.4f;

    private void Awake()
    {
        colliders = new CircleCollider2D[2];
        colliders = GetComponents<CircleCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            colliders[0].enabled = false;
            colliders[1].enabled = true;
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            colliders[0].enabled = true;
            colliders[1].enabled = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        float dist = 0;
        if (this.transform.position.x > lastPosX)
            dist = Mathf.Abs(this.transform.position.x - lastPosX);
        else
            dist = Mathf.Abs(lastPosX - this.transform.position.x);


        if((layerMask & 1 << collision.gameObject.layer) == 1 << collision.gameObject.layer && Input.GetAxis("Horizontal") != 0 && dist >= dxMinCheckDist)
        {
            //this.transform.position += new Vector3(0f, 1f, 0f);
            rb2D.MovePosition(this.transform.position += new Vector3(0f, 1f, 0f));
            lastPosX = this.transform.position.x;
        }
    }


    private void OnDisable()
    {
        colliders[0].enabled = false;
        colliders[1].enabled = false;
    }
}
