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

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>(); // get therigidbody2D to use
        animator = GetComponent<Animator>(); // get the Animator to use
    }

    // Update is called once per frame
    void Update()
    {

        if (isChasing) // if isChasing == True
        {
            if(Vector2.Distance(transform.position, playerTransform.position) > aggroDistance) // If player distance higher than Max Chasing distance
            {
                transform.localScale = new Vector3(-1,1,1);
                isChasing = false;

            }

            if (transform.position.x > playerTransform.position.x) // monster is on our right
            {
                transform.localScale = new Vector3(1,1,1);
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            }

            if (transform.position.x < playerTransform.position.x) // monster is on our left
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            }

        }

        else
        {
            if(Vector2.Distance(transform.position,playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }

            

            

            if (patrolDestination == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[0].position, moveSpeed * Time.deltaTime);

               

                if (Vector2.Distance(transform.position, patrolPoint[0].position) < .2f)
                {
                    transform.localScale = new Vector3(1, 1, 1);
                    patrolDestination = 1;
                }
            }

            if (patrolDestination == 1)
            {
                transform.position = Vector2.MoveTowards(transform.position, patrolPoint[1].position, moveSpeed * Time.deltaTime);
             

                if (Vector2.Distance(transform.position, patrolPoint[1].position) < .2f)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                    patrolDestination = 0;
                }
            }

            
        }
        
        }












    }



  


