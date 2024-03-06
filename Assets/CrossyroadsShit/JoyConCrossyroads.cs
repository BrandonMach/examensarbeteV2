using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoyConCrossyroads : JoyconPlayerBase
{

	[Header("Cross the road Game")]
	[SerializeField] TextMeshProUGUI _playerNameText;
	[SerializeField] GameObject playerInfoObj;
	public bool winner;
	Vector3 spawn;
	Vector3 direction;
	bool readyToMove, reachedChekpoint;

	new void Start()
	{
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
		_playerNameText.color = Color.red;
		//_playerNameText.GetComponent<RectTransform>().position = new Vector3(250 + (500 * jc_ind), 260, this.transform.position.z);

		switch (jc_ind)
		{
			case 0:
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
				spawn = new Vector3(-3, 1, -5);
				transform.position = spawn;
				break;
			case 1:
				this.name = "Player 2";
				_playerNameText.color = Color.blue;
				//_playerNameText.GetComponent<RectTransform>().position = new Vector3(750, 264, this.transform.position.z);
				playerInfoObj.GetComponent<RectTransform>().localPosition = new Vector3(playerInfoObj.transform.localPosition.x + (400 * jc_ind), playerInfoObj.transform.localPosition.y, playerInfoObj.transform.localPosition.z);
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
				spawn = new Vector3(-1, 1, -5);
				transform.position = spawn;
				break;
			case 2:
				this.name = "Player 3";
                _playerNameText.color = Color.yellow;
				playerInfoObj.GetComponent<RectTransform>().localPosition = new Vector3(playerInfoObj.transform.localPosition.x + (400 * jc_ind), playerInfoObj.transform.localPosition.y, playerInfoObj.transform.localPosition.z);
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
				spawn = new Vector3(1, 1, -5);
				transform.position = spawn;
				break;
			case 3:
				this.name = "Player 4";
                _playerNameText.color = Color.magenta;
				playerInfoObj.GetComponent<RectTransform>().localPosition = new Vector3(playerInfoObj.transform.localPosition.x + (400 * jc_ind), playerInfoObj.transform.localPosition.y, playerInfoObj.transform.localPosition.z);
                this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.magenta);
				spawn = new Vector3(3, 1, -5);
                transform.position = spawn;
				break;
			default:
				break;
		}

		_playerNameText.text = name;

		CrossyroadsManager.Instance.UpdatePlayerArray();

	}

	void Update()
	{
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];


			//if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			//{
			//	PlayerIsReady = true;

			//	ReadyUpUI.gameObject.GetComponent<ReadyUpScript>().IsReady();
			//}

			stick = j.GetStick();



			if (!PlayerIsReady && j.GetButton(Joycon.Button.DPAD_UP))
			{
				Debug.Log("Rumble because not ready");
				j.SetRumble(160, 320, 0.6f, 200);
			}
			if (j.GetButtonUp(Joycon.Button.DPAD_DOWN) && !readyToMove || j.GetButtonUp(Joycon.Button.DPAD_UP) && !readyToMove)
			{
				readyToMove = true;
			}
			if (CrossyroadsManager.Instance.StartTheGame && !CrossyroadsManager.Instance.GameOver && j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			{
				OnPlayerMove();
            }if (CrossyroadsManager.Instance.StartTheGame && !CrossyroadsManager.Instance.GameOver && j.GetButtonDown(Joycon.Button.DPAD_UP))
			{
				OnPlayerMoveReverse();
            }

			gameObject.transform.rotation = orientation;
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Respawn"))
		{
			transform.position = spawn;
		}
		if (other.CompareTag("Checkpoint"))
		{
			spawn = new Vector3(transform.position.x, 1, 25);
			reachedChekpoint = true;
            direction = new Vector3(0, 0, -2);
        }
		if (other.CompareTag("Finish") && reachedChekpoint)
		{
			winner = true;
			CrossyroadsManager.Instance.DeclareWinner(this.name);
			CrossyroadsManager.Instance.StartTheGame = false;
			CrossyroadsManager.Instance.GameOver = true;
		}
	}
	public void OnPlayerMove()
	{
		if (readyToMove)
		{
			readyToMove = false;
            transform.Translate(direction);
        }
		
	}
	public void OnPlayerMoveReverse()
	{
		if (!reachedChekpoint && this.transform.position.z + -direction.z <= spawn.z || reachedChekpoint && this.transform.position.z + -direction.z >= spawn.z)
		{
			return;
		}
		if (readyToMove)
		{
			readyToMove = false;
            transform.Translate(-direction);
        }
		
	}
}
