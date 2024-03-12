using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyconSelectMenuControls : MonoBehaviour
{
	private List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public int jc_ind = 0;


	void Start()
	{
		// get the public Joycon array attached to the JoyconManager in scene
		joycons = JoyconManagerSelectMenu.Instance.j;
		if (joycons.Count < jc_ind + 1)
		{
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{

		// make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
		{
			Joycon j = joycons[jc_ind];

			if (!JoyconManagerSelectMenu.Instance.loading  && j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			{
				JoyconManagerSelectMenu.Instance.eventsystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
			}



			#region Send Stick direction to joyconManager Select menu

			stick = j.GetStick();

			//Rmust go back to neutral before you can go next
            if (j.GetStick()[0] == 0 && j.GetStick()[1] == 0 && !JoyconManagerSelectMenu.Instance.canMoveUI)
            {
                JoyconManagerSelectMenu.Instance.canMoveUI = true;
            }

            if (JoyconManagerSelectMenu.Instance.canMoveUI  && j.GetStick()[1] > 0.8f)
			{
				JoyconManagerSelectMenu.Instance.MoveInUI(JoyconManagerSelectMenu.MoveDirection.up);
			}

			if (JoyconManagerSelectMenu.Instance.canMoveUI  && j.GetStick()[1] < -0.8f)
			{
				JoyconManagerSelectMenu.Instance.MoveInUI(JoyconManagerSelectMenu.MoveDirection.down);
			}

			if (JoyconManagerSelectMenu.Instance.canMoveUI  && j.GetStick()[0] < -0.8f)
			{
				JoyconManagerSelectMenu.Instance.MoveInUI(JoyconManagerSelectMenu.MoveDirection.left);
			}

			if (JoyconManagerSelectMenu.Instance.canMoveUI  && j.GetStick()[0] > 0.8f)
			{
				JoyconManagerSelectMenu.Instance.MoveInUI(JoyconManagerSelectMenu.MoveDirection.right);
			}

			#endregion

		}
    }
}
