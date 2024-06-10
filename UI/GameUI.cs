using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public static GameUI instance;
    public PlayerController playerController;

    [Header("AUDIO")]
    [SerializeField] private AudioSource mainGameAudioSource;
    [SerializeField] private AudioSource touchButtonAudioSource;
    [SerializeField] private AudioSource pressButtonAudioSource;
    [SerializeField] private AudioSource gameOverAudioSource;

    [SerializeField] private AudioClip touchButtonAudioClip;
    [SerializeField] private AudioClip pressButtonAudioClip;
    [SerializeField] private AudioClip gameOverAudioClip;

    [Header("TEXT")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI bulletsText;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject pauseMenu;
    public GameObject gameOverMenu;

    public int score;
    public float timer;
    public bool volumeIsActive;
    private float delay = 0.5f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateTimer();
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        volumeIsActive = true;
    }

    private void Update()
    {
        UpdateTimer();
        PauseMenuInput();
        BestTimeAndScoreManager.instance.CheckAndSetBestScore(score);
        BestTimeAndScoreManager.instance.CheckAndSetBestTime(timer);
    }

    public void UpdateScore(int increment = 1)
    {
        score += increment;
        scoreText.text = score.ToString();
    }

    public void PauseMenuInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentState == GameState.playing)
        {
            PauseGame();
            playerController.freezingPowerUpText.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && RoundManager.instance.currentState == GameState.pause)
        {
            ResumeGame();
            pauseMenu.SetActive(false);
        }
    }

    public void GameOver()
    {
        RoundManager.instance.currentState = GameState.gameOver;
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        mainGameAudioSource.Stop();
        AudioManager.instance.PlaySound(gameOverAudioSource, gameOverAudioClip);
        BestTimeAndScoreManager.instance.CheckAndSetBestScore(score);
        BestTimeAndScoreManager.instance.CheckAndSetBestTime(timer);
        playerController.freezingPowerUpText.SetActive(false);
        Debug.Log("Current state is: " + RoundManager.instance.currentState);
    }

    public void PauseGame()
    {
        RoundManager.instance.currentState = GameState.pause;
        Time.timeScale = 0f;
        Debug.Log("Current state is: " + RoundManager.instance.currentState);
    }

    public void ResumeGame()
    {
        if (playerController.isSlowMotion == false)
        {
            RoundManager.instance.currentState = GameState.playing;
            Time.timeScale = 1f;
            Debug.Log("Current state is: " + RoundManager.instance.currentState);
        }
        else
        {
            RoundManager.instance.currentState = GameState.playing;
            Time.timeScale = 0.5f;
            Debug.Log("Current state is: " + RoundManager.instance.currentState);
        }
    }

    public void UpdateBulletsInfo(int currentBullets, int maxBullets)
    {
        if (maxBullets == int.MaxValue)
        {
            bulletsText.text = "∞ / ∞";
        }
        else
        {
            bulletsText.text = currentBullets + "/" + maxBullets;
        }
    }

    public void UpdateTimer()
    {
        timerText.text = Time.time.ToString("#,#");
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ContinueButton()
    {
        pauseMenu.SetActive(false);
        ResumeGame();
    }

    public void ExitButton()
    {
        StartCoroutine(ExitButtonAfterDelay());
    }

    IEnumerator ExitButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        Application.Quit();
    }

    public void RestartButton()
    {
        StartCoroutine(RestartButtonAfterDelay());
    }

    IEnumerator RestartButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void GameVolume()
    {
        if (!volumeIsActive)
        {
            mainGameAudioSource.UnPause();
            volumeIsActive = true;
            Debug.Log("Volume is: " + volumeIsActive);
        }
        else
        {
            mainGameAudioSource.Pause();
            volumeIsActive = false;
            Debug.Log("Volume is: " + volumeIsActive);
        }
    }

    public void BackToMenuFromGameOver()
    {
        StartCoroutine(BackToMenuAfterDelay());
    }

    IEnumerator BackToMenuAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene("MenuScene");
    }

    public void PressButtonAudioSource()
    {
        pressButtonAudioSource.PlayOneShot(pressButtonAudioClip);
    }

    public void TouchButtonAudioSource()
    {
        touchButtonAudioSource.PlayOneShot(touchButtonAudioClip);
    }
}
