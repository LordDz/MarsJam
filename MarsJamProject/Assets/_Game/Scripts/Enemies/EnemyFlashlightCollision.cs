using UnityEngine;

public class EnemyFlashlightCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision: " + other.name);
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Friend"))
        {
            if (other.TryGetComponent<RespawnableObject>(out var respawnable))
            {
                respawnable.Die();
            }
        }
    }
}
