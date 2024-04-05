using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPlayer : MonoBehaviour
{
    private CharacterController controller;
    public int score;
    public float speed = 5f;
    private Vector3 spawn;
    bool tookCoin, reachedStartAgain;
    GameObject coin;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        spawn = new Vector3(-11.4f, 0.03f, 15f);
    }
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (move!=Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        Physics.SyncTransforms();
        controller.Move(move * Time.deltaTime * speed);
        Debug.Log(spawn);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint") && !tookCoin)
        {
            tookCoin = true;
            coin = other.gameObject;
            coin.SetActive(false);
        }
        if (other.CompareTag("Enemy"))
        {
            if (tookCoin)
            {
                coin.SetActive(true);
            }
            tookCoin = false;
            transform.position = new Vector3(-11.4f, 0.03f, 15f);

        }
        if (other.CompareTag("Finish") && tookCoin)
        {
            tookCoin = false;
            score++;
            coin.SetActive(true);
        }
    }
}
