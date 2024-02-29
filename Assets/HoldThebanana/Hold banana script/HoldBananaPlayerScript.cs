using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HoldBananaPlayerScript : JoyconPlayerBase
{

	[Header("Hold Banana Player")]
	public int yo;


	void Start()
	{
		base.Start();


		gyro = new Vector3(0, 0, 0);
		accel = new Vector3(0, 0, 0);


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
		

		}
	}
}
