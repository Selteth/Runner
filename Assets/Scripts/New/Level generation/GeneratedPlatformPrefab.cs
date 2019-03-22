using UnityEngine;

// Represents a game object connected to a generated platform
public class GeneratedPlatformPrefab
{
    // The object prefab
    public GameObject Prefab { get; private set; }
    // The position relative to the center of a generated platform
    public Vector2 RelativePosition { get; private set; }
    // The size
    public Vector2 Size { get; private set; }

    public GeneratedPlatformPrefab(GameObject prefab, 
        Vector2 relativePosition, Vector2 size)
    {
        Prefab = prefab;
        RelativePosition = relativePosition;
        Size = size;
    }

}
