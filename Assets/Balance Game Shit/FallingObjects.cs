using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody _rb;

    public enum FoodType
    {
        Apple,
        Banana,
        Fish,
        Avocado,
        Carrot,
        Garlic,
        Mushroom,
        Pear,
        Pepper,
        Pumpkin,
        Steak,
        Tomato,
        Watermelon

    }

    public FoodType foodType;

    void Start()
    {
       _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //_rb.velocity = new Vector3(0, -4.91f, 0);
    }
}
