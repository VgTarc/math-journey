using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float speed = 1f; // Walk / Run Speed
    public float jumpSpeed = 9f; // Jump Speed
    public float maxSpeed = 10f; // Max Walk / Run Speed
    public float jumpPower = 20f; // Jump Power
    public bool grounded = true; // Check if on the ground
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





    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // get therigidbody2D to use
        animator = GetComponent<Animator>(); // get the Animator to use
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = true;
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        animator.SetBool("Grounded", true); // Set the Grounded variable in animator to be TRUE
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); // Set the Speed variable to be the Input Horizontal
        
        if (inputHorizontal < 0f && !isFacingRight) // Flip to the left side
        {
            Flip();
            
        }
        if (inputHorizontal > 0f && isFacingRight) // Flip to the right side
        {
            Flip();   
        }
        if (inputHorizontal != 0f && inputVertical == 0)
        {
            rb2D.velocity = new Vector2(inputHorizontal * speed, inputVertical);
        }
        if (Input.GetKeyDown(KeyCode.Space)  && Time.time > nextJumpPress && grounded == true)
        {
            nextJumpPress = Time.time * jumpRate;
            inputHorizontal = 0;
            rb2D.AddForce(jumpSpeed * (Vector2.up * jumpPower));

            grounded = false;
            
        }

    }

            
        void Flip() // Flip Method to use
        {
            
            Vector2 currentScale = transform.localScale;
            currentScale.x *= -1;
            gameObject.transform.localScale = currentScale;
            isFacingRight = !isFacingRight;
        }

  

    }
