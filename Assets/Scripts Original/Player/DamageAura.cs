using UnityEngine;

// Represents damage aura of the hero.
// Is increasing when hero is falling.
// The longer hero is falling the more damage enemies will take and
// the more distance is taking into account
public class DamageAura : MonoBehaviour
{
    // Maximum damage applied to enemies
    public float maxDamage;
    // Damage step per fixed update
    public float damagePerFUpdate;
    // Maximum radius of damage
    public float maxRadius;
    // Radius step per fixed update
    public float radiusPerFUpdate;
    // Maximum push force applied to enemies
    public float maxPushForce;
    // Push force per fixed update
    public float forcePerFUpdate;

    // Damage counter.
    // Is increasing when hero is falling.
    // Cannot be greater than maxDamage
    private float damageCounter = 0f;
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

    void Awake()
    {
        playerTransofrm = GetComponent<Transform>();
        playerMovement = GetComponent<MovementControl>();
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

    public float GetDamage()
    {
        return damageCounter;
    }

    private void IncreaseImpact()
    {
        // As long as hero is falling...
        if (playerMovement.IsFalling())
        {
            /* ...increase his damage aura */
            damageCounter += damagePerFUpdate;
            radiusCounter += radiusPerFUpdate;
            pushForceCounter += forcePerFUpdate;
            
            if (damageCounter > maxDamage)
            {
                damageCounter = maxDamage;
            }
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
                    float damage = damageCounter * (1 - distanceCoefficient);
                    float pushForce = pushForceCounter * (1 - distanceCoefficient);

                    PushEnemy(enemy, pushForce);
                    // TODO. Apply damage to the enemy...
                }
            }

            damageCounter = 0f;
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
