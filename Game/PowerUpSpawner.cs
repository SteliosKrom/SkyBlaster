using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    private readonly float xSpawn = 7.5f;
    private readonly float ySpawn = -3.0f;
    private readonly float zSpawn = -0.07f;
    public float startSpawnTime;
    public float spawnDelay;

    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject freezingPowerUp;

    private void Start()
    {
        InvokeRepeating("SpawnPowerUp", startSpawnTime, spawnDelay);
    }

    public void SpawnPowerUp()
    {
        if (RoundManager.instance.currentState != GameState.gameOver)
        {
            var spawnPos = new Vector3(Random.Range(-xSpawn, xSpawn), ySpawn, zSpawn);
            Instantiate(freezingPowerUp, spawnPos, freezingPowerUp.transform.rotation);
        }
    }
}
