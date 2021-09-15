using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Ending : MonoBehaviour
{
    
    
    public Sprite polaroid_V;
    public Sprite polaroid_A;
    public Sprite ending_MC;
    private string currentEnding;
    private TextMeshProUGUI endingText;
    private Image polaroid;
    private Image bg;

    void Start()
    {
        endingText = this.GetComponent<TextMeshProUGUI>();
        bg = this.transform.parent.GetChild(0).GetComponent<Image>();
        polaroid = this.transform.parent.GetChild(0).GetChild(0).GetComponent<Image>();
        // Display Ending at the Start
        DisplayEnding();
    }

    public void DisplayEnding()
    {
        // Grabs the saved ending string
        currentEnding = PlayerPrefs.GetString("ending");
        switch (currentEnding)
        {
            case "mc":
                Destroy(polaroid.gameObject);
                bg.sprite = ending_MC;
                endingText.text = "The end.";
                break;
            case "v":
                polaroid.sprite = polaroid_V;
                endingText.text = "Thanks for playing!";
                break;
            case "a":
                polaroid.sprite = polaroid_A;
                endingText.text = "Thanks for playing!";
                break;
            default:
                break;
        }
    }
}
