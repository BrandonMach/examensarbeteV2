using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedSelector : MonoBehaviour
{
    [SerializeField]
    CrossEnemies[] enemies;
    float timer;
    bool startTimer;
    bool stopTimer;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GetComponentsInChildren<CrossEnemies>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startTimer)
        {
            timer += Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !startTimer)
        {
            startTimer = true;
            foreach(CrossEnemies enemy in enemies)
            {
                if (Random.Range(0.0f, 1.0f) < 0.5f)
                {
                    enemy.dir = enemy.originalDir / 2;
                }
                else
                {
                    enemy.dir = enemy.originalDir * 2;
                }
            }
            
        }
        if (timer > 8)
        {
            timer = 0;
            startTimer = false;
            foreach(CrossEnemies enemy in enemies)
            {
                enemy.dir = enemy.originalDir;
            }
        }
    }
}
