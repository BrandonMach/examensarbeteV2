using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(menuName ="Minigame Select Button")]
public class MinigameButtonSO : ScriptableObject
{
    // Start is called before the first frame update
    public string MinigameName;
    public int SceneIndex;
    public Texture Texture;
    public VideoClip VideoClip;
    public Sprite Thumbnail;
    

}
