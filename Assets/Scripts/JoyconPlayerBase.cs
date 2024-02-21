using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ReadyUpScript))]
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

	[Header("Player Ready Up")]
	public bool PlayerIsReady;
	public GameObject ReadyUpUI;

	public void AssignPlayerNumber(int index)
	{
		jc_ind = index;

	}



    public virtual void ReadyUp(Joycon j)
    {
		if (j.GetButtonDown(Joycon.Button.MINUS) || j.GetButtonDown(Joycon.Button.PLUS))
		{
			PlayerIsReady = true;

			ReadyUpUI.gameObject.GetComponent<ReadyUpScript>().IsReady();
		}
	}
}
