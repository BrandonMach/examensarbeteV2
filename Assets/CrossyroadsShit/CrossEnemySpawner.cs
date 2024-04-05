using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossEnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    private float timer;
    public Vector3 rot, pos;
    public int rotationDegree;
    GameObject obstacles;
    public GameObject center;

    private void Start()
    {
        Spawn();
        rotationDegree = 5;
        rot = new Vector3(0,1,0);
        pos = obstacles.GetComponent<Renderer>().bounds.center;
        
    }

    private void FixedUpdate()
    {
        //obstacles.transform.RotateAround(position, rotation, rotationDegree * Time.deltaTime);
    }
    private void Spawn()
    {
        obstacles = Instantiate(Enemies[Random.Range(0, Enemies.Count)], this.transform);
    }
}
