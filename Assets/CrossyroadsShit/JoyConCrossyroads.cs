using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoyConCrossyroads : MonoBehaviour
{
	private List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public Vector3 gyro;
	public Vector3 accel;
	public int jc_ind = 0;
	public Quaternion orientation;

	[Header("Cross the road Game")]
	[SerializeField] TextMeshProUGUI _playerNameText;
	public bool PlayerIsReady;
	public bool winner;
	public GameObject ReadyUpUI;
	Vector3 spawn;

	void Start()
	{
		gyro = new Vector3(0, 0, 0);
		accel = new Vector3(0, 0, 0);
		// get the public Joycon array attached to the JoyconManager in scene
		joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind + 1)
		{
			Destroy(gameObject);
		}

		this.name = "Player " + (1 + jc_ind);
		//_playerNameText.color = Color.red;
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
				//_playerNameText.color = Color.blue;
				//_playerNameText.GetComponent<RectTransform>().position = new Vector3(750, 264, this.transform.position.z);
				this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
				spawn = new Vector3(-1, 1, -5);
				transform.position = spawn;
				break;
			case 2:
				_playerNameText.color = Color.yellow;
				break;
			case 3:
				_playerNameText.color = Color.yellow;
				break;
			default:
				break;
		}

		_playerNameText.text = name;



	}

	void Update()
	{
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];


			if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			{
				PlayerIsReady = true;

				//ReadyUpUI.gameObject.GetComponent<ReadyUpScript>().IsReady();
			}

			stick = j.GetStick();



			if (!PlayerIsReady && j.GetButton(Joycon.Button.DPAD_UP))
			{
				Debug.Log("Rumble because not ready");
				j.SetRumble(160, 320, 0.6f, 200);
			}
			else if (CrossyroadsManager.Instance.StartTheGame && !CrossyroadsManager.Instance.GameOver && j.GetButtonDown(Joycon.Button.DPAD_UP))
			{
				
				Debug.LogError("hej från " + this.name);
			}


			gameObject.transform.rotation = orientation;
		}
	}
	public void AssignPlayerNumber(int index)
	{
		jc_ind = index;

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Respawn"))
		{
			transform.position = spawn;
		}
		if (other.CompareTag("Finish"))
		{
			winner = true;
			CrossyroadsManager.Instance.DeclareWinner(this.name);
			CrossyroadsManager.Instance.StartTheGame = false;
			CrossyroadsManager.Instance.GameOver = true;
		}
	}
	public void OnPlayerMove()
	{
		transform.Translate(new Vector3(0, 0, 2));
	}
}
