using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] int scorePerFriend = 3;
    [SerializeField] int berriesPerMedium = 4;
    [SerializeField] int berriesPerBoss = 5;
    [SerializeField] Spawner spawnerSmall;
    [SerializeField] Spawner spawnerMedium;
    [SerializeField] Spawner spawnerBig;
    private ScoreBush[] listBushes;
    private FriendSpawner friendSpawner;
    private int score = 0;
    private int scoreFriend = 0;
    private int berriesMedium = 0;
    private int berriesBoss = 0;

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
        }
    }

    public void BerryPickedUp()
    {
        berriesMedium++;
        berriesBoss++;

        if (berriesMedium >= berriesPerMedium)
        {
            berriesMedium = 0;
            spawnerMedium.SpawnEnemy();
        }

        if (berriesBoss >= berriesPerBoss)
        {
            berriesBoss = 0;
            spawnerBig.SpawnEnemy();
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

    public int GetScore()
    {
        return score;
    }
}

