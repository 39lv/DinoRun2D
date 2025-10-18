using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dinoController : MonoBehaviour
{
    public float jumpForce;
    public bool isGround;
    public bool isDown;

    private Animator anime;
    private Rigidbody2D rb;

    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
        anime.SetBool("isGround", true);
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);

        if (Input.GetKeyDown(KeyCode.Space) && isGround.Equals(true))
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isGround.Equals(true))
        {
            anime.SetBool("isDown", true);
        }
        

        if (Input.GetKeyUp(KeyCode.Space) && isGround.Equals(true))
        {

            anime.SetBool("isGround", false);
        }
        anime.SetBool("isGround", isGround);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, 0.2f);
    }

}
