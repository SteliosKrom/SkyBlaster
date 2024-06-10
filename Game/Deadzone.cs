using System.Collections;
using TMPro;
using UnityEngine;

public class Deadzone : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int lives;

    [Header("AUDIO")]
    [SerializeField] private AudioSource hurtAudioSource;
    [SerializeField] private AudioClip hurtAudioClip;

    private void Start()
    {
        lives = 3;
        livesText.text = lives.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Target")
        {
            if (lives <= 0)
            {
                HandleTargetCollision();
            }
            else
            {
                lives--;
                livesText.text = lives.ToString();
                playerController.playerAnim.Play("PlayerDamage");
                
                if (lives != 0)
                {
                    AudioManager.instance.PlaySound(hurtAudioSource, hurtAudioClip);
                }

                if (lives <= 0)
                {
                    HandleTargetCollision();
                }
            }
        }
        else if (other.tag == "Player")
        {
            HandlePlayerCollision();
        }
    }

    public void HandlePlayerCollision()
    {
        Time.timeScale = 0f;
        RoundManager.instance.currentState = GameState.gameOver;
        GameUI.instance.gameOverMenu.SetActive(true);
        GameUI.instance.GameOver();
        Debug.Log("You lost the game!");
    }

    public void HandleTargetCollision()
    {
        RoundManager.instance.currentState = GameState.gameOver;
        GameUI.instance.gameOverMenu.SetActive(true);
        GameUI.instance.GameOver();
        Time.timeScale = 0f;
        Debug.Log("You lost the game!");
        Debug.Log("Lives remaining: " + lives);
    }
}
