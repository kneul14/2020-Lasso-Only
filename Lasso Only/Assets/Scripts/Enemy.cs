using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movementspeed = 2f;
    public Rigidbody2D rb;
    public LayerMask groundLayers;

    public Animator anim; //Reference to the animator

    public Transform groundCheck; //Checks to see if Robber is on the ground.

    bool isFacingRight = true;

    RaycastHit2D hit;

    public int maxHealth = 100;
    int currentHealth;


    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        anim.SetTrigger("Hurt");

        if(currentHealth <= 0)
        {
            Die();
        }
    }    

    private void Update()
    {  //Enemy will patrol the ground space they are given when they are on the ground.
        hit = Physics2D.Raycast(groundCheck.position, -transform.up, 1f, groundLayers);
    }

    private void FixedUpdate()
    {
        if (hit.collider != false)
        {
            if (isFacingRight)
            {
                rb.velocity = new Vector2(movementspeed, rb.velocity.y);
                
            }
            else
            {
                rb.velocity = new Vector2(-movementspeed, rb.velocity.y);
                
            }
            
        }
        else
        {
            isFacingRight = !isFacingRight; //Makes it the opposite of whatever direction it was facing.
            transform.localScale = new Vector3(-transform.localScale.x, 1f, 1f);
            //Debug.Log("Is not hitting ground");
        }

    }

    void Die()
    {
        Debug.Log("Enemy Died!");

        anim.SetBool("isDead", true);

        Destroy(gameObject);    
        //GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;

    }


    /*Beforehand the Robber(Enemies) would collide with the cactuses due to them having a box collider on them so that 
     *they could kill the player but with this block of code it means that the robbers wont collide with either themselves or the cacti
     meaning that the robbers can patrol and not be stopped by the cacti*/
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }

}

