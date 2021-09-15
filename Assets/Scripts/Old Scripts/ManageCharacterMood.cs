using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class ManageCharacterMood : MonoBehaviour
{
    [Header("Ui Elements")]
    public Slider moodMeter;
    public float moodMeterCurrent, moodMeterMax = 1;
    

    // Values that affect the Anxiety Meter
    public float moodMeterPositive;
    public float moodMeterNegative;

    // Values that will affect the mood values
    public float incrementPosValue;
    public float incrementNegValue;

    // For handling the visual effects and feeling bar
    public enum feelings
    {
        Cheerful,
        relaxed,
        anxious,
        doubt
    }
    // Should also add in the anim to handle changing of feelings anims

    public class Character
    {
        // Variables that affects the choices
        public int charFeelForMC; // Will add or subtract depending on the choice of MC
    }

    void Start()
    {
        //moodMeterCurrent = moodMeter.value;
        //moodMeter = this.transform.GetComponent<Slider>();
        
    }


    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Space))
        {
            moodMeter.value = 1;
        }
    }

    public void CalculateMood()
    {
        moodMeterCurrent = moodMeterPositive-moodMeterNegative;
    }

}
