using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private Transform checkpointTransform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.name);
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint reached!");
            checkpointManager.currentCheckpoint = checkpointTransform;
        }
    }
}
