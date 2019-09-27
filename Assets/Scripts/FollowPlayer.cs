using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public Transform player;
    public float smoothSpeed = .3f;

    private Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (Player.instance.isAlive)
        {
            if (player.position.y > transform.position.y)
            {

                Vector3 newpos = new Vector3(0, player.position.y, -10);
                transform.position = Vector3.SmoothDamp(transform.position, newpos, ref currentVelocity, smoothSpeed * Time.deltaTime);

            }
        }
    }


}
