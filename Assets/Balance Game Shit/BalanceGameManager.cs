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

    [SerializeField] bool _enoughPlayers;
    [SerializeField] BalancePlayer[] _playerArray;
    public BalancePlayer[] GetPlayerArray { get { return _playerArray; } }

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


        foreach (BalancePlayer player in _playerArray)
        {
            if(player.jc_ind == 0)
            {
                foreach (BalancePlayer otherPlayer in _playerArray)
                {
                    if(player != otherPlayer)
                    {
                        if(otherPlayer.jc_ind == 2)
                        {
                            player.PartnerPlayer = otherPlayer;
                            otherPlayer.PartnerPlayer =player;
                        }
                    }
                }
            }
            else if (player.jc_ind == 1)
            {
                foreach (BalancePlayer otherPlayer in _playerArray)
                {
                    if (player != otherPlayer)
                    {
                        if (otherPlayer.jc_ind == 3)
                        {
                            player.PartnerPlayer = otherPlayer;
                            otherPlayer.PartnerPlayer = player;
                        }
                    }
                }
            }
        }


        StartSpawn = true;


    }

    // Update is called once per frame
    void Update()
    {

        if(_playerArray.Length == 4)
        {
            _enoughPlayers = true;
        }


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

            var _spawnFallingFood = _fallingItems[Random.Range(0, _fallingItems.Length - 1)];

            Instantiate(_spawnFallingFood, new Vector3(Random.Range(-4,-0.7f), 5, -5), _spawnFallingFood.transform.rotation);
            Instantiate(_spawnFallingFood, new Vector3(Random.Range(4, 0.7f), 5, -5), _spawnFallingFood.transform.rotation);


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
