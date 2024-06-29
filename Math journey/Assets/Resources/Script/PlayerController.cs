using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1f; // Walk / Run Speed
    public float jumpSpeed = 9f; // Jump Speed
    public float maxSpeed = 10f; // Max Walk / Run Speed
    public float jumpPower = 20f; // Jump Power
    public bool grounded; // Check if on the ground
    public float jumpRate = 1f;
    public float nextJumpPress = 0.0f;
    public float fireRate = 0.2f;
    public float nextFireRate = 0.0f;
    private Rigidbody2D rb2D;
    private Physics2D physic2D;
    Animator animator;
    public int health = 100;

    float inputHorizontal;
    float inputVertical;

    private bool isFacingRight;

    public Transform groundCheck;
    public LayerMask groundLayer;
    






    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // get therigidbody2D to use
        animator = GetComponent<Animator>(); // get the Animator to use
    }

    // Update is called once per frame
    void Update()
    {




        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        animator.SetBool("Grounded", true); // Set the Grounded variable in animator to be TRUE
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); // Set the Speed variable in animator to be the Input Horizontal

        // Walk system
        if (inputHorizontal != 0)
        {
            rb2D.velocity = new Vector2(inputHorizontal, rb2D.velocity.y);
        }

        if (inputHorizontal < -0f && !isFacingRight) // Flip to the right side
        {
            Flip();

        }
        
        if (inputHorizontal > 0f && isFacingRight) // Flip to the left side
        {

            Flip();

        }

        //----------------------------------------------------------------------------------------------
        // Jump system
        if (Input.GetKeyDown(KeyCode.Space)&& isGrounded())
        {
            animator.SetBool("Jump", true);
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpPower);
        }
        else
        {
            animator.SetBool("Jump", false);
        }

        bool isGrounded()
        {
            return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.0093f, 0.18f), CapsuleDirection2D.Vertical, 0, groundLayer);
        }
    
        //-------------------------------

    }


    void Flip() // Flip Method to use
    {

        Vector2 currentScale = transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;
        isFacingRight = !isFacingRight;
    }




    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                grounded = false;
            }
        }
    }
   
