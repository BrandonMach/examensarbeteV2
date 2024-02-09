using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StopwatchSpawnPlayer : MonoBehaviour
{
    [SerializeField] GameObject PlayerPrefab;

    int _playerCount = 0;


    [SerializeField] PlayerInputManager _playerINputManager;


    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        if(_playerCount < 4)
        {
            Instantiate(PlayerPrefab);
            _playerCount += 1;
        }
       
    }
}
