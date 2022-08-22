using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private float timeToSpawn = 5f;
    private float timeSinceSpawn;
    [SerializeField] private ObjectPool objectPool;
    [SerializeField] private GameObject[] critterPrefab;

    private LightController lightController;

    private void Start()
    {
        lightController = FindObjectOfType<LightController>();
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (!lightController.TimeNowDay)
        {
            timeSinceSpawn += Time.deltaTime;
            if (timeSinceSpawn >= timeToSpawn)
            {
                int index = Random.Range(0, critterPrefab.Length);
                GameObject newCritter = objectPool.GetObject(critterPrefab[index], transform);
            
                newCritter.transform.position = this.transform.position;
                timeSinceSpawn = 0f;
            }   
        }
    }
}
