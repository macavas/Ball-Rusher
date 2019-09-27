using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    public GameObject deathEffect;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            if (Player.instance.immune)
            {
                Player.instance.GiveForce();
            }
            else
            {
                Player.instance.isAlive = false;
                FindObjectOfType<AudioManager>().Play("Death");
                Destroy(collision.gameObject);
                StartCoroutine(DeathEffect(collision.transform));
            }
            
        }
        
    }

    IEnumerator DeathEffect(Transform player)
    {
        GameObject effect = Instantiate(deathEffect, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        GameManager.instance.EndGame();
        Destroy(effect);

    }

}
