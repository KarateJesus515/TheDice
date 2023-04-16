using System.Collections;
using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    Rigidbody rb;

    bool hasLanded;
    bool thrown;

    Vector3 initPosition;

    public DiceSide[] diceSides;

    public int diceValue;

    public TextMeshProUGUI totalScore;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        Print("void Start")
       Print("thrown:" + thrown)
       Print("hasLanded:" + hasLanded)
    }
    public void RollDice()
    {
        Print("void RollDice")
         Print("thrown:" + thrown)
         Print("hasLanded:" + hasLanded)
         if (!thrown && !hasLanded)
        {
            StartCoroutine(RollDiceDelay());
            thrown = true;
            rb.AddTorque(Random.Range(100, 900), Random.Range(100, 900), Random.Range(100, 900));
        }
        else if (thrown && hasLanded)
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
        hasLanded = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (rb.IsSleeping() && !hasLanded && !thrown)
            {
                RollDice();

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

    public int SideValueCheck()
    {
        diceValue = 0;

        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                totalScore.text = "Score: " + diceValue.ToString(); // I changed score to diceValue
                return diceValue;

                //send results to GameManager script
                //GameManager.instance.RollDice(diceValue);
            }
        }
    }
}