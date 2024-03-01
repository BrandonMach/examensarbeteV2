using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossEnemies : MonoBehaviour
{
    float dir, lastDir;
    // Start is called before the first frame update
    void Start()
    {
        dir = Random.Range(-2, 3);
        if (dir == 0)
        {
            dir = 1;
        }
        dir /= 4;
        lastDir = dir;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CrossyroadsManager.Instance.StartTheGame)
        {
            transform.Translate(new Vector3(dir, 0, 0));

            if (transform.position.x >= (24))
            {
                lastDir = dir;
                dir = -lastDir;
            }
            if (transform.position.x <= (-24))
            {
                lastDir = dir;
                dir = -lastDir;
            }
        }
    }
}
