using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyconMainMenucontrols : MonoBehaviour
{
	private List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public Vector3 gyro;
	public Vector3 accel;
	public int jc_ind = 0;
	public Quaternion orientation;

	
	void Start()
	{
		gyro = new Vector3(0, 0, 0);
		accel = new Vector3(0, 0, 0);
		// get the public Joycon array attached to the JoyconManager in scene
		joycons = JoyconManagerMainMenu.Instance.j;
		if (joycons.Count < jc_ind + 1)
		{
			Destroy(gameObject);
		}


		
	}

	// Update is called once per frame
	void FixedUpdate()
	{
        


		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];
			

			if (j.GetButtonDown(Joycon.Button.PLUS) || j.GetButtonDown(Joycon.Button.MINUS))
			{

				JoyconManagerMainMenu.Instance.eventsystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
			}

			stick = j.GetStick();

			Debug.Log(j.GetStick()[1]);
			if(j.GetStick()[1] > 0.5f)
            {
				Debug.Log("Stick Up");

				JoyconManagerMainMenu.Instance.ButtonGoUp();
			}

			if(j.GetStick()[1] < -0.5f )
            {
				
				Debug.Log("Stick ¨Down");

				JoyconManagerMainMenu.Instance.ButtonGoDown();
			}


			if (j.GetButtonDown(Joycon.Button.DPAD_UP))
			{
				JoyconManagerMainMenu.Instance.ButtonGoUp();
			}

			if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
				JoyconManagerMainMenu.Instance.ButtonGoDown();
			}
			
		}




	}


	
}
