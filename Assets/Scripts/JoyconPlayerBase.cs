using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class JoyconPlayerBase : MonoBehaviour
{
	[Header("Joycon values")]
	public List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public Vector3 gyro;
	public Vector3 accel;
	public int jc_ind = 0;
	public Quaternion orientation;


	[SerializeField] protected TextMeshProUGUI _playerNameText;



	[Header("Player Ready Up")]
	public bool PlayerIsReady;
	public GameObject ReadyUpUI;

	public void AssignPlayerNumber(int index)
	{
		jc_ind = index;

	}

	// get the public Joycon array attached to the JoyconManager in scene
	public void Start()     
	{
		joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind + 1)
		{
			Destroy(gameObject);
		}

		SetUpPlayerName();
		
	}



    public virtual void ReadyUp(Joycon j)
    {
		if (j.GetButtonDown(Joycon.Button.MINUS) || j.GetButtonDown(Joycon.Button.PLUS)) // if player has pressed Ready up
		{
			PlayerIsReady = true;

			ReadyUpUI.gameObject.GetComponent<ReadyUpScript>().IsReady();
		}
	}

	void SetUpPlayerName()
    {
		this.name = "Player " + (1 + jc_ind);

		//Canvas ready up position
        switch (MinigamesManagers.Instance.CurrentMinigame)
        {
            case MinigamesManagers.MiniGameType.Stopwatch:
				_playerNameText.GetComponent<RectTransform>().position = new Vector3(250 + (500 * jc_ind), 150, this.transform.position.z);
				break;
            case MinigamesManagers.MiniGameType.Crossyroad:
				_playerNameText.GetComponent<RectTransform>().position = new Vector3(250 + (500 * jc_ind), 260, this.transform.position.z);
				break;
            case MinigamesManagers.MiniGameType.ShakeAndRun:
				_playerNameText.GetComponent<RectTransform>().position = new Vector3(250 + (500 * jc_ind), 160, this.transform.position.z);
				break;
            case MinigamesManagers.MiniGameType.Defence:
				_playerNameText.GetComponent<RectTransform>().position = new Vector3(250 + (500 * jc_ind), 260, this.transform.position.z);
				break;
            case MinigamesManagers.MiniGameType.Balance:
				_playerNameText.GetComponent<RectTransform>().position = new Vector3(250 + (500 * jc_ind), 260, this.transform.position.z);
				break;
            default:
                break;
        }

       

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
				_playerNameText.color = Color.green;
				break;
			default:
				break;
		}

		_playerNameText.text = name;
	}
}
