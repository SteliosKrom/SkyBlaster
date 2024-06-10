using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private float blinkRate;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI pressAnyKeyToStart;
    
    private void Start()
    {
        timer = 0f;
    }

    private void Update()
    {
        timer = timer + Time.deltaTime;

        if (timer >= blinkRate)
        {
            pressAnyKeyToStart.enabled = !pressAnyKeyToStart.enabled;
            timer = 0f;
            Debug.Log("Timer is greater than blinkrate: " + timer);
        }
        PressAnyKeyToStartFunction();
    }

    public void PressAnyKeyToStartFunction()
    {
        if (Input.anyKeyDown == true)
        {
            SceneManager.LoadScene("MenuScene");
            Debug.Log("Menu scene loaded");
        }
    }
}
