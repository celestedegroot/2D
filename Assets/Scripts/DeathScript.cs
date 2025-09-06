using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DeathScript : MonoBehaviour
{
    [SerializeField] private CheckpointManager checkpointManager;
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource deathSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            deathSound.Play();
            checkpointManager.Respawn();
        }
    }
}
