using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StopwatchManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _timerHUDElement;

    [SerializeField] float _startTime;
    [SerializeField] float _stopwatch;
    private bool TimeRanOut;


    [Header("Microphone")]
    [SerializeField] AudioLoudnessDetection _audioDetector;
    public float loudnessSensibility = 100;
    public float threshold = 0.5f;
    [SerializeField] float loudness;

    [Header("Add more Time")]   
    [SerializeField] float _timeToAdd;
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _timeToAddPrefab;
    bool _addMoreTime;



    #region Player 1 
    [SerializeField] TextMeshProUGUI _player1TimerText;
    bool player1Stopped;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _stopwatch = _startTime;
        
    }

    // Update is called once per frame
    void Update()
    {

        
        _timerHUDElement.text = _stopwatch.ToString("0.0");


        if(!TimeRanOut){
            _stopwatch -= Time.deltaTime;
        }


        AddMoreTime();


        if (_stopwatch <= 0 && !TimeRanOut)
        {
            TimeRanOut = true;
            Debug.Log("Timer is done");

        }


       




        if (!TimeRanOut && !player1Stopped && Input.GetKeyDown(KeyCode.Space) )
        {

            player1Stopped = true;

            float playerStopTime = _stopwatch;
            //Debug.LogWarning(playerStopTime);

            _player1TimerText.text = playerStopTime.ToString("0.00");
            
            if (playerStopTime < 2 && playerStopTime > 0)
            {
                Debug.LogError("You Won");
            }
            else
            {
                Debug.LogError("You Lose Too Early!");
            }
        }
       

    }

    void AddMoreTime()
    {
        loudness = _audioDetector.GetLoudnessFromMicrophone() * loudnessSensibility;

        
        if(loudness < threshold)
        {
            loudness = 0;
            _addMoreTime = false;
        }

        if (loudness > threshold && !_addMoreTime)
        {
            _addMoreTime = true;
            _stopwatch += _timeToAdd;


            

            Debug.LogError("Add " + _timeToAdd + " seconds");
            GameObject tempAddTimePrefab = Instantiate(_timeToAddPrefab);
            tempAddTimePrefab.transform.parent = _canvas.transform;
            tempAddTimePrefab.transform.localScale = new Vector3(1, 1, 1);
            tempAddTimePrefab.transform.localPosition = new Vector3(80, 140, 0);
            //Destroy(tempAddTimePrefab, 1f);
        }
        
        
        
    }

    

    
}
