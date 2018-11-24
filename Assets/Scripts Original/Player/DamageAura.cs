using UnityEngine;

// Represents damage aura of the hero.
// Is increasing when hero is falling.
// The longer hero is falling the more damage enemies will take and
// the more distance is taking into account
public class DamageAura : MonoBehaviour
{
    // Maximum radius of damage
    public float maxRadius;
    // Radius step per fixed update
    public float radiusPerFUpdate;
    // Maximum push force applied to enemies
    public float maxPushForce;
    // Push force per fixed update
    public float forcePerFUpdate;
    // Impulse speed after killing enemy
    public float killImpulseSpeed;
    
    // Radius counter.
    // Is increasing when hero is falling.
    // Cannot be greater than maxRadius
    private float radiusCounter = 0f;
    // Push force counter.
    // Is increasing when hero is falling.
    // Cannot be greater than maxPushForce
    private float pushForceCounter = 0f;
    // Player position
    private Transform playerTransofrm;
    // Player movement script
    private MovementControl playerMovement;
    // Whether hero is grounded
    private bool isGrounded = false;
    // Player rigidbody. Needed for jump after killing enemy
    private Rigidbody2D playerRigidbody;

    void Awake()
    {
        playerTransofrm = GetComponent<Transform>();
        playerMovement = GetComponent<MovementControl>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        isGrounded = playerMovement.IsGrounded();
    }

    void FixedUpdate()
    {
        IncreaseImpact();
        ApplyGroundDamage();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (HasDamageAura())
            {
                collision.gameObject.GetComponent<Damage>().Damaged();
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, killImpulseSpeed);
                Debug.Log("Killed him!");
            }
            else
            {
                GetComponent<Damage>().Damaged();
                Debug.Log("I am damaged!");
            }
        }
    }

    private bool HasDamageAura()
    {
        return radiusCounter > 0;
    }

    private void IncreaseImpact()
    {
        // As long as hero is falling...
        if (playerMovement.IsFalling())
        {
            /* ...increase his damage aura power */
            radiusCounter += radiusPerFUpdate;
            pushForceCounter += forcePerFUpdate;
            
            if (radiusCounter > maxRadius)
            {
                radiusCounter = maxRadius;
            }
            if (pushForceCounter > maxPushForce)
            {
                pushForceCounter = maxPushForce;
            }
        }
    }

    private void ApplyGroundDamage()
    {
        if (isGrounded)
        {
            if (radiusCounter > 0)
            {
                // Find all enemies in given radius...
                Collider2D[] enemies = Physics2D.OverlapCircleAll(playerTransofrm.position, radiusCounter, 1 << LayerMask.NameToLayer("Enemy"));
                // ...and apply to them damage depending on their distance to the hero
                foreach (Collider2D enemy in enemies)
                {
                    float distanceCoefficient = GetDistanceCoefficient(enemy);
                    float pushForce = pushForceCounter * (1 - distanceCoefficient);

                    PushEnemy(enemy, pushForce);
                    // TODO. Apply damage to the enemy...
                }
            }
            
            radiusCounter = 0f;
            pushForceCounter = 0f;
        }
    }

    // Returns ratio between player-enemy distance and auro radius distance.
    private float GetDistanceCoefficient(Collider2D enemy)
    {
        float distance = Vector2.Distance(playerTransofrm.position, enemy.transform.position);
        float distanceCoefficient = distance / radiusCounter;

        /* I DO NOT WHY BUT SOMETIMES RATIO IS GREATER THAN 1. WTF? */
        if (distanceCoefficient > 1)
            return 1;
        else
            return distanceCoefficient;
    }

    // Pushes enemy with specified force
    private void PushEnemy(Collider2D enemy, float pushForce)
    {
        Rigidbody2D enemyRigidbody = enemy.GetComponent<Rigidbody2D>();
        // Push direction
        float directionSign = Mathf.Sign(enemy.transform.position.x - playerTransofrm.position.x);
        enemyRigidbody.AddForce(Vector2.right * directionSign * pushForce);
    }

}
