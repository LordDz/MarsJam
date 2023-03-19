using System.Collections;
using UnityEngine;

public class YggdrasilTree : MonoBehaviour
{
    public float leaveDistance;
    private ScoreInventory scoreInventory;


    void Start()
    {
        scoreInventory = FindObjectOfType<ScoreInventory>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        AddBerries();
    }

    void AddBerries()
    {
        if (scoreInventory.CanReturnBerries())
        {
            scoreInventory.StartReturningBerries();
        }
    }
}
