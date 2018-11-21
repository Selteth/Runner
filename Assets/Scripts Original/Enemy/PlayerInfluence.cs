using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfluence : MonoBehaviour
{
    // The minimum angle between hit and vertical vector to kill enemy
    public float minimumAngle = 0f;
    // The maximum angle between hit and vertical vector to kill enemy
    public float maximumAngle = 50f;
    // The amount of implulse to apply to the player after killing an enemy
    public float killImpulse = 500f;
    // The amount of impulse to apply to the player after being hit by an enemy
    public float hitImpulse = 500f;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject gameObject = collision.gameObject;
        if (gameObject.tag == "Player")
        {
            Vector2 resultVector = gameObject.transform.position - transform.position;
            resultVector.Normalize();
            float angle = Vector2.Angle(Vector2.up, resultVector);
            Rigidbody2D gameObjectRigidbody = gameObject.GetComponent<Rigidbody2D>();

            if (angle >= minimumAngle && angle <= maximumAngle && gameObjectRigidbody.velocity.y < 0)
            {
                gameObjectRigidbody.velocity = new Vector2(gameObjectRigidbody.velocity.x, 10f);
                
                this.GetComponent<EnemyMain>().Damaged();
            }
            else
            {
                float directionSign = -Mathf.Sign(transform.position.x - gameObject.transform.position.x);
                gameObjectRigidbody.AddForce(Vector2.right * directionSign * hitImpulse);
                
                gameObject.GetComponent<PlayerMain>().Damaged();
            }
        }
       
    }

}
