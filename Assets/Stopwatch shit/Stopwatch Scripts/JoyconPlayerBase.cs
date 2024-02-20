using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconPlayerBase : MonoBehaviour
{

	public List<Joycon> joycons;

	// Values made available via Unity
	public float[] stick;
	public Vector3 gyro;
	public Vector3 accel;
	public int jc_ind = 0;
	public Quaternion orientation;

	public void AssignPlayerNumber(int index)
	{
		jc_ind = index;

	}
}
