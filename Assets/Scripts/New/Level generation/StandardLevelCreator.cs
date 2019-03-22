using System.Collections.Generic;
using UnityEngine;

public class StandardLevelCreator : ILevelCreator
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
        float runSpeed = variables.playerRunSpeed;
        float jumpSpeed = variables.playerJumpSpeed;
        float jumpTime = variables.playerJumpTime;
        float gravity = -Physics2D.gravity.y;
        for (int i = 0; i < count; i++)
        {
            Vector2 distance = GetDistance(runSpeed, jumpSpeed, jumpTime, gravity);
            GeneratedPlatform platform = new GeneratedPlatform(distance, size);
            platforms.Add(platform);
        }

        return platforms;
    }

    public static float GetMaxLinearWidth(float runSpeed, float jumpSpeed, float jumpTime, float height)
    {
        return NormalizeDistance(jumpSpeed / runSpeed 
            * height * jumpTime);
    }

    public static float GetMaxLinearHeight(float jumpSpeed, float jumpTime)
    {
        return NormalizeDistance(jumpSpeed * jumpTime);
    }

    public static float GetMaxProjectileWidth(float runSpeed, float jumpSpeed, 
        float gravity, float height)
    {
        return NormalizeDistance(runSpeed * (jumpSpeed + Mathf.Sqrt(jumpSpeed * jumpSpeed
            - 2 * gravity * height)) / gravity);
    }

    public static float GetMaxProjectileHeight(float jumpSpeed, float jumpTime,
        float gravity)
    {
        return NormalizeDistance(jumpSpeed * jumpSpeed / (2 * gravity));
    }

    // Returns random distance to next platform
    private Vector2 GetDistance(float runSpeed, float jumpSpeed,
        float jumpTime, float gravity)
    {
        /* Player jump distance consists of linear motion (when 
         * player holds jump button and therefore jumps higher)
         * and projectile motion distance for an object launched 
         * at an angle (when player releases jump button and 
         * starts falling) */

        /* Find projectile motion distance */
        float maxProjectileHeight = GetMaxProjectileHeight(jumpSpeed, jumpTime, gravity);
        float randomProjectileHeight = Random.Range(-maxProjectileHeight, maxProjectileHeight);
        float maxProjectileWidth = GetMaxProjectileWidth(runSpeed, jumpSpeed, gravity, randomProjectileHeight);
        float randomProjectileWidth = Random.Range(0, maxProjectileWidth);

        /* Find linear motion distance */
        float maxLinearHeight = GetMaxLinearHeight(jumpSpeed, jumpTime);
        float maxLinearWidth = GetMaxLinearWidth(runSpeed, jumpSpeed, jumpTime, maxLinearHeight);
        
        float height = randomProjectileHeight + maxLinearHeight * Mathf.Sign(randomProjectileHeight);
        float width = randomProjectileWidth + maxLinearWidth;

        return new Vector2(width, height);
    }
    
    // Lowers maximum distance to a generated platform
    // so that player will definitely reach it
    private static float NormalizeDistance(float distance)
    {
        /* Be careful when changing distance multiplier.
         * If you change it to 1.0f or greater then due 
         * to floating point precision, Mathf.Sqrt
         * in function GetMaxProjectileWidth will get
         * negative number and try to calculate the
         * square root of it. Unity player (from 
         * 21.03.2019) will stuck and start eating memory */

        return distance * 0.9f;
    }
}