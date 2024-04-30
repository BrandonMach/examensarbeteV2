using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossEnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();
    private float timer;
    [SerializeField] bool interaction;
    public Vector3 rot, pos;
    [SerializeField] int rotation;
    [HideInInspector] public int rotationDegree;
    GameObject obstacles;
    public GameObject center;
    bool reversed;
    public bool miniTreshholdMet;
    [SerializeField]
    float rotationDelay;

    [Header("Microphone")]
    [SerializeField] AudioLoudnessDetection _audioDetector;
    public float loudnessSensibility = 100;
    public float threshold = 0.5f;
    public float miniTreshhold = 0.5f;
    public float loudness;

    [SerializeField] bool isLoud;
    [SerializeField] SpriteRenderer _audioSpriteRenderer;

    private void Start()
    {
        Spawn();
        rotationDegree = rotation;
        rot = new Vector3(0,1,0);
        pos = obstacles.GetComponent<Renderer>().bounds.center;
        interaction = false;
        
    }

    private void Update()
    {
        CheckLoudness();
    }
    private void FixedUpdate()
    {
        //obstacles.transform.RotateAround(position, rotation, rotationDegree * Time.deltaTime);
        if (loudness > threshold && interaction == false)
        {
            interaction = true;
        }

        if (interaction)
        {
            if (!reversed)
            {
                reversed = true;
                rotationDegree *= -1;
            }
            timer += Time.deltaTime;
            if (timer >= rotationDelay)
            {
                reversed = false;
                interaction = false;
                timer = 0;
            }
        }
    }
    private void Spawn()
    {
        obstacles = Instantiate(Enemies[Random.Range(0, Enemies.Count)], this.transform);
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
            loudness = 0;
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
            isLoud = true;
            return true;
        }
    }
}
