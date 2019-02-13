using UnityEngine;

public class GhostSkill : SkillBase
{
    private readonly float duration = 3.0f;
    private float durationCounter = 0f;
    private bool isInsideObstacle = false;
    private int playerLayer;
    private int obstacleLayer;

    void Awake()
    {
        playerLayer = gameObject.layer;
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    void Start()
    {
        
    }

    void Update()
    {
        durationCounter += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == obstacleLayer)
            isInsideObstacle = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == obstacleLayer)
            isInsideObstacle = false;
    }

    protected override void DoActivate()
    {
        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, true);
    }

    protected override bool DoDeactivate()
    {
        if (isInsideObstacle || durationCounter < duration)
            return false;

        Physics2D.IgnoreLayerCollision(playerLayer, obstacleLayer, false);
        durationCounter = 0f;
        return true;
    }
}