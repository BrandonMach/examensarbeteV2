using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BalanceGameManager : MonoBehaviour
{

    #region Singelton
    static BalanceGameManager _instance;
    public static BalanceGameManager Instance
    {
        get => _instance;
        set => _instance = value;
    }
    #endregion


    [SerializeField] BalancePlayer[] _playerArray;
    public bool StartTheGame; //Only start the game when all player have joined
    public bool GameIsFinished;

    [SerializeField] GameObject[] _fallingItems;
    [SerializeField] int _fallingItemsCount;


    [SerializeField]  bool StartSpawn;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogWarning("More than one instance of BalanceGameManager found");
            return;
        }
        Instance = this;



    }

    void Start()
    {
        _playerArray = FindObjectsOfType<BalancePlayer>();
        StartSpawn = true;


    }

    // Update is called once per frame
    void Update()
    {

        if (_playerArray.All(go => go.PlayerIsReady == true ))
        {
            StartTheGame = true;
           
        }




        if (StartTheGame )
        {

            if (StartSpawn)
            {
                StartSpawn = false;
                StartCoroutine(SpawnFallingItems());
            }
            
        }

    }



    void SpawnFallingItem()
    {
        _fallingItemsCount++;

        Instantiate(_fallingItems[0], new Vector3(0, 4, 0), Quaternion.identity);

    }

    IEnumerator SpawnFallingItems()
    {
       while(true)
       {

            
           
            yield return new WaitForSeconds(3f);
        
            Instantiate(_fallingItems[Random.Range(0,_fallingItems.Length-1)], new Vector3(0, 4, 0), Quaternion.identity);


        }

       
    }
}
