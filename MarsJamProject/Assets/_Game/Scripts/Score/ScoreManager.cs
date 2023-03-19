using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int scorePerFriend = 3;
    private ScoreBush[] listBushes;
    private FriendSpawner friendSpawner;
    private int score = 0;
    private int scoreFriend = 0;

    private void Start()
    {
        friendSpawner = FindObjectOfType<FriendSpawner>();
        listBushes = FindObjectsOfType<ScoreBush>();
    }

    public void AddScore()
    {
        score++;
        scoreFriend++;
        if (scoreFriend >= scorePerFriend)
        {
            scoreFriend = 0;
            friendSpawner.SpawnFriend();

            for (int i = 0; i < listBushes.Length; i++)
            {
                listBushes[i].ResetBush();
            }
        }
    }
}

