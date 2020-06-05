using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public WorldToPlayerMessenger worldToPlayer;
    public AutoJump autoJump;
    private SpriteRenderer playerRenderer;
    private Transform tf;
    private Rigidbody2D rb2D;
    private BoxCollider2D coll2D;
    //private CapsuleCollider2D coll2D;

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
        playerRenderer = GetComponent<SpriteRenderer>();

        rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void Update()
    {
        if (worldToPlayer.GenerationStatus() == 1 && rb2D.constraints == RigidbodyConstraints2D.FreezeAll)
        {
            rb2D.constraints = RigidbodyConstraints2D.None;
            rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        
        Mine();
        isGrounded = GroundCheck();
    }
    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");

        if (xInput < 0)
            playerRenderer.flipX = true;
        else if(xInput > 0)
            playerRenderer.flipX = false;

        

        tf.Translate(xInput * speed * Time.fixedDeltaTime, 0f, 0f);

        //rb2D.AddForce(new Vector2(Input.GetAxis("Horizontal") * speed * 100 * Time.deltaTime, 0f)); better methode for acceleration effect;

        if (isGrounded)
        {
            autoJump.enabled = true;
            if (Input.GetButton("Jump"))
                Jump();
        }
        else
            autoJump.enabled = false;




    }

    public bool GroundCheck()
    {
        RaycastHit2D hitL = Physics2D.Raycast(coll2D.bounds.center - new Vector3(0.1f, 0f, 0f), Vector2.down, coll2D.bounds.extents.y + 0.1f, groundLayers);
        RaycastHit2D hitR = Physics2D.Raycast(coll2D.bounds.center + new Vector3(0.1f, 0f, 0f), Vector2.down, coll2D.bounds.extents.y + 0.1f, groundLayers);

        if(hitL.collider != null || hitR.collider != null)
        {
            Debug.DrawRay(coll2D.bounds.center - new Vector3(0.1f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f) , Color.red);
            Debug.DrawRay(coll2D.bounds.center + new Vector3(0.1f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f), Color.red);
            return true;
        }
        Debug.DrawRay(coll2D.bounds.center - new Vector3(0.1f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f), Color.green);
        Debug.DrawRay(coll2D.bounds.center + new Vector3(0.1f, 0f, 0f), Vector2.down * (coll2D.bounds.extents.y + 0.01f), Color.green);
        return false;
    }

    private void Jump()
    {
        autoJump.enabled = false;
        rb2D.AddForce(Vector2.up  * jumpForce * 500, ForceMode2D.Force);
    }

    private void Mine()
    {
        if (Input.GetMouseButtonDown(0) && !WindowController.isWindowOpen)
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
