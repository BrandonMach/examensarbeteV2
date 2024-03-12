using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(PlayerInput))]


public class CrossyroadsPlayer : JoyconPlayerBase
{
   
    public bool winner;

    Vector3 spawn;

    void Start()
    {
        if (GetComponent<PlayerInput>().playerIndex == 0)
        {

            this.name = "Player 1";
            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            spawn = new Vector3(-3, 1, -5);
            transform.position = spawn;

        }

        if (GetComponent<PlayerInput>().playerIndex == 1)
        {

            this.name = "Player 2";

            this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            spawn = new Vector3(-1, 1, -5);
            transform.position = spawn;
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            transform.position = spawn;
        }
        if (other.CompareTag("Finish"))
        {
            winner = true;
            CrossyroadsManager.Instance.DeclareWinner(this.name);
            CrossyroadsManager.Instance.StartTheGame = false;
            CrossyroadsManager.Instance.GameOver = true;
        }
    }

    public void OnPlayerIsReady(InputAction.CallbackContext context)
    {
        PlayerIsReady = true;
    }

    public void OnPlayerMove(InputAction.CallbackContext context)
    {
        if (CrossyroadsManager.Instance.StartTheGame && !CrossyroadsManager.Instance.GameOver)
        {
            if (context.performed)
            {
                transform.Translate(new Vector3(0, 0, 2));
            }
           
        }
        
    }
}
