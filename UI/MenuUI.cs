using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject goBackToMenu;

    [Header("AUDIO")]
    [SerializeField] private AudioSource touchButtonAudioSource;
    [SerializeField] private AudioSource pressButtonAudioSource;
    [SerializeField] private AudioClip touchButtonAudioClip;
    [SerializeField] private AudioClip pressButtonAudioClip;

    private float delay = 0.5f;

    private void Start()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        goBackToMenu.SetActive(false);
    }

    public void PlayButton()
    {
        StartCoroutine(PlayButtonAfterDelay());
    }

    IEnumerator PlayButtonAfterDelay()
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void ControlsButton()
    {
        controlsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void CreditsButton()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        goBackToMenu.SetActive(true);
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

    public void GoBackToMenu()
    {
        creditsMenu.SetActive(false);
        controlsMenu.SetActive(false);
        mainMenu.SetActive(true);
        goBackToMenu.SetActive(false);
    }

    public void TouchButtonAudioSource()
    {
        touchButtonAudioSource.PlayOneShot(touchButtonAudioClip);
    }

    public void PressButtonAudioSource()
    {
        pressButtonAudioSource.PlayOneShot(pressButtonAudioClip);
    }
}
