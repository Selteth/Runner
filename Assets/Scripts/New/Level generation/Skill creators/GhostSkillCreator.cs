using System.Collections.Generic;
using UnityEngine;

public class GhostSkillCreator : ILevelCreator
{
    private Variables variables;
    private StandardLevelCreator standardLevelCreator;

    public GhostSkillCreator(Variables variables, 
        StandardLevelCreator standardLevelCreator)
    {
        this.variables = variables;
        this.standardLevelCreator = standardLevelCreator;
    }

    public IList<GeneratedPlatform> GetNextPlatforms(int count)
    {
        IList<GeneratedPlatform> platforms = standardLevelCreator.GetNextPlatforms(count);

        Vector2 obstacleSize = variables.standardObstacleSize;
        for (int i = 0; i < platforms.Count; i++)
        {
            GeneratedPlatform p = platforms[i];
            p.Prefabs = GetObstacles(p, obstacleSize);
        }

        return platforms;
    }

    public IList<GeneratedPlatformPrefab> GetObstacles(GeneratedPlatform p, Vector2 size)
    {
        IList<GeneratedPlatformPrefab> obstacles = new List<GeneratedPlatformPrefab>();

        float y = p.Size.y / 2.0f + size.y / 2.0f;
        float x = 0f;
        if (size.x > p.Size.x)
            size.x = p.Size.x;
        else
            x = Random.Range(-p.Size.x / 2.0f + size.x / 2.0f, p.Size.x / 2.0f - size.x / 2.0f);

        GameObject prefab = Resources.Load<GameObject>("Prefabs/TestObstacle");
        Vector2 position = new Vector2(x, y);
        GeneratedPlatformPrefab obstacle = new GeneratedPlatformPrefab(prefab, position, size);

        obstacles.Add(obstacle);

        return obstacles;
    }
}
