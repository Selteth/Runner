using UnityEngine;

public class Variables : MonoBehaviour
{
    /* Platforms and obstacles */
    public Vector2 standardPlatformSize = new Vector2(5.0f, 1.0f);
    public Vector2 standardObstacleSize = new Vector2(1.5f, 4.0f);

    /* Movement */
    public float playerRunSpeed = 0.0f;
    public float playerJumpSpeed = 0.0f;
    public float playerJumpTime = 0.0f;

    /* Skills */
    public float flySkillDuration = 0.9f;
    public float highJumpMultiplier = 1.8f;
}
