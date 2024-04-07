using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossEnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    private float timer;
    [SerializeField] bool interaction;
    public Vector3 rot, pos;
    [SerializeField] int rotation;
    [HideInInspector] public int rotationDegree;
    GameObject obstacles;
    public GameObject center;

    private void Start()
    {
        Spawn();
        rotationDegree = rotation;
        rot = new Vector3(0,1,0);
        pos = obstacles.GetComponent<Renderer>().bounds.center;
        interaction = false;
        
    }

    private void FixedUpdate()
    {
        //obstacles.transform.RotateAround(position, rotation, rotationDegree * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.R) && !interaction)
        {
            interaction = true; 
        }
        if (interaction)
        {
            rotationDegree = rotation * -1;
            timer += Time.deltaTime;
            if (timer >= 8)
            {
                timer = 0;
                interaction = false;
                rotationDegree = rotation;
            }
        }
    }
    private void Spawn()
    {
        obstacles = Instantiate(Enemies[Random.Range(0, Enemies.Count)], this.transform);
    }
}
