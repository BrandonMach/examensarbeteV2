using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem.XR;

public class JoyConCrossyroads : JoyconPlayerBase
{

	[Header("Cross the road Game")]
	[SerializeField] GameObject  playerIndicator;
	[SerializeField] TextMeshProUGUI scoreValue;
    public int score;
    public bool winner;
	Vector3 spawn;
	Vector3 direction;
	Vector3 joyConAngles;
	float speed;
	bool readyToMove, reachedChekpoint;
	CharacterController controller;
	float horizontalValue, verticalValue;
    bool tookCoin, reachedStartAgain;
    GameObject coin;

    new void Start()
	{
		score = 0;
		speed = 6;
		controller = GetComponent<CharacterController>();
		gyro = new Vector3(0, 0, 0);
		accel = new Vector3(0, 0, 0);
		// get the public Joycon array attached to the JoyconManager in scene
		joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind + 1)
		{
			Destroy(gameObject);
		}

		direction = new Vector3(0,0,2);

		this.name = "Player " + (1 + jc_ind);

		base.Start();

		playerIndicator.GetComponent<Renderer>().material.SetColor("_Color", _playerNameText.color);

        switch (jc_ind)
        {
            case 0:
                spawn = new Vector3(-11f, 0.03f, 15f);
                break;
            case 1:
                spawn = new Vector3(-4f, 0.03f, 15f);
                break;
            case 2:           
                spawn = new Vector3(4f, 0.03f, 15f);
                break;
            case 3:    
                spawn = new Vector3(11, 1, -5);
                break;
            default:
                break;
        }
        transform.position = spawn;


        CrossyroadsManager.Instance.UpdatePlayerArray();
		UpdateScoreText();

	}

	void FixedUpdate()
	{
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			
			Joycon j = joycons[jc_ind];

			stick = j.GetStick();

            if (!CrossyroadsManager.Instance.StartTheGame)
            {
                ReadyUp(j);
                if (!PlayerIsReady && j.GetButton(Joycon.Button.DPAD_UP))
                {
                    Debug.Log("Rumble because not ready");
                    j.SetRumble(160, 320, 0.6f, 200);
                }
            }
			else
			{
                gameObject.transform.rotation = orientation;

                CalculateAngles(j);

                Vector3 move = new Vector3(horizontalValue, 0, verticalValue);
                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                }
                Physics.SyncTransforms();
                controller.Move(move * Time.deltaTime * speed);
            }

        }
	}
	public void CalculateAngles(Joycon j)
	{
        joyConAngles = j.GetVector().eulerAngles;

		if ((joyConAngles.x < 5 && joyConAngles.x > 0) || (joyConAngles.x < 360 && joyConAngles.x > 355))
		{
			verticalValue = 0f;
		}
		else if ((joyConAngles.x > 5 && joyConAngles.x < 180))
		{
			verticalValue = -1f;
		}
		else if (joyConAngles.x < 355 && joyConAngles.x > 180)
		{
			verticalValue = 1f;
		}

		if (joyConAngles.y < 190 && joyConAngles.y > 170)
		{
			horizontalValue = 0f;
		}
		else if (joyConAngles.y < 360 && joyConAngles.y > 190)
		{
			horizontalValue = -1f;
		}
		else if (joyConAngles.y > 0 && joyConAngles.y < 170)
		{
			horizontalValue = 1f;
		}

		Debug.Log(joyConAngles);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint") && !tookCoin)
        {
            tookCoin = true;
            coin = other.gameObject;
            coin.SetActive(false);
        }
        if (other.CompareTag("Enemy"))
        {
            if (tookCoin)
            {
                coin.SetActive(true);
            }
            tookCoin = false;
            transform.position = spawn;

        }
        if (other.CompareTag("Finish") && tookCoin)
        {
            tookCoin = false;
            score++;
			UpdateScoreText();
            coin.SetActive(true);
        }
    }
    private void UpdateScoreText()
	{
		scoreValue.SetText(score.ToString());
	}

}
