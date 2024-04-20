using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalancePlayer : JoyconPlayerBase
{
    
    [SerializeField] GameObject _winnerCrown;
    [SerializeField] GameObject _mainBody;
    [SerializeField] List<FallingObjects> _recipeList;

    [SerializeField] Collider _inPotTriggerCollider;


    public BalancePlayer PartnerPlayer;

    List<FallingObjects> _listOfFoodInPot;
    bool addFood;


    [Header("Pot Boundary")]
    [SerializeField] float _maxRightBoundary;
    [SerializeField] float _maxLeftBoundary;


    void Start()
    {
        base.Start();
        _winnerCrown.SetActive(false);

        //Place player relative to the middle of the screen

        //Player 1 & 3
        //Player 2 & 4
        switch (jc_ind)
        {
            case 0:
                gameObject.transform.position = new Vector3(-2f, -1, -5);
                _mainBody.GetComponent<MeshRenderer>().material.color = Color.red;
                _maxLeftBoundary = -4;
                _maxRightBoundary = -0.7f;
                
                break;
            case 1:
                gameObject.transform.position = new Vector3(2f, -1, -5);
                _mainBody.GetComponent<MeshRenderer>().material.color = Color.blue;
                _maxLeftBoundary = 0.7f;
                _maxRightBoundary = 4;
                break;
            
            default:
                break;
        }

        addFood = true;
    }

    // Update is called once per frame
    void Update()
    {

        

        if (joycons.Count > 0)
        {
            Joycon j = joycons[jc_ind];
    

            base.ReadyUp(j);






            if (jc_ind == 2 || jc_ind == 3)
            {
                _mainBody.SetActive(false);

                if (j.GetButtonDown(Joycon.Button.DPAD_DOWN))
                {
                    j.SetRumble(160, 220, 0.6f, 150);


                   
                }

                var joyConAngles = j.GetVector().eulerAngles;

                if (joyConAngles.y < 190 && joyConAngles.y > 170)
                {
                    Debug.Log("Not moving");
                }
                else if (joyConAngles.y < 360 && joyConAngles.y > 190)
                {
                    Debug.Log("Left");
                    PartnerPlayer.transform.position += new Vector3(-0.02f, 0, 0);
                }
                else if (joyConAngles.y > 0 && joyConAngles.y < 170)
                {
                    Debug.Log("Right");
                    PartnerPlayer.transform.position += new Vector3(0.02f, 0, 0);
                }


            }
            else
            {
                if(transform.position.x >= _maxRightBoundary)
                {
                    transform.position = new Vector3(_maxRightBoundary, transform.position.y, transform.position.z);
                }
                if(transform.position.x <= _maxLeftBoundary)
                {
                    transform.position = new Vector3(_maxLeftBoundary, transform.position.y, transform.position.z);
                }
            }


            gameObject.transform.rotation = orientation;
            orientation = j.GetVector();
            gameObject.transform.Rotate(90, 0, 0, Space.World);


            //// Gyro values: x, y, z axis values (in radians per second)
            //gyro = j.GetGyro();

            //// Accel values:  x, y, z axis values (in Gs)
            //accel = -j.GetAccel();






        }

        
    }


    public void SetRecipe(List<FallingObjects> setRecipeList)
    {
        _recipeList = setRecipeList;
    }


    private void OnTriggerEnter(Collider other)
    {

        if(_listOfFoodInPot != null)
        {
            foreach (var foodsInlist in _listOfFoodInPot)
            {
                if (other.GetComponent<FallingObjects>() != foodsInlist)
                {
                    addFood = true;
                }
                else
                {
                    addFood = false;
                }
            }
        }
        

        if (addFood)
        {

            if (this.jc_ind == 0)
            {
                PartnerPlayer.RumbleController(joycons[PartnerPlayer.jc_ind]);
            }
            else if (this.jc_ind == 1)
            {
                PartnerPlayer.RumbleController(joycons[PartnerPlayer.jc_ind]);
            }

            //if (other.GetComponent<FallingObjects>() != null)
            //{
            //    _listOfFoodInPot.Add(other.GetComponent<FallingObjects>());
            //}
        }

    }

    private void RumbleController(Joycon j)
    {
        j.SetRumble(160, 220, 0.6f, 150);
    }

}
