using System.Collections.Generic;
using UnityEngine;

class StandardLevelCreator : ILevelCreator
{
    private Variables variables;
    
    public StandardLevelCreator(Variables variables)
    {
        this.variables = variables;
    }

    // Generates specified amount of platforms
    public IList<GeneratedPlatform> GetNextPlatforms(int count)
    {
        List<GeneratedPlatform> platforms = new List<GeneratedPlatform>(count);

        Vector2 size = variables.standardPlatformSize;
        for (int i = 0; i < count; i++)
        {
            Vector2 distance = GetDistance();
            GeneratedPlatform platform = new GeneratedPlatform(distance, size);
            platforms.Add(platform);
        }

        return platforms;
    }

    public float GetMaxWidth(float height)
    {
        float vx = variables.playerRunSpeed;
        float vy = variables.playerJumpSpeed;
        float jumpTime = variables.playerMaxJumpTime;
        float g = -Physics2D.gravity.y;
        float maxWidth = (vy / vx + Mathf.Sqrt(Mathf.Pow(vy / vx, 2.0f) - 2 * g * height / (vx * vx))) / g * vx * vx + vx * jumpTime;

        return maxWidth;
    }

    public float GetMaxHeight()
    {
        float vy = variables.playerJumpSpeed;
        float jumpTime = variables.playerMaxJumpTime;
        float g = -Physics2D.gravity.y;
        float maxHeight = vy * vy / (2 * g) + vy * jumpTime;

        return maxHeight;
    }

    // Returns random distance to next platform
    private Vector2 GetDistance()
    {
        /* Player jump distance consists of linear motion (when 
         * player holds jump button and therefore jumps higher)
         * and projectile motion distance for an object launched 
         * at an angle (when player releases jump button and 
         * starts falling) */

        float vx = variables.playerRunSpeed;
        float vy = variables.playerJumpSpeed;
        float jumpTime = variables.playerMaxJumpTime;
        float g = -Physics2D.gravity.y;

        /* Find projectile motion distance */
        float maxHeight = vy * vy / (2 * g);
        float normalizedHeight = NormalizeDistance(maxHeight);
        float height = Random.Range(-normalizedHeight, normalizedHeight);
        float maxWidth = vx * (vy + Mathf.Sqrt(vy * vy - 2 * g * (height))) / g;
        float normalizedWidth = NormalizeDistance(maxWidth);
        float width = Random.Range(0, normalizedWidth);

        /* Add linear motion distance */
        height += NormalizeDistance(vy * jumpTime * Mathf.Sign(height));
        width += NormalizeDistance(vx * jumpTime);

        return new Vector2(width, height);
    }
    
    // Lowers maximum distance to a generated platform
    // so that player will definitely reach it
    private float NormalizeDistance(float distance)
    {
        return distance * 0.9f;
    }
}