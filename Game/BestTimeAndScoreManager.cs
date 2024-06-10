using TMPro;
using UnityEngine;

public class BestTimeAndScoreManager : MonoBehaviour
{
    public static BestTimeAndScoreManager instance;

    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private TextMeshProUGUI bestTimeText;

    [SerializeField] private int bestScore;
    [SerializeField] private float bestTime;

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
        LoadBestScoreAndTime();
    }

    private void LoadBestScoreAndTime()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        bestTime = PlayerPrefs.GetFloat("BestTime", 0);

        UpdateBestScoreText();
        UpdateBestTimeText();
    }

    public void CheckAndSetBestScore(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            UpdateBestScoreText();
        }
    }

    public void CheckAndSetBestTime(float time)
    {
        if (time > bestTime)
        {
            bestTime = time;
            PlayerPrefs.SetFloat("BestTime", bestTime);
            UpdateBestTimeText();
        }
    }

    private void UpdateBestScoreText()
    {
        if (bestScoreText != null)
        {
            bestScoreText.text = bestScore.ToString();
        }
    }

    private void UpdateBestTimeText()
    {
        if (bestTimeText != null)
        {
            int minutes = Mathf.FloorToInt(bestTime / 60);
            int seconds = Mathf.FloorToInt(bestTime % 60);
            bestTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }      
    }
}
