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

	public TextMeshProUGUI PlayerTimeText;

	public float _stoppedTime = Mathf.Infinity;
	


	[SerializeField] GameObject _winnersCrown;
	public bool IsWinner;

	void Start()
	{
		base.Start();
		this.name = "Player " + (1 + jc_ind);

		PlayerTimeText.gameObject.SetActive(false);
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

		_winnersCrown.SetActive(IsWinner);
	}

	private void StopwatchGameControls(Joycon j)
    {

        if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
		{
			if (!PlayerIsReady)
			{
				Debug.Log("Rumble because not ready");
				j.SetRumble(160, 220, 0.6f, 150);
			}
			else if (StopwatchManager.Instance.StartTheGame && !_hasStoppedTime)
			{

				_hasStoppedTime = true;
				j.SetRumble(160, 220, 0.6f, 150);
				Debug.LogError("hej från " + this.name);
				StopwatchManager.Instance.JoyconPlayerStopTime(this);
			}

		}

		

		

	}

}
