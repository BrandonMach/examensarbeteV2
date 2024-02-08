using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithSound : MonoBehaviour
{
    [SerializeField] AudioSource _soundSource;
    [SerializeField] Vector3 _minScale;
    [SerializeField] Vector3 _maxScale;
    [SerializeField] AudioLoudnessDetection _audioDetector;

    public float loudnessSensibility = 100;
    public float threshold = 0.1f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float loudness = _audioDetector.GetLoudnessFromAudiClip(_soundSource.timeSamples, _soundSource.clip) * loudnessSensibility;

        if(loudness < threshold)
        {
            loudness = 0;
        }
        transform.localScale = Vector3.Lerp(_minScale, _maxScale, loudness);

        //Gör att med tid läggs till i stopwatch minigame
    }
}
