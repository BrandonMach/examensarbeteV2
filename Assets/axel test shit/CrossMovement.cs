using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossMovement : MonoBehaviour
{
    Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            transform.Translate(new Vector3(0, 0, 2));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            transform.position = spawn;
        }
    }
}
