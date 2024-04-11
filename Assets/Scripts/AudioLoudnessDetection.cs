using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLoudnessDetection : MonoBehaviour
{
    public int SampleWindow = 64; //how big the sample clip is

    private AudioClip _microphoneClip;


    private void Start()
    {
        MicrophoneToAudioClip();
    }

    public void MicrophoneToAudioClip()
    {
        // Get name of first plugged in mic
        string microphoneName = Microphone.devices[0];
        _microphoneClip = Microphone.Start(microphoneName, true,60, AudioSettings.outputSampleRate); //True = loop
    }


    public float GetLoudnessFromMicrophone()
    {
        

        return GetLoudnessFromAudiClip(Microphone.GetPosition(Microphone.devices[0]), _microphoneClip); 
    }




    public float GetLoudnessFromAudiClip(int clipPosition, AudioClip clip)
    {
        int startPosition = clipPosition - SampleWindow;


        if (startPosition < 0)
        {
            startPosition = 0;
        }

        float[] waveData = new float[SampleWindow];
        clip.GetData(waveData, startPosition);

        //Compute loudness, get absoulte min value

        float totalLoudness = 0;

        for (int i = 0; i < SampleWindow; i++)
        {
            totalLoudness += Mathf.Abs(waveData[i]);
            // 0 = no sound
        }

        return totalLoudness / SampleWindow; //returns min value of array
    }
}
