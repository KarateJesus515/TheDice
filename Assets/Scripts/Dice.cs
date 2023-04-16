using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dice : MonoBehaviour
{
    Rigidbody rb;

    bool thrown;

    private AudioSource diceAudioSource;
    public AudioClip[] diceHitSounds;

    Vector3 initPosition;

    public DiceSide[] diceSides;

    public int diceValue;

    public TextMeshProUGUI totalScore;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        diceAudioSource = GetComponent<AudioSource>();

        print("void Start");
        print("thrown:" + thrown);
    }
    public void RollDice()
    {
        print("void RollDice");
        print("thrown:" + thrown);
        if (!thrown)
        {
            StartCoroutine(RollDiceDelay());
            thrown = true;
            rb.AddForce(Random.Range(-800, 800), Random.Range(900, 1400), Random.Range(-800, 800));
            rb.AddTorque(Random.Range(-20000, 20000), Random.Range(-20000, 20000), Random.Range(-20000, 20000));
        }
        else if (thrown)
        {
            //reset the dice position
            Reset();
        }
    }

    IEnumerator RollDiceDelay()
    {
        yield return new WaitForSeconds(5);
        //RollDice();
        SideValueCheck();
    }

    void Reset()
    {
        transform.position = initPosition;
        thrown = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            diceAudioSource.clip = diceHitSounds[Random.Range(0, diceHitSounds.Length)];
            diceAudioSource.PlayOneShot(diceAudioSource.clip);
        }
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (rb.IsSleeping() && !thrown)
            {
                RollDice();

            }
            else if (rb.IsSleeping() && thrown)
            {
                Reset();
            }
            //roll the dice again

        }



    }

    /*void RollAgain()//needed to rename
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 700), Random.Range(0, 700), Random.Range(0, 700));
    }*/

    public void SideValueCheck()
    {
        diceValue = 0;

        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                totalScore.text = "Score: " + diceValue.ToString(); // I changed score to diceValue
                //return diceValue;

                //send results to GameManager script
                //GameManager.instance.RollDice(diceValue);
            }
        }
    }
}