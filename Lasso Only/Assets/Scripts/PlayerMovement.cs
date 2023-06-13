 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public Rigidbody2D rb; //Reference to the rigidbody attatched to the Cowboy.

    public Animator anim; //Reference to the animator

    public Transform attackPoint; //Reference to the end of the lasso
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;


    public float jumpingForce = 20f; //how high player jumps
    public Transform feet;    
    public LayerMask groundLayers; 

    float movementX; //For movement along X-axis
    bool isGrounded; //Is the player on the ground?

    private void Update()
    {
        movementX = Input.GetAxisRaw("Horizontal"); //Left & Right movement

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        //Plays the walking animation
        
        if (Mathf.Abs(movementX) > 0.05f)
        {
            anim.SetBool("isWalking", true);            
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
        //if movement if going toward the left the sprite animation will be flipped to face the left.

        if (movementX > 0f)
        {
            Vector3 scale = transform.localScale;

            scale.Set(1f, 1f, 1f);

            transform.localScale = scale;
                        
        }
        else if (movementX < 0f)
        {
            Vector3 scale = transform.localScale;

            scale.Set(-1f, 1f, 1f);

            transform.localScale = scale;
                      
        }

        anim.SetBool("isGrounded", IsGrounded()); //makes makes sure player is grounded.

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();            
        }

    }


    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementX * movementSpeed, rb.velocity.y); //Left & Right movement
        rb.velocity = movement;
    }

    //Everything below is for the links to the animations.

    void Jump() //for jump movement
    {
        Vector2 movement = new Vector2(rb.velocity.x, jumpingForce);

        rb.velocity = movement;
    }

    //If Player is on the ground this will return true/yes if not, false/no, does this by checking if the feet are in contact with the "Ground" layer.

    public bool IsGrounded() // to check if player is grounded
    {
        Collider2D isOnGround = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);
         
        if (isOnGround != null)
        {
            return true;
        }
        return false;
    }

    void Attack()
    {

        //play attack animation 
        anim.SetTrigger("Attack"); //Refers to the Attack trigger in animation
        
        //detect enemies in range of attack, store list of enemies hit
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //kill them
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("You hit " + enemy.name); //Says in the Debug Log which enemy we hit.

            enemy.GetComponent<Enemy>().TakeDamage(20);
        }

    }

}
