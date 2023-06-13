using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowboyDeath : MonoBehaviour
{
    public Transform respawnPoint;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("NotGroundBarrier"))
        {
            Destroy(gameObject);
            LevelManager.instance.PlayerRespawn();
        }
    }

}
