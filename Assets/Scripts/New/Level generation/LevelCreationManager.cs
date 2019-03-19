using UnityEngine;
using System.Collections.Generic;

public class LevelCreationManager : MonoBehaviour
{
    public BoxCollider2D lastPlatform;
    public BoxCollider2D platformPrefab;
    
    private ILevelCreator levelCreator;
    /* Distance from player to last generated platform */
    private float distanceToLastPlatform;
    private Transform playerTransform;
    /* Sprite renderer of platform prefab */
    private SpriteRenderer prefabSpriteRenderer;

    void Awake()
    {
        levelCreator = new StandardLevelCreator(GameObject.Find("Variables").GetComponent<Variables>());
        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
        prefabSpriteRenderer = platformPrefab.GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        distanceToLastPlatform = GameObject.Find("Main Camera")
               .GetComponent<CameraInfo>().GetSize().x * 1.5f;
    }
    
    void Update()
    {
        while (lastPlatform.transform.position.x <=
            playerTransform.position.x + distanceToLastPlatform)
        {
            IList<GeneratedPlatform> platforms = levelCreator.GetNextPlatforms(5);
            foreach (GeneratedPlatform g in platforms)
                CreatePlatform(g);
        }
    }

    // Instantiates specified platform
    private void CreatePlatform(GeneratedPlatform platform)
    {
        Vector2 lastPlatformEnd = GetPlatformEnd(lastPlatform);
        // Top left corner of a new platform
        Vector2 newPlatformBegin = lastPlatformEnd + platform.Distance;
        Vector2 newPlatformPosition = new Vector2(
            newPlatformBegin.x + platform.Size.x / 2.0f,
            newPlatformBegin.y - platform.Size.y / 2.0f
            );

        lastPlatform = Instantiate(platformPrefab, newPlatformPosition, 
            Quaternion.identity);
        lastPlatform.transform.localScale = new Vector2(
            platform.Size.x / lastPlatform.bounds.size.x,
            platform.Size.y / prefabSpriteRenderer.size.y
            );
    }
    
    // Returns top right corner of a platform
    private Vector2 GetPlatformEnd(BoxCollider2D platform)
    {
        return platform.bounds.center + platform.bounds.extents;
    }
}
