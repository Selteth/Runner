using UnityEngine;
using System.Collections.Generic;

public class GeneratedPlatform
{
    public Vector2 Distance { get; private set; }
    public Vector2 Size { get; private set; }
    public IList<GeneratedPlatformPrefab> Prefabs { get; set; }

    public GeneratedPlatform(Vector2 distance, Vector2 size)
        : this(distance, size, null)
    {

    }

    public GeneratedPlatform(Vector2 distance, Vector2 size,
        IList<GeneratedPlatformPrefab> prefabs)
    {
        Distance = distance;
        Size = size;
        Prefabs = prefabs;
    }

}

