using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreHandler : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public CanvasGroup gameOverCanvas;
    public TextMeshProUGUI finalScoreText;
    public GameObject newHighScoreText;
    public TextMeshProUGUI secondsAwayText;

    public int amountOfEnemyTwos;

    float timeToReach;
    bool shouldCountUp = true;

    void Start() {
        timeToReach = 1;

        highScoreText.text = "high score: " + PlayerPrefs.GetInt("HIGH_SCORE", 0);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }

        if (!shouldCountUp) {
            gameOverCanvas.alpha = Mathf.Lerp(gameOverCanvas.alpha, 1, 4 * Time.deltaTime);

            if (Input.GetKeyDown(KeyCode.Space)) {
                StartCoroutine(LoadSceneCo("SampleScene"));
            }
            return;
        }

        if (Time.time > timeToReach) {
            timeToReach = Time.time + 1;
            score++;
            scoreText.text = "score: " + score;
        }
    }

    public void GameOver() {
        shouldCountUp = false;
        finalScoreText.text = "final score: " + score + " seconds";

        if (PlayerPrefs.GetInt("HIGH_SCORE", 0) == 0) {
            secondsAwayText.gameObject.SetActive(false);
        }

        if (PlayerPrefs.GetInt("HIGH_SCORE", 0) < score) {
            PlayerPrefs.SetInt("HIGH_SCORE", score);
            newHighScoreText.SetActive(true);
            secondsAwayText.gameObject.SetActive(false);
        } else if (PlayerPrefs.GetInt("HIGH_SCORE", 0) == score) {
            secondsAwayText.text = "your score is equal to your high score!";
        } else {
            secondsAwayText.text = "you were " + (PlayerPrefs.GetInt("HIGH_SCORE", 0) - score) + " seconds away from beating your high score of " + PlayerPrefs.GetInt("HIGH_SCORE", 0);
        }
    }

    IEnumerator LoadSceneCo(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone) {
            yield return null;
        }
    }
}
