using System.Collections;
using UnityEngine;

public class ScoreBerry : MonoBehaviour
{
    [SerializeField] int score = 1;


    public void Pickup(Transform plr)
    {
        transform.parent = plr;
    }
}
