using System.Collections;
using UnityEngine;
using TMPro;

public class GameOverTracker : MonoBehaviour
{
    public Transform damageContainer;
    public LiverHealthBar healthBar;
    public LifeProgressBar progressBar;
    public GameObject gameOverCamera;
    public GameObject gameWonCamera;
    public TMP_Text scoreLabel;
    public float transitionDelay = 5;
    private bool done;

    void Update()
    {
        float currentHealth = healthBar.health - damageContainer.childCount;
        if (currentHealth <= 0 && !done)
        {
            StartCoroutine(GameOver());
            done = true;
        }
        if (progressBar.GetProgress() >= 1 && !done)
        {
            StartCoroutine(GameWon());
            done = true;
        }
    }

    IEnumerator GameOver()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(transitionDelay);
        Camera.main.gameObject.SetActive(false);
        gameOverCamera.SetActive(true);
        float ageYears = progressBar.GetAgeYears();
        scoreLabel.text = $"{progressBar.playerName}: {ageYears.ToString("F2")}";
    }

    IEnumerator GameWon()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(transitionDelay);
        Camera.main.gameObject.SetActive(false);
        gameWonCamera.SetActive(true);
    }
}
