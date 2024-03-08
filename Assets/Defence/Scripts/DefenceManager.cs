using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : MonoBehaviour
{
    #region Singelton
    static DefenceManager _instance;
    public static DefenceManager Instance { get => _instance; set => _instance = value; }
    #endregion

    public int lives;
    public float timer;
    public bool gameOver;
    public TMPro.TextMeshProUGUI timerTxt;
    public TMPro.TextMeshProUGUI livestxt;
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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            EndGame();
        }
        timerTxt.SetText(timer.ToString("#.00"));
        //livestxt.SetText(lives.ToString("#.00"));
    }

    private void EndGame()
    {
        //Anything that should happen when the game ends
        gameOver = true;
    }
}
