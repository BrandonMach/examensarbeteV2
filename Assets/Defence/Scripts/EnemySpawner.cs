using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public float spawnSpeed, timer;
    private float delay, lowX, highX, lowZ, highZ, spawnX, spawnZ;
    private Vector3 spawnPos;
    [SerializeField] private enum position { left, right, top, bottom };
    [SerializeField] private position pos;
    private void Start()
    {
        delay = spawnSpeed;
        switch (pos)
        {
            case position.left:
                lowX = -100;
                highX = -100;
                lowZ = -65;
                highZ = 65;
                break;
            case position.right:
                lowX = 100;
                highX = 100;
                lowZ = -65;
                highZ = 65;
                break;
            case position.top:
                lowX = -100;
                highX = 100;
                lowZ = 65;
                highZ = 65;
                break;
            case position.bottom:
                lowX = -100;
                highX = 100;
                lowZ = -65;
                highZ = -65;
                break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (DefenceManager.Instance.startGame)
        {
            delay -= Time.deltaTime;
            if (delay <= 0)
            {
                spawnX = Random.Range(lowX, highX);
                spawnZ = Random.Range(lowZ, highZ);
                spawnPos = new Vector3(spawnX, 2.5f, spawnZ);
                Instantiate(EnemyPrefab, spawnPos, Quaternion.identity, this.transform);
                delay = spawnSpeed;
            }
        }
    }
}
