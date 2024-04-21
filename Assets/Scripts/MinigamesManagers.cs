using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigamesManagers : MonoBehaviour
{
    static MinigamesManagers instance;

    public static MinigamesManagers Instance
    {
        get { return instance; }
    }

   


    public enum MiniGameType
    {
        Stopwatch,
        Crossyroad,
        ShakeAndRun,
        Defence,
        Balance,
        Direction
    }
    public  MiniGameType CurrentMinigame;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;

        
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
