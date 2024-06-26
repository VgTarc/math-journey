using System.Collections;
using System.Collections.Generic;
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





    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // get therigidbody2D to use
        animator = GetComponent<Animator>(); // get the Animator to use
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Grounded",true); // Set the Grounded variable in animator to be TRUE
        animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal"))); // Set the Speed variable to be the Input Horizontal
        if (Input.GetAxis("Horizontal") < -0.1f )// if it's below ZERO
        {
           transform.Translate(Vector2.right * speed * Time.deltaTime);
           transform.eulerAngles = new Vector2(0,180);
        }
        else if(Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
    }
}
