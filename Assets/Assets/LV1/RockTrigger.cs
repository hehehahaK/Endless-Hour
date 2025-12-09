using UnityEngine;

public class RockTrigger : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform spawnPoint;
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            Instantiate(rockPrefab, spawnPoint.position, Quaternion.identity);
            hasTriggered = true;
        }
    }
}
