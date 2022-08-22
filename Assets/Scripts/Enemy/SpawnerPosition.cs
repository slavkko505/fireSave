using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPosition : MonoBehaviour
{
    [SerializeField] private List<GameObject> SpawnPos;
    public int MaxPosCountter = 1;

    private void Start()
    {
        DisaebleAll();
        RefreshLevel();
    }

    private void DisaebleAll()
    {
        foreach (var pos in SpawnPos)
        {
            pos.SetActive(false);
        }
    }
    
    private void RefreshLevel()
    {
        if (MaxPosCountter <= SpawnPos.Count)
        {
            for (int i = 0; i < MaxPosCountter; i++)
            {
                SpawnPos[i].SetActive(true);
            }
        }
    }
    
    public void AddLevel()
    {
        MaxPosCountter++;
    } 

}
