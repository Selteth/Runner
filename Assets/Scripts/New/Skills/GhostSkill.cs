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

    void Awake()
    {
        playerLayer = gameObject.layer;
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    void Start()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);
        StartCoroutine(TimeOver());
    }

    // Disables ability after N seconds
    private IEnumerator TimeOver()
    {
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
        Deactivate();
    }
}