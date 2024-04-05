using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossEnemies : MonoBehaviour
{
    float lastDir;
    public float dir, originalDir;
    GameObject parent1;
    CrossEnemySpawner parent;
    // Start is called before the first frame update
    void Start()
    {
        parent1 = transform.parent.parent.gameObject;
        parent = parent1.GetComponent<CrossEnemySpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(parent.center.transform.position, parent.rot, parent.rotationDegree * Time.deltaTime);
    }
}
