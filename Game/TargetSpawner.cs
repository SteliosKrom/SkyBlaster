using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] targetPrefab;
    [SerializeField] private float cooldown;
    private float timer;
    private int targetCreated;
    private int targetMilestone = 10;
    [SerializeField] private float xSpawnBounds;
    [SerializeField] private float ySpawnBounds;
    [SerializeField] private float zSpawnBounds;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = cooldown;
            targetCreated++;

            if (targetCreated > targetMilestone && cooldown >= 0.8f)
            {
                targetMilestone += 10;
                cooldown -= 0.2f;
            }
            Vector3 spawnPos = new Vector3(Random.Range(-xSpawnBounds, xSpawnBounds), ySpawnBounds, zSpawnBounds);
            int targetIndex = Random.Range(0, targetPrefab.Length);
            Instantiate(targetPrefab[targetIndex], spawnPos,Quaternion.identity);
        }
    }
}
