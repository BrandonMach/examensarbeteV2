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
    void Awake()
    {
        name = _minigameSO.MinigameName + " Button";
        _videoPlayer = GetComponent<VideoPlayer>();
        _rawImage = GetComponent<RawImage>();
       

        //Set up minigame button with Minigame Button So information
        _videoPlayer.clip = _minigameSO.VideoClip;
        _videoPlayer.targetTexture = (RenderTexture)_minigameSO.Texture;
        _minigameName.text = _minigameSO.MinigameName;
        _rawImage.texture = (RenderTexture)_minigameSO.Texture;

        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameClicked()
    {
        Debug.Log("Minigame has been clicked");
    }

    public void PlayVideo()
    {
        _thumbnail.SetActive(false);
        _videoPlayer.Play();
    }
    public void StopVideo()
    {

        _videoPlayer.isLooping = true;
        _videoPlayer.Stop();
        _thumbnail.SetActive(true);
    }
}
