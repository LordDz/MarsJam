using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int scorePerFriend = 3;
    [SerializeField] int scorePerBoss = 3;
    [SerializeField] Spawner spawnerSmall;
    [SerializeField] Spawner spawnerBig;
    private ScoreBush[] listBushes;
    private FriendSpawner friendSpawner;
    private int score = 0;
    private int scoreFriend = 0;
    private int scoreBoss = 0;

    private void Start()
    {
        friendSpawner = FindObjectOfType<FriendSpawner>();
        listBushes = FindObjectsOfType<ScoreBush>();
    }

    public void AddScore()
    {
        score++;
        scoreBoss++;
        scoreFriend++;

        if (scoreBoss >= scorePerBoss)
        {
            scoreBoss = 0;
            spawnerBig.SpawnEnemy();
        }

        if (scoreFriend >= scorePerFriend)
        {
            scoreFriend = 0;
            friendSpawner.SpawnFriend();
        }
    }

    public void ResetScore()
    {
        scoreFriend = 0;
        for (int i = 0; i < listBushes.Length; i++)
        {
            listBushes[i].ResetBush();
        }
    }
}

