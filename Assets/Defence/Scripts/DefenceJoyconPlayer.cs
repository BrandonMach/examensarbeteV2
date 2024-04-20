using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class DefenceJoyconPlayer : JoyconPlayerBase
{
    
    [SerializeField] GameObject playerInfoObj;
    // Start is called before the first frame update
    GameObject core;
    [SerializeField] GameObject playerRepresentation;
    new public void Start()
    {
        gyro = new Vector3 (0, 0, 0);
        accel = new Vector3 (0, 0, 0);
        joycons = JoyconManager.Instance.j;
        if (joycons.Count < jc_ind + 1)
        {
            Destroy(gameObject);
        }
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(0, 2.5f, 0);
        transform.parent = sphere.transform;



        playerRepresentation.transform.SetParent(sphere.transform);

        DefenceManager.Instance.UpdatePlayerArray();

        this.name = "Player " + (1 + jc_ind);

        switch (jc_ind)
        {
            case 0:
                this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                break;
            case 1:
                this.name = "Player 2";
                this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
                break;
            case 2:
                this.name = "Player 3";
                this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.yellow);
                break;
            case 3:
                this.name = "Player 4";
                this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                break;
            default:
                break;
        }

        base.Start();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        





        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
            if (!DefenceManager.Instance.startGame)
            {
                updateRotation(j);
                ReadyUp(j);
            }
            ReadyUp(j);
            if (DefenceManager.Instance.startGame)
            {
                

                gyro = j.GetGyro();

                accel = j.GetAccel();

                updateRotation(j);
            }
        }

        
    }

    private void updateRotation(Joycon j)
    {
        orientation = j.GetVector();
        var angles = orientation.eulerAngles;
        if (joycons.Count == 2)
        {
            if (angles.y > 270) { angles.y = 270; }
            else if (angles.y < 90) { angles.y = 90; }
            if (angles.y <= 270 && angles.y >= 90)
            {
               
               // transform.parent.rotation = Quaternion.Euler(0, angles.y + (180 * jc_ind), 0);
                transform.parent.localRotation = Quaternion.Euler(0, angles.y + (180 * jc_ind), 0);
            }
        }
        if (joycons.Count == 3)
        {
            if (angles.y > 240) { angles.y = 240; }
            else if (angles.y < 160) { angles.y = 160; }
            if (angles.y <= 240 && angles.y >= 160)
            {
                transform.parent.localRotation = Quaternion.Euler(0, angles.y + (135 * jc_ind), 0);
            }
        }
        if (joycons.Count == 4)
        {
            if (angles.y > 225) { angles.y = 225; }
            else if (angles.y < 135) { angles.y = 135; }
            if (angles.y <= 225 && angles.y >= 135)
            {
                transform.parent.localRotation = Quaternion.Euler(0, angles.y + (90 * jc_ind), 0);
            }
        }
    }
}
