using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelCreationManager : MonoBehaviour
{
    public BoxCollider2D lastPlatform;
    public BoxCollider2D platformPrefab;
    
    private ILevelCreator[] levelCreators;
    private float[] creationChances;
    /* Distance from player to last generated platform */
    private float distanceToLastPlatform;
    private Transform playerTransform;
    /* Sprite renderer of platform prefab */
    private SpriteRenderer prefabSpriteRenderer;
    private Movement movement;

    private const float difficultyUpTime = 30.0f;

    void Awake()
    {
        Variables variables = GameObject.Find("Variables").GetComponent<Variables>();
        StandardLevelCreator standardCreator = new StandardLevelCreator(variables);
        levelCreators = new ILevelCreator[4];

        levelCreators[0] = standardCreator;
        levelCreators[1] = new GhostSkillCreator(variables, standardCreator);
        levelCreators[2] = new FlySkillCreator(variables);
        levelCreators[3] = new HighJumpSkillCreator(variables);

        creationChances = new float[levelCreators.Length];
        creationChances[0] = 0.7f;
        creationChances[1] = 0.1f;
        creationChances[2] = 0.1f;
        creationChances[3] = 0.1f;

        GameObject player = GameObject.Find("Player");
        playerTransform = player.transform;
        movement = player.GetComponent<Movement>();
        prefabSpriteRenderer = platformPrefab.GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        distanceToLastPlatform = GameObject.Find("Main Camera")
               .GetComponent<CameraInfo>().GetSize().x * 1.5f;

        StartCoroutine(DifficultyUp());
    }
    
    void Update()
    {
        while (lastPlatform.transform.position.x <=
            playerTransform.position.x + distanceToLastPlatform)
        {
            int randomInt = ChooseCreator();
            IList<GeneratedPlatform> platforms = levelCreators[randomInt].GetNextPlatforms(1);
            foreach (GeneratedPlatform g in platforms)
                CreatePlatform(g);
        }
    }

    private int ChooseCreator()
    {
        int index = -1;
        float number = Random.Range(0.0f, 1.0f);
        while (number >= 0.0f)
        {
            ++index;
            number -= creationChances[index];
        }

        return index;
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

        if (platform.Prefabs != null)
        {
            foreach (GeneratedPlatformPrefab p in platform.Prefabs)
            {
                Vector2 position = newPlatformPosition + p.RelativePosition;
                GameObject prefab = Instantiate(p.Prefab, position, Quaternion.identity);
                BoxCollider2D collider = prefab.GetComponent<BoxCollider2D>();
                SpriteRenderer renderer = prefab.GetComponent<SpriteRenderer>();
                collider.transform.localScale = new Vector2(p.Size.x / collider.bounds.size.x, p.Size.y / renderer.size.y);
            }
        }
    }
    
    // Returns top right corner of a platform
    private Vector2 GetPlatformEnd(BoxCollider2D platform)
    {
        return platform.bounds.center + platform.bounds.extents;
    }

    private IEnumerator DifficultyUp()
    {
        yield return new WaitForSeconds(difficultyUpTime);
        if (creationChances[0] > 0.1f)
        {
            creationChances[0] -= 0.15f;
            creationChances[1] += 0.05f;
            creationChances[2] += 0.05f;
            creationChances[3] += 0.05f;
            movement.IncreaseRunSpeed(0.5f);
            Debug.Log("Difficulty up!");
            StartCoroutine(DifficultyUp());
        }
        else
        {
            creationChances[0] = 0.1f;
            creationChances[1] = 0.3f;
            creationChances[2] = 0.3f;
            creationChances[3] = 0.3f;
            movement.SetRunSpeed(0.7f);
        }
    }
}
