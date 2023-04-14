using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
    Rigidbody rb;

    bool hasLanded;
    bool thrown;

    Vector3 initPosition;
    Quaternion initRotation;

    public DiceSide[] diceSides;

    public int diceValue;

    private AudioSource diceAudioSource;
    public AudioClip[] diceHitSounds;


    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        diceAudioSource = GetComponent<AudioSource>();
        initPosition = transform.position;
        initRotation = transform.rotation;

    }

    void Reset()
    {
        transform.position = initPosition;
        thrown = false;
        hasLanded = false;
    }

    public void RollDice()
    {
        Reset();
        if (!thrown && !hasLanded)
        {
            thrown = true;
            rb.AddTorque(Random.Range(0, 900), Random.Range(0, 900), Random.Range(0, 900));
        }
        else if (thrown && hasLanded)
        {
            //reset the dice position
            Reset();
        }
    }
    public void SideValueCheck()
    {
        diceValue = 0;

        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                //return diceValue;

                //send results to GameManager script
                //GameManager.instance.RollDice(diceValue);
            }

            /*if(side.OnGround()) **This code is for changing the values of plain side with respect to the situation**
            {
                if(side.sideValue == 0 && side.sideValue1 ==0)**1st -This function will work if and only if plain side comes on both the dice.**
                {
                    diceValue = 12;
                   
                }
                else  **Plain side value will be considered as 0 by default**
                {
                    diceValue = side.sideValue + side.sideValue1;
                    
                }
            }*/

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.IsSleeping() && !hasLanded && thrown)
        {
            hasLanded = true;
            //side value check
            SideValueCheck();

        }
        else if (rb.IsSleeping() && hasLanded/* && diceValue ==0*/)//If dice didnt drawn properly it will re-roll
        {
            //roll the dice again
            RollDice();
        }
    }
}
