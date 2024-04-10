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

    [SerializeField] FallingObjects[] _fallingItems;
    [SerializeField] int _fallingItemsCount;


    [SerializeField]  bool StartSpawn;
    [SerializeField] float _balanceTimer;

    bool generatePlayerRecipe;

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

            if (!generatePlayerRecipe)
            {
                GeneratePlayerRecipes();
            }
            



        }
        else
        {
            StartTheGame = false;
        }




        if (StartTheGame )
        {
            //Start Coroutine only once
            if (StartSpawn)
            {
                StartSpawn = false;
                StartCoroutine(SpawnFallingItems());
            }
            
        }

    }


    

    IEnumerator SpawnFallingItems()
    {
       while(true)
       {

            
           
            yield return new WaitForSeconds(3f);
        
            Instantiate(_fallingItems[Random.Range(0, _fallingItems.Length - 1)], new Vector3(-1, 4, -5), Quaternion.identity);
            Instantiate(_fallingItems[Random.Range(0, _fallingItems.Length - 1)], new Vector3(1, 4, -5), Quaternion.identity);


       }

       
    }


    private void GeneratePlayerRecipes()
    {
        generatePlayerRecipe = true;

        List<FallingObjects> recipeList = new List<FallingObjects>();

        for (int i = 0; i < 3; i++)
        {
            recipeList.Add(_fallingItems[Random.Range(0, _fallingItems.Length - 1)]);
        }

        foreach (var players in _playerArray)
        {
            players.SetRecipe(recipeList);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);

    }
}
