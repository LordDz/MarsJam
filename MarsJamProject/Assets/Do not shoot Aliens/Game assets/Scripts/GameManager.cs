using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Animator startPanel;
    public Animator gamePanel;
    public Animator gameOverPanel;
    public Animator startOverlay;
    public Animator tutorial;
    public Animator moveCamera;
    public Animator transition;

    public Text bestText;
    public Text scoreText;

    [HideInInspector]
    public bool gameStarted;

    [SerializeField] Spawner spawnerSmall;
    [SerializeField] Spawner spawnerBig;
    [SerializeField] PlayerController player;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!gameStarted)
            {
                StartGame();
            }
            else if (!gamePanel.gameObject.activeSelf)
            {
                StartCoroutine(RestartGame());
            }
        }
    }

    void StartGame()
    {
        gameStarted = true;

        moveCamera.SetTrigger("MoveCamera");

        startPanel.SetTrigger("Fade");
        startOverlay.SetTrigger("Fade");

        tutorial.SetBool("Invisible", false);

        gamePanel.SetBool("Visible", true);

        spawnerSmall.StartSpawning();
        spawnerBig.StartSpawning();
    }

    public void GameOver()
    {
        if (!gamePanel.gameObject.activeSelf)
            return;

        ScoreBush target = FindObjectOfType<ScoreBush>();
        int score = 0; //target.GetScore();

        if (score > PlayerPrefs.GetInt("Best"))
            PlayerPrefs.SetInt("Best", score);

        bestText.text = PlayerPrefs.GetInt("Best") + "";
        scoreText.text = score + "";

        gamePanel.gameObject.SetActive(false);
        gameOverPanel.SetTrigger("Game over");

        //player.Die();
    }

    IEnumerator RestartGame()
    {
        transition.SetTrigger("Transition");

        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
