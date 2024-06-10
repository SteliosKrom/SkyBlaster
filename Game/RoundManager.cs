using UnityEngine;

public enum GameState
{
    playing, 
    gameOver,
    pause
}

public class RoundManager : MonoBehaviour
{
    public static RoundManager instance;
    public GameState currentState;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentState = GameState.playing;
        Debug.Log("Game has started and the current state is: " + currentState);
    } 
}
