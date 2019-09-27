using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {

    public GameObject explosionEffect;
    public GameObject deathEffect;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (Player.instance.immune)
            {
                FindObjectOfType<AudioManager>().Play("Explosion");
                Player.instance.GiveForce();
                DestroyThis();
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Explosion");
                
                Player.instance.isAlive = false;
                Destroy(collision.gameObject);
                StartCoroutine(DeathEffect(collision.transform));
                
                DestroyThis();
            }
        }
    }

    public void DestroyThis()
    {
        
        StartCoroutine(ExplosionEffect(gameObject.transform));
        sr.sprite = null;
        
    }

    IEnumerator ExplosionEffect(Transform bomb)
    {
        GameObject effect = Instantiate(explosionEffect, bomb.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        Destroy(effect);
    }


    IEnumerator DeathEffect(Transform player)
    {
        GameObject effect = Instantiate(deathEffect, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1);
        GameManager.instance.EndGame();
        Destroy(effect);

    }
}
