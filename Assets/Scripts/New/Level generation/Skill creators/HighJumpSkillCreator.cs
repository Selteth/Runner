using System.Collections.Generic;
using UnityEngine;

public class HighJumpSkillCreator : ILevelCreator
{
    private readonly Variables variables;

    public HighJumpSkillCreator(Variables variables)
    {
        this.variables = variables;
    }

    public IList<GeneratedPlatform> GetNextPlatforms(int count)
    {
        List<GeneratedPlatform> platforms = new List<GeneratedPlatform>(count);

        Vector2 size = variables.standardPlatformSize;
        float runSpeed = variables.playerRunSpeed;
        float jumpSpeed = variables.playerJumpSpeed;
        float jumpTime = variables.playerJumpTime;
        float gravity = -Physics2D.gravity.y;
        float jumpMultiplier = variables.highJumpMultiplier;
        for (int i = 0; i < count; i++)
        {
            Vector2 distance = GetDistance(runSpeed, jumpSpeed, jumpTime, gravity, jumpMultiplier);
            GeneratedPlatform platform = new GeneratedPlatform(distance, size);
            platforms.Add(platform);
        }

        return platforms;
    }

    public Vector2 GetDistance(float runSpeed, float jumpSpeed, float jumpTime, 
        float gravity, float jumpMultiplier)
    {
        float newJumpSpeed = jumpSpeed * jumpMultiplier;
        float maxProjectileHeight = StandardLevelCreator.GetMaxProjectileHeight(newJumpSpeed, jumpTime, gravity);
        float randomProjectileHeight = Random.Range(maxProjectileHeight * 0.6f, maxProjectileHeight);
        float maxProjectileWidth = StandardLevelCreator.GetMaxProjectileWidth(runSpeed, newJumpSpeed, gravity, randomProjectileHeight);
        float maxLinearHeight = StandardLevelCreator.GetMaxLinearHeight(newJumpSpeed, jumpTime);
        float maxLinearWidth = StandardLevelCreator.GetMaxLinearWidth(runSpeed, newJumpSpeed, jumpTime, maxLinearHeight);

        float height = randomProjectileHeight + maxLinearHeight 
            * (randomProjectileHeight == 0.0f ? 0.0f : Mathf.Sign(randomProjectileHeight));
        float width = maxProjectileWidth + maxLinearWidth;

        return new Vector2(width, height);
    }
}