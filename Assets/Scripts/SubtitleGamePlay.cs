using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitleGamePlay : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Color32 zeroOpacity;
    private Color32 fullOpacity;
    private float fadeDuration = 1f;
    private int spaceCount = 0;
    private float originalXPos;
    private float newXPos = 340;
    public string subtitleMode;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        originalXPos = this.transform.position.x;

        //Set subtitle text color to have no opacity on awake
        text.color = zeroOpacity;
    }

    void Update()
    {
        // Adjusts the position and the color of the zeroOpacity and full Opacity
        //      depending on the mode of the subtitle
        if (subtitleMode == "phone")
        {
            this.transform.position = new Vector2 (newXPos, this.transform.position.y);
            zeroOpacity = new Color32(255,255,255,0);
            fullOpacity = new Color32(255,255,255,255);
        }
        else if (subtitleMode == "start")
        {
            this.transform.position = new Vector2 (originalXPos, this.transform.position.y);
            zeroOpacity = new Color32(0,0,0,0);
            fullOpacity = new Color32(0,0,0,255);
        }

        // Controls the dialouge and fade out of the subtitle
        //  Entirely dependent on the space, would be better if there were checks
        //  so that the user won't skip the important text
        if(Input.GetKeyDown(KeyCode.Space))
        {
            spaceCount++;
            switch(subtitleMode.ToLower())
            {
                case "start":
                    switch(spaceCount)
                    {
                        case 1:
                            StartCoroutine(FadeOutAndStopScript());
                            break;
                        default:
                            break;
                    }
                    break;
                case "phone":
                    switch (spaceCount)
                    {
                        case 1:
                            text.text = "You can hold down the left mouse button and drag up or down to view previous messages";
                            break;
                        case 2:
                            StartCoroutine(FadeOutAndStopScript());
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

    }

    // Simple Fade function
    public IEnumerator Fade(string mode)
    {
        float lerpDuration = 0f;
        
        switch (mode.ToLower())
        {
            case "fadein":
                while(lerpDuration <= 1)
                {
                    text.color = Color32.Lerp(zeroOpacity, fullOpacity, lerpDuration);
                    lerpDuration += Time.deltaTime/fadeDuration;
                    yield return null;
                }
                break;
            case "fadeout":
                while(lerpDuration <= 1)
                {
                    text.color = Color32.Lerp(fullOpacity, zeroOpacity, lerpDuration);
                    lerpDuration += Time.deltaTime/fadeDuration;
                    yield return null;
                }
                break;
            default:
                break;
        }
        
    }

    // Fade out the text using a coroutine, resets the spaceCount
    // then set up the new first dialouge
    public IEnumerator FadeOutAndStopScript()
    {
        yield return StartCoroutine(Fade("FadeOut"));
        text.text = "Press the button inside the choices box to send a message, then press space to view the next message";
        spaceCount = 0;
        this.gameObject.SetActive(false);
    }

    // Public Functions for other scripts to use
    public void FadeOut()
    {
        StartCoroutine(Fade("FadeOut"));
    }

    public void FadeIn()
    {
        StartCoroutine(Fade("FadeIn"));
    }

    
}
