using System.Collections.Generic;
using UnityEngine;

class StandardLevelCreator : ILevelCreator
{
    private Movement playerMovement;
    
    public StandardLevelCreator(Movement playerMovement)
    {
        this.playerMovement = playerMovement;
    }

    // Generates specified amount of platforms
    public IList<GeneratedPlatform> GetNextPlatforms(int count)
    {
        List<GeneratedPlatform> platforms = new List<GeneratedPlatform>(count);

        Vector2 size = new Vector2(5.0f, 1.0f);
        for (int i = 0; i < count; i++)
        {
            Vector2 distance = GetDistance();
            GeneratedPlatform platform = new GeneratedPlatform(
                distance, size
                );
            platforms.Add(platform);
        }

        return platforms;
    }

    // Returns random distance to next platform
    private Vector2 GetDistance()
    {
        /* Player jump distance consists of linear motion (when 
         * player holds jump button and therefore jumps higher)
         * and projectile motion distance for an object launched 
         * at an angle (when player releases jump button and 
         * starts falling */

        float vx = playerMovement.runSpeed;
        float vy = playerMovement.jumpSpeed;
        float jumpTime = playerMovement.maxJumpTime;
        float g = -Physics2D.gravity.y;

        /* Find projectile motion distance for ... at an angle*/
        float maxHeight = vy * vy / (2 * g);
        float normalizedHeight = NormalizeDistance(maxHeight);
        float height = Random.Range(-normalizedHeight, normalizedHeight);
        float maxWidth = (vy / vx + Mathf.Sqrt(Mathf.Pow(vy / vx, 2.0f) - 2 * g * height / (vx * vx))) / g * vx * vx;
        float normalizedWidth = NormalizeDistance(maxWidth);
        float width = Random.Range(0, normalizedWidth);

        /* Add linear motion distance */
        height += vy * jumpTime * Mathf.Sign(height);
        width += vx * jumpTime;
        
        return new Vector2(width, height);
    }

    // Lowers maximum distance to a generated platform
    // so that player will definitely reach it
    private float NormalizeDistance(float distance)
    {
        return distance * 0.85f;
    }
}