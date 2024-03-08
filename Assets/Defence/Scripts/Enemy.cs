using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(new Vector3(0, 2.5f, 0));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            //take damage
            Destroy(this.gameObject);
        }
    }
}
