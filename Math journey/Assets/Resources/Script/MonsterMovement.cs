using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Slime_Run : MonoBehaviour
{

    public Transform[] patrolPoint;
    public float moveSpeed;
    public int patrolDestination;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;
    public float aggroDistance; // Max Chasing distance


    private bool isFacingRight;
    private Rigidbody2D rb2D;
    public float maxYDifference = 0.5f;

    public Transform groundCheck;
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;

    private PlayerMovement playerMovement;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // get therigidbody2D to use
        animator = GetComponent<Animator>(); // get the Animator to use
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        //float scaleX = Mathf.Abs(transform.localScale.x); // ค่าบวกเสมอ

        if (patrolPoint[0].position.x < transform.position.x)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1); // หันซ้าย (sprite หันซ้ายอยู่แล้ว)
        else
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1); // หันขวา

        patrolDestination = 0;
    }

    // Update is called once per frame
    void Update()
    {
        


        if (isChasing) // if isChasing == True
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
            float yDifference = Mathf.Abs(transform.position.y - playerTransform.position.y);

            bool isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);



            if (distanceToPlayer > aggroDistance || yDifference > maxYDifference || !isGroundAhead)
            {

                if (yDifference > maxYDifference && playerMovement.grounded == false)
                {

                }
                else
                {

                    Vector3 currentScale = transform.localScale;
                    currentScale.x *= -1;
                    transform.localScale = currentScale;
                    
                    isChasing = false;
                    patrolDestination = GetClosestPatrol();
                    


                    return;
                }
            }

            

            if (transform.position.x > playerTransform.position.x) // monster is on our right
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }

            else if (transform.position.x < playerTransform.position.x) // monster is on our left
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }

        }

        else if(!isChasing)
        {
            float yDifference = Mathf.Abs(transform.position.y - playerTransform.position.y);
            float xDistance = Vector2.Distance(transform.position, playerTransform.position);

            if (xDistance < chaseDistance && yDifference < maxYDifference)
            {
                if(playerMovement.grounded || !playerMovement.grounded)
                    isChasing = true;
            }

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, moveSpeed * Time.deltaTime);



                if (Vector2.Distance(transform.position, patrolPoint[0].position) < .2f)
                {
                    patrolDestination = 1;

                    // ตั้งการหันหน้าตามทิศทางใหม่
                    if (patrolPoint[patrolDestination].position.x > transform.position.x)
                        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                    else
                        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, moveSpeed * Time.deltaTime);

                
                if (Vector2.Distance(transform.position, patrolPoint[1].position) < .2f)
                {
                    
                    patrolDestination = 0;
                    

                    // ตั้งการหันหน้าตามทิศทางใหม่
                    if (patrolPoint[patrolDestination].position.x > transform.position.x)
                        transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                    else
                        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
                }
            }


        }

    }

    int GetClosestPatrol()
    {
        float distanceTo0 = Vector2.Distance(transform.position, patrolPoint[0].position);
        float distanceTo1 = Vector2.Distance(transform.position, patrolPoint[1].position);

        return (distanceTo0 < distanceTo1) ? 0 : 1;

    }



}

