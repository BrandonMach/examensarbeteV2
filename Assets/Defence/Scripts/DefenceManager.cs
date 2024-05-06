using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefenceManager : MonoBehaviour
{
    #region Singelton
    static DefenceManager _instance;
    public static DefenceManager Instance { get => _instance; set => _instance = value; }
    #endregion

    [SerializeField] DefenceJoyconPlayer[] _playerArray;

    public int lives;
    public float timer, interactTimer;
    public bool gameOver, startGame, interaction;
    public TMPro.TextMeshProUGUI timerTxt;
    public TMPro.TextMeshProUGUI livestxt;
    public GameObject gameOverPanel, VictoryPanel;
    public GameObject core;
    float rotationValue;
    [SerializeField] GameObject _controls;

    [Header("Microphone")]
    [SerializeField] AudioLoudnessDetection _audioDetector;
    public float loudnessSensibility = 100;
    public float threshold = 0.5f;
    public float miniTreshhold = 0.5f;
    public float loudnessDivider = 120;
    public bool miniTreshholdMet;
    public float loudness;

    [SerializeField] bool isLoud;
    [SerializeField] SpriteRenderer _audioSpriteRenderer;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogWarning("More than one instance of GameManager found");
            return;
        }
        Instance = this;

    }

  

    // Start is called before the first frame update
    public void TakeDamage()
    {
        lives--;
    }

    public void UpdatePlayerArray()
    {
        _playerArray = FindObjectsOfType<DefenceJoyconPlayer>();
        
    }

    // Update is called once per frame
  

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        _controls.SetActive(!startGame);
        CheckLoudness();
        
        foreach (var players in _playerArray)
        {
            players.transform.parent.SetParent(core.transform);
        }



        if (_playerArray.Length >= 2 && _playerArray.All(go => go.PlayerIsReady == true)) //Start the game once all player are ready 
        {
            startGame = true;
            foreach (var players in _playerArray)
            {
                StartCoroutine(players.ReadyUpUI.GetComponent<ReadyUpScript>().AllPlayersReady()); //Should be fade text instead of set active false

            }
        }
        if (startGame)
        {
            if (timer > 0 && lives > 0)
            {
                timer -= Time.deltaTime;
                if (Input.GetKeyDown(KeyCode.R))
                {
                    interaction = true;
                }
                if (loudness > miniTreshhold && !interaction)
                {
                    rotationValue += (loudness / 120);
                    core.transform.rotation = Quaternion.Euler(0, rotationValue, 0);
                }
                if (AudienceIsLoud() && !interaction)
                {
                    rotationValue *= -1;
                    interaction = true;
                }
                if (interaction)
                {
                    interactTimer += Time.deltaTime;
                    rotationValue += -0.5f;
                    core.transform.rotation = Quaternion.Euler(0, rotationValue, 0);

                    //Rotate the Core for 6 seconds
                    if (interactTimer > 3)
                    {
                        interactTimer = 0;
                        interaction = false;
                    }
                }
            }
            else
            {
                EndGame();
            }
            timerTxt.SetText(timer.ToString("#.00"));
            livestxt.SetText(lives.ToString());
        }
    }

    private void EndGame()
    {
        //Anything that should happen when the game ends
        if (lives <= 0)
        {
            gameOverPanel.SetActive(true);
        }
        else if(timer <= 0)
        {
            VictoryPanel.SetActive(true);
        }
        gameOver = true;
    }

    void CheckLoudness()
    {
        // Multiply so that loudness easier to work with
        loudness = _audioDetector.GetLoudnessFromMicrophone() * loudnessSensibility * 5;

        if ((loudness * 2f) <= 80)
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, loudness * 2f);
        }
        else
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, 80);
        }

        //if loudness is negative set to 0 as mininum and prevent more time to be added until it has atleast reseted
        if (loudness < threshold)
        {
            if (loudness > miniTreshhold)
            {
                miniTreshholdMet = true;
            }
            else
            {
                miniTreshholdMet = false;
            }
            //loudness = 0;
            isLoud = false;
        }
        else
        {

            isLoud = true;
        }


    }

    bool AudienceIsLoud()
    {
        // Multiply so that loudness easier to work with
        loudness = _audioDetector.GetLoudnessFromMicrophone() * loudnessSensibility * 5;

        //if loudness is negative set to 0 as mininum and prevent more time to be added until it has atleast reseted

        if ((loudness) <= threshold)
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, loudness);
        }
        else
        {
            _audioSpriteRenderer.size = new Vector2(_audioSpriteRenderer.size.x, 80);
        }


        if (loudness < threshold)
        {
            loudness = 0;
            isLoud = false;
            return false;
        }
        else
        {

            if (loudness > miniTreshhold)
            {
                isLoud = true;
                return true;
            }
            return false;

        }
    }
}
