using System.Collections.Generic;
using UnityEngine;

public class FlySkillCreator : ILevelCreator
{
    private readonly Variables variables;

    public FlySkillCreator(Variables variables)
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
        float flyDuration = variables.flySkillDuration;
        float gravity = -Physics2D.gravity.y;
        for (int i = 0; i < count; i++)
        {
            Vector2 distance = GetDistance(runSpeed, jumpSpeed, jumpTime, gravity, flyDuration);
            GeneratedPlatform platform = new GeneratedPlatform(distance, size);
            platforms.Add(platform);
        }

        return platforms;
    }

    private Vector2 GetDistance(float runSpeed, float jumpSpeed,
        float jumpTime, float gravity, float flyDuration)
    {
        float maxProjectileHeight = StandardLevelCreator.GetMaxProjectileHeight(jumpSpeed, jumpTime, gravity);
        float randomProjectileHeight = Random.Range(-maxProjectileHeight, maxProjectileHeight);
        float maxProjectileWidth = StandardLevelCreator.GetMaxProjectileWidth(runSpeed, jumpSpeed, gravity, randomProjectileHeight);
        float maxLinearHeight = StandardLevelCreator.GetMaxLinearHeight(jumpSpeed, jumpTime);
        float maxLinearWidth = StandardLevelCreator.GetMaxLinearWidth(runSpeed, jumpSpeed, flyDuration, maxLinearHeight);
        float flyWidth = runSpeed * NormalizeFlyDuration(flyDuration);


        float height = NormalizeHeight(randomProjectileHeight + maxLinearHeight * Mathf.Sign(randomProjectileHeight));
        float maxWidth = maxProjectileWidth + maxLinearWidth + flyWidth;
        float width = Random.Range(maxWidth * 0.8f, maxWidth);

        return new Vector2(width, height);
    }

    private float NormalizeFlyDuration(float flyDuration)
    {
        return flyDuration * 0.6f;
    }

    private float NormalizeHeight(float height)
    {
        if (height > 0)
            height *= 0.8f;

        return height;
    }
}
