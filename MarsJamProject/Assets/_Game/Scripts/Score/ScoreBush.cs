using UnityEngine;

public class ScoreBush : MonoBehaviour
{
    public GameObject carveNavmesh;
    public Animator anim;
    public float leaveDistance;

    MoveArea area;

    bool isPickedUp = false;

    ScoreInventory scoreInventory;

    void Start()
    {
        area = FindObjectOfType<MoveArea>();
        scoreInventory = FindObjectOfType<ScoreInventory>();

        carveNavmesh.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return;
        if (!other.gameObject.CompareTag("Player") || !scoreInventory.CanAddBerry())
            return;

        isPickedUp = true;

        carveNavmesh.SetActive(true);
        anim.SetBool("Open", true);

        //player = other.gameObject.transform;
        //player.GetComponent<PlayerController>().SwitchSafeState(true);
        scoreInventory.AddBerry();
    }

    public void ResetBush()
    {
        isPickedUp = false;

        carveNavmesh.SetActive(false);
        anim.SetBool("Open", false);
    }
}
