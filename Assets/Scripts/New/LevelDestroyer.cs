using UnityEngine;

public class LevelDestroyer : MonoBehaviour
{
    private float distanceToCamera;

    private void Start()
    {
        LevelCreator levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
        Camera camera = GetComponentInParent<Camera>();
        
        float height = 2f * camera.orthographicSize;
        float width = height * camera.aspect;

        distanceToCamera = width * 1.5f + levelCreator.maxWidth;

        transform.position = new Vector2(
            transform.parent.position.x - distanceToCamera,
            transform.parent.position.y
            );
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
    }
}
