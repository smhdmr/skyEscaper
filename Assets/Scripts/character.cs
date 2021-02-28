using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
 *This script must work on the character.
 * It's managing character movements.
 */



public class character : MonoBehaviour
{
    
    //for access character variables from game manager
    public static character Instance;
    
    
    //character components
    public Rigidbody2D rb;                              //character rigidbody2d
    public Animator anim;                               //character animator
    public SpriteRenderer spriteRenderer;               //character sprite renderer
    
    
    //character variables
    [SerializeField] private float jumpForce = 2.0f;    //character jump force
    [SerializeField] private float moveSpeed = 2.0f;    //character move speed
    [SerializeField] private int jumpCount = 0;         //character jump count
    [SerializeField] private bool isLanded = false;     //is character on the platform
    [SerializeField] private bool isMoving = false;     //is character moving
                                                        //isIdle bool controls the character animations


    private void Awake()
    {
        Instance = this;
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    
    // Update is called once per frame
    void Update()
    {
        
        //if A is pressed
        if (Input.GetKey(KeyCode.A))
        {
            isMoving = true;
            spriteRenderer.flipX = true;
            transform.position += Vector3.left * (moveSpeed * Time.deltaTime);
            
        }

        
        //if D is pressed
        else if (Input.GetKey(KeyCode.D))
        {
            isMoving = true;
            spriteRenderer.flipX = false;
            transform.position += Vector3.right * (moveSpeed * Time.deltaTime);
        }
        
        
        //jumping and triple jump protection
        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount < 2))
        {
            
            //if it's first jump, player must be landed
            if ((jumpCount == 0) && (isLanded))
            {
                jump();
            }
            
            //if it's not first jump, just jump
            else if (jumpCount > 0)
            {
                jump();
            }

        }
        
        
        //turn off animation if a movement key is released
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.Space))
        {
            isMoving = false;
        }

        
        //if the character is landed and moving, turn on the run animation
        if (isMoving && isLanded)
        {
            anim.SetBool("isIdle", false);
        }
        
        
        //if the character is not landed or not moving, turn off the animation
        else if (!isMoving || !isLanded)
        {
            anim.SetBool("isIdle", true);
        }
        
    }

    
    
    //a method for character jump
    void jump()
    {
        anim.SetBool("isMoving", true);     
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpCount++;
    }

    
    
    //when collision starts
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if the collision between character and platform starts, character is landed
        if (other.gameObject.CompareTag("platform"))
        {
            jumpCount = 0;
            isLanded = true;
        }
    }

    
    
    //when collision ends
    private void OnCollisionExit2D(Collision2D other)
    {
        //if the collision between character and platform ends, character is not landed
        if (other.gameObject.CompareTag("platform"))
        {
            isLanded = false;
        }
    }
    
}
