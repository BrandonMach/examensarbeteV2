using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rb;
    void Start()
    {
       _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.velocity = new Vector3(0, -4.91f, 0);
    }
}
