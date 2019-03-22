using UnityEngine;
using System.Collections;

public class GhostSkill : SkillBase
{
    // Count of seconds that player can be ghost
    private readonly float duration = 2f;
    // Whether the time of using the ability has passed
    private bool timedOut = false;
    private int playerLayer;
    private int obstacleLayer;
    private int obstacleGroundLayer;
    private BoxCollider2D playerCollider;

    void Awake()
    {
        playerLayer = gameObject.layer;
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        obstacleGroundLayer = LayerMask.NameToLayer("ObstacleGround");
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleGroundLayer, true);
        StartCoroutine(TimeOver());
    }

    void FixedUpdate()
    {
        if (timedOut)
        {
            if (!IsInsideObstacle())
            {
                Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
                Physics2D.IgnoreLayerCollision(playerLayer, obstacleGroundLayer, false);
                Deactivate();
            }
        }
    }

    // Disables ability after N seconds
    private IEnumerator TimeOver()
    {
        yield return new WaitForSeconds(duration);
        timedOut = true;
    }

    private bool IsInsideObstacle()
    {
        return Physics2D.OverlapBox(gameObject.transform.position, 
            playerCollider.bounds.size, 0, 1 << obstacleLayer) != null;
    }
}