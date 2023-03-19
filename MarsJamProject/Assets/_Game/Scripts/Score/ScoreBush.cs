using UnityEngine;

public class ScoreBush : MonoBehaviour
{
    bool isPickedUp = false;
    ScoreInventory scoreInventory;
    [SerializeField] GameObject circle;
    [SerializeField] GameObject scoreBerrySprite;

    void Start()
    {
        scoreInventory = FindObjectOfType<ScoreInventory>();
    }

    private void Update()
    {
        if (!isPickedUp)
        {
            MoveAroundBerry();
        }
    }

    private void MoveAroundBerry()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return;
        if (!other.gameObject.CompareTag("Player") || !scoreInventory.CanAddBerry())
            return;

        isPickedUp = true;
        scoreBerrySprite.SetActive(false);
        circle.SetActive(false);
        scoreInventory.AddBerry();
    }

    public void ResetBush()
    {
        isPickedUp = false;
        scoreBerrySprite.SetActive(true);
        circle.SetActive(true);
    }
}
