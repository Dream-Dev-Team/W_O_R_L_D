﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WorldToPlayerMessenger worldToPlayer;
    public SpriteRenderer jojo;
    private Transform tf;
    private Rigidbody2D rb2D;
    private BoxCollider2D coll2D;

    [Range(1, 10)]
    public float speed = 5f;
    [Range(1, 15)]
    public float jumpForce;
    public bool isGrounded = false;
    public float raycastLength = 1.1f;
    public LayerMask groundLayers;


    // Start is called before the first frame update
    void Start()
    {
        tf = GetComponent<Transform>();
        rb2D = GetComponent<Rigidbody2D>();
        coll2D = GetComponent<BoxCollider2D>();

        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        if (worldToPlayer.GenerationStatus() == 1 && rb2D.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            rb2D.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            rb2D.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        Mine();
        isGrounded = GroundCheck();
    }
    private void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") < 0)
            jojo.flipX = false;
        else
            jojo.flipX = true;

        tf.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f, 0f);

        if (Input.GetButton("Jump"))
            if (isGrounded)
                Jump();
    }

    public bool GroundCheck()
    {
        RaycastHit2D hitL = Physics2D.Raycast(coll2D.bounds.center - new Vector3(0.5f, 0f, 0f), Vector2.down, coll2D.bounds.extents.y + 0.1f, groundLayers);
        RaycastHit2D hitR = Physics2D.Raycast(coll2D.bounds.center + new Vector3(0.5f, 0f, 0f), Vector2.down, coll2D.bounds.extents.y + 0.1f, groundLayers);

        if(hitL.collider != null || hitR.collider != null)
        {
            Debug.DrawRay(coll2D.bounds.center - new Vector3(0.5f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f) , Color.red);
            Debug.DrawRay(coll2D.bounds.center + new Vector3(0.5f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f), Color.red);
            return true;
        }
        Debug.DrawRay(coll2D.bounds.center - new Vector3(0.5f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f), Color.green);
        Debug.DrawRay(coll2D.bounds.center + new Vector3(0.5f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f), Color.green);
        return false;
    }

    private void Jump()
    {
        rb2D.AddForce(Vector2.up  * jumpForce * 500, ForceMode2D.Force);
    }

    private void Mine()
    {
        if (Input.GetMouseButtonDown(0) && !WindowOpener.isWindowOpen)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0f, groundLayers);
            if (hit.collider != null && hit.transform.GetComponent<Block>() != null)
            {
                Debug.Log("MINING : " + hit.transform.name);
                hit.transform.GetComponent<Block>().DamageBlock(10, 1);
            }
        }
    }
}