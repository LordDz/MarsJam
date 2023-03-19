using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreInventory : MonoBehaviour
{
    [SerializeField] List<ScoreBerry> scoreBerries;
    [SerializeField] float timePerBerry = 0.2f;

    [SerializeField] List<AudioSource> berryPickupSound;
    [SerializeField] List<AudioSource> berryReturnSound;

    private ScoreManager scoreManager;
    int nrOfBerries = 0;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void AddBerry()
    {
        scoreBerries[nrOfBerries].gameObject.SetActive(true);
        berryPickupSound[nrOfBerries].Play();
        nrOfBerries++;
        scoreManager.BerryPickedUp();
        //Debug.Log("nrOfBerries: " + nrOfBerries);
    }

    public bool CanReturnBerries()
    {
        return nrOfBerries > 0;
    }

    public bool CanAddBerry()
    {
        //Debug.Log("CanAddBerry - nr of Berries: " + nrOfBerries + " < " + scoreBerries.Count);
        return nrOfBerries < scoreBerries.Count - 1;
    }

    public void StartReturningBerries()
    {
        if (nrOfBerries > 0)
        {
            StartCoroutine(ReturnBerries());
        }
    }

    public void Reset()
    {
        for (int i = 0; i < scoreBerries.Count; i++)
        {
            scoreBerries[i].gameObject.SetActive(false);
            nrOfBerries = 0;
        }
    }

    IEnumerator ReturnBerries()
    {
        while (nrOfBerries > 0)
        {
            nrOfBerries--;
            scoreBerries[nrOfBerries].gameObject.SetActive(false);
            berryReturnSound[nrOfBerries].Play();
            scoreManager.AddScore();
            yield return new WaitForSeconds(timePerBerry);
        }
    }
}
