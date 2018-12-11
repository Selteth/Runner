using UnityEngine;

public class LevelDestroyer : MonoBehaviour
{
    private float distanceToCamera;

    private void Start()
    {
        LevelCreator levelCreator = GameObject.Find("LevelCreator").GetComponent<LevelCreator>();
        CameraInfo cameraInfo = GetComponentInParent<CameraInfo>();
        
        distanceToCamera = cameraInfo.GetSize().x * 1.5f + levelCreator.maxWidth;

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
