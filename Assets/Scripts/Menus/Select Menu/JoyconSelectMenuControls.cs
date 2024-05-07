using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyconSelectMenuControls : MonoBehaviour
{
	private List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public int jc_ind = 0;

	[SerializeField] bool canMoveUI;


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

			//if (!JoyconManagerSelectMenu.Instance.loading  && j.GetButtonDown(Joycon.Button.DPAD_DOWN))
			//{
			//	JoyconManagerSelectMenu.Instance.eventsystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
			//}

			if (!JoyconManagerSelectMenu.Instance.loading)
			{
                if (j.GetButtonDown(Joycon.Button.PLUS)|| j.GetButtonDown(Joycon.Button.MINUS))
                {
					JoyconManagerSelectMenu.Instance.eventsystem.currentSelectedGameObject.GetComponent<Button>().onClick.Invoke();
				}
				
			}
			



			#region Send Stick direction to joyconManager Select menu

			//	stick = j.GetStick();
			var curentButton = EventSystem.current;

			if (!JoyconManagerSelectMenu.Instance.loading)
			{
				var currentButtonSelectMinigameButtonScript = curentButton.currentSelectedGameObject.gameObject.GetComponent<SelectMinigameButton>();

                if (JoyconManagerSelectMenu.Instance.canMoveUI)
                {
					if (j.GetButtonDown(Joycon.Button.DPAD_UP))
					{
						JoyconManagerSelectMenu.Instance.canMoveUI = false;
						if (currentButtonSelectMinigameButtonScript._upNeighbourButton != null)
						{
							Debug.Log("Hej");
							curentButton.SetSelectedGameObject(currentButtonSelectMinigameButtonScript._upNeighbourButton.gameObject);
						}
					}

					if (j.GetButtonDown(Joycon.Button.DPAD_RIGHT))
					{
						JoyconManagerSelectMenu.Instance.canMoveUI = false;
						if (currentButtonSelectMinigameButtonScript._rightNeighbourButton != null)
						{
							Debug.Log("Hej");
							curentButton.SetSelectedGameObject(currentButtonSelectMinigameButtonScript._rightNeighbourButton.gameObject);
						}
					}

					if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
					{
						JoyconManagerSelectMenu.Instance.canMoveUI = false;
						if (currentButtonSelectMinigameButtonScript._downNeighbourButton != null)
						{
							Debug.Log("Hej");
							curentButton.SetSelectedGameObject(currentButtonSelectMinigameButtonScript._downNeighbourButton.gameObject);
						}
					}
					if (j.GetButtonDown(Joycon.Button.DPAD_LEFT))
					{
						JoyconManagerSelectMenu.Instance.canMoveUI = false;
						if (currentButtonSelectMinigameButtonScript._leftNeighbourButton != null)
						{
							Debug.Log("Hej");
							curentButton.SetSelectedGameObject(currentButtonSelectMinigameButtonScript._leftNeighbourButton.gameObject);
						}
					}
				}

				


				//Player must release button to press again
				if (j.GetButtonUp(Joycon.Button.DPAD_UP) || j.GetButtonUp(Joycon.Button.DPAD_RIGHT) || j.GetButtonUp(Joycon.Button.DPAD_DOWN) || j.GetButtonUp(Joycon.Button.DPAD_LEFT))
				{
					JoyconManagerSelectMenu.Instance.canMoveUI = true;

				}



			}


		


			#endregion

		}
    }
}
