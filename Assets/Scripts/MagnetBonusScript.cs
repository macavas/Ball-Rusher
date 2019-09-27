using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBonusScript : MonoBehaviour {

    public Transform target;

    public float speed = 10f;

	// Update is called once per frame
	void Update () {
        if(target == null)
        {
            return;
        }
        transform.position = target.transform.position;
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Point" || collision.tag == "CoinBonus")
        {
            collision.gameObject.transform.position = Vector2.MoveTowards(collision.transform.position, transform.position, speed * Time.deltaTime);
        }
    }
}
