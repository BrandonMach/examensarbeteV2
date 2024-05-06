using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{


    public void SelectMinigame()
    {
        SceneManager.LoadScene(1,LoadSceneMode.Single);
    }

    public void RandomMinigame()
    {
        SceneManager.LoadScene(Random.Range(2, 7), LoadSceneMode.Single);
    }
}
