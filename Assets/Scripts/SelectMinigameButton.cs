using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectMinigameButton : MonoBehaviour
{
    
    [SerializeField] MinigameButtonSO _minigameSO;

    [SerializeField] GameObject _thumbnail;

    [SerializeField] VideoPlayer _videoPlayer;
    [SerializeField] TextMeshProUGUI _minigameName;
    [SerializeField] RawImage _rawImage;
    [SerializeField] GameObject BorderGO;

    [SerializeField] public Button _upNeighbourButton;
    [SerializeField] public Button _downNeighbourButton;
    [SerializeField] public Button _leftNeighbourButton;
    [SerializeField] public Button _rightNeighbourButton;

    
    void Awake()
    {
        BorderGO.SetActive(false);
        name = _minigameSO.MinigameName + " Button";
        _videoPlayer = GetComponent<VideoPlayer>();
        _rawImage = GetComponent<RawImage>();
       

        //Set up minigame button with Minigame Button So information
        _videoPlayer.clip = _minigameSO.VideoClip;
        _videoPlayer.targetTexture = (RenderTexture)_minigameSO.Texture;
        _minigameName.text = _minigameSO.MinigameName;
        _rawImage.texture = (RenderTexture)_minigameSO.Texture;

        _thumbnail.GetComponent<Image>().sprite = _minigameSO.Thumbnail;



    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameClicked()
    {
        Debug.Log("Minigame has been clicked");
       // SceneManager.LoadScene(_minigameSO.MinigameName);
        SceneManager.LoadScene(_minigameSO.SceneIndex, LoadSceneMode.Single);
        
    }

    public void PlayVideo()
    {
        Debug.LogWarning("Play video");
        BorderGO.SetActive(true);
        _thumbnail.SetActive(false);
        _videoPlayer.Play();
        _minigameName.fontSize += 5;
    }
    public void StopVideo()
    {
        BorderGO.SetActive(false);
        _videoPlayer.isLooping = true;
        _videoPlayer.Stop();
        _thumbnail.SetActive(true);
        _minigameName.fontSize -= 5;
    }



}
