using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Start is called before the first frame update

    //kan ta bort dett askriptet och lägga in metoden i JoyconManager Main menu
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMinigame()
    {
        SceneManager.LoadScene(1);
    }
}
