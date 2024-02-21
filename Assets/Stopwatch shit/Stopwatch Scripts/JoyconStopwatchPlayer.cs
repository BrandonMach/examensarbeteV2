using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JoyconStopwatchPlayer : JoyconPlayerBase
{

	[Header("Stopwatch Game")]
	public bool StopTime = false;
	bool _hasStoppedTime;
	public bool PlayerHasStoppedTime { get => _hasStoppedTime; }
	[SerializeField] TextMeshProUGUI _playerNameText;

	[SerializeField] TextMeshProUGUI _playerTimeText;

	float _stoppedTime = Mathf.Infinity;
	public float GetStoppedTime { get => _stoppedTime; set => _stoppedTime = value; }

	


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
		_playerNameText.color = Color.red;
		_playerNameText.GetComponent<RectTransform>().position = new Vector3(250 + (500 * jc_ind), 260, this.transform.position.z);

		switch (jc_ind)
		{
			case 0:
				_playerNameText.color = Color.red;
				break;
			case 1:
				_playerNameText.color = Color.blue;
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

	// Update is called once per frame
	void Update()
	{
		
		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];

			base.ReadyUp(j);
			//If game type så kan vi återanvända skriptet
			StopwatchGameControls(j);

		}
	}

	private void StopwatchGameControls(Joycon j)
    {
		//if (j.GetButtonDown(Joycon.Button.MINUS) || j.GetButtonDown(Joycon.Button.PLUS))
		//{
		//	PlayerIsReady = true;

		//	ReadyUpUI.gameObject.GetComponent<ReadyUpScript>().IsReady();
		//}

		



		if (!PlayerIsReady && j.GetButtonDown(Joycon.Button.DPAD_DOWN))
		{
			Debug.Log("Rumble because not ready");
			j.SetRumble(160, 320, 0.6f, 200);
		}
		else if (StopwatchManager.Instance.StartTheGame && !_hasStoppedTime && j.GetButtonDown(Joycon.Button.DPAD_DOWN))
		{
			_hasStoppedTime = true;
			Debug.LogError("hej från " + this.name);
			StopwatchManager.Instance.JoyconPlayerStopTime(_playerTimeText, this);
		}

		

	}

}
