using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cutscene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            cutscene.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
