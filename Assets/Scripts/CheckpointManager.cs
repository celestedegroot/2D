using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask checkpointLayer;
    public Transform currentCheckpoint;

    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        player.position = currentCheckpoint.position;
    }
}
