using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollector : MonoBehaviour {

    public GameObject deathEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Point")
        {
            Destroy(collision.gameObject);
        }
        else if(collision.tag == "Player")
        {
            if (Player.instance.immune)
            {
                Player.instance.GiveForce();
            }
            else
            {
                StartCoroutine(DeathEffect(collision.transform));
                Player.instance.isAlive = false;
                Destroy(collision.gameObject);
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
