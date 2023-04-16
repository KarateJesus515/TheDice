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

    public DiceSide[] diceSides;

    public int diceValue;

    private AudioSource diceAudioSource;
    public AudioClip[] diceHitSounds;
    public bool diceResting = false;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ground")
        {
            diceAudioSource.clip = diceHitSounds[Random.Range(0, diceHitSounds.Length)];
            diceAudioSource.PlayOneShot(diceAudioSource.clip);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        diceAudioSource = GetComponent<AudioSource>();
    }

    void Reset()
    {
        transform.position = initPosition;
        thrown = false;
        hasLanded = false;
    }

    public void RollDice()
    {
        
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
        

        foreach (DiceSide side in diceSides)
        {
            if (side.OnGround())
            {
                diceValue = side.sideValue;
                Debug.Log("Dice Value is: " + diceValue);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {    
        if (Input.GetKeyDown("r"))
        {
            RollDice();
        }
    }
}
