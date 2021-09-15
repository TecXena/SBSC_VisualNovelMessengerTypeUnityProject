using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class FinalDialougeScript : MonoBehaviour
{

#region Variables
    [Header("Dialouge")]
    public Button btn;
    private Coroutine co;
    private GameObject topBarPanel;
    private GameObject choicesPanel;
    private GameObject messagePanel;
    private GameObject subGameObject;
    private RectTransform messagePanelRectTransform;
    private ScrollRect messageScroll;
    private SubtitleGamePlay subtitle;
    private TextMeshProUGUI dialouge;
    private TransitionAndSceneManagement TASManagement;
    private float currentScrollPos;
    private string currentText;
    
    

    [Header("Ink")]
    public TextAsset inkJSONAsset;
    public string currentMode;
    public string currentUser;
    private List<string> tags;
    private Story story;
    private int V_Points;
    private int Abe_Points;
    

    [Header("Ui Elements")]
    
    public Image bg;
    public Sprite bg_Day;
    public Sprite bg_Night;
    public GameObject msg_MC;
    public GameObject msg_Vivi;
    public GameObject msg_Abe;
    public GameObject transitionHolder;
    private Animator transitionAnim;
    private Animator phoneAnim;
    private Animator narrativeTextBoxAnim;
    private Color32 bg_Light;
    private Color32 bg_Dark;
    private GameObject narrativeTextBox;
    private TextMeshProUGUI narrativeTextBoxText;
    private float bg_Fade_Duration;
    

    [Header("Audio")]
    public AudioClip[] SFX_NewMessageList;
    public AudioSource SFX_NewMessage;
    public AudioSource SFX_NarrativeVoice;
    public AudioSource SFX_PressedButton;
    
#endregion
#region Default Functions
    // Start is called before the first frame update
    void Awake()
    {
        // Setting the variables Related to the Ink files
        story = new Story(inkJSONAsset.text);
        tags = new List<string>();
        currentMode = "";
        V_Points = (int) story.variablesState["V_points"];
        Abe_Points = (int) story.variablesState["Abe_points"];

        // Setting the variables that requires manipulation of singular components 
        // & addition of new game object children
        topBarPanel = this.transform.GetChild(0).gameObject;
        choicesPanel = this.transform.GetChild(2).GetChild(1).gameObject;
        messageScroll= this.transform.GetChild(1).gameObject.GetComponent<ScrollRect>();
        messagePanel = this.transform.GetChild(1).GetChild(0).gameObject;
        narrativeTextBox = this.transform.parent.parent.GetChild(2).gameObject;
        subGameObject = this.transform.parent.parent.GetChild(3).gameObject;
        
        // Setting the variables that needs their respective Components
        transitionAnim = this.transform.parent.parent.GetChild(4).gameObject.GetComponent<Animator>();
        phoneAnim = transform.GetComponentInParent<Animator>();
        narrativeTextBoxAnim = narrativeTextBox.GetComponent<Animator>();
        messagePanelRectTransform = messagePanel.transform.GetComponent<RectTransform>();
        narrativeTextBoxText = narrativeTextBox.GetComponentInChildren<TextMeshProUGUI>();
        TASManagement = transitionHolder.GetComponent<TransitionAndSceneManagement>();
        subtitle = subGameObject.GetComponent<SubtitleGamePlay>();

        // Sets the variables for the ChangeBG IEnumerator
        bg_Light = new Color32(255,255,255,255);
        bg_Dark = new Color32(191,191,191,255);
        bg_Fade_Duration = 0.5f;

        // Starts off the Animations of the Phone and Narrative Text Box
        phoneAnim.SetBool("isShown", false);
        narrativeTextBoxAnim.SetBool("isShown", true);
    }

    void Start()
    {
        // Skip the first text of dialouge function here 
        DisplayDialouge();
    }

    // Update is called once per frame
    void Update()
    {

        //For moving the dialouge using the space button
        //Previous Code
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(story.canContinue)
            {
                DisplayDialouge();
                if (story.currentChoices.Count != 0)
                {
                    CreateChoices();
                }

                /*Previous Skip Dialouge Function (Requires to press space 3 times)
                    // clickCount_Dialouge++;
                    
                    // // If the clickCount_Dialouge is equal to 2 or if the coroutine exists
                    // if (clickCount_Dialouge >= 2 && co != null)
                    // {
                    //     // Skip text Animation and then reset the click cout
                    //     SkipAnimation(currentMode);
                    //     clickCount_Dialouge = 0;
                    // }
                    // else
                    // {
                    //     // Else display the dialouge 
                    //     DisplayDialouge();
                    //     if (story.currentChoices.Count != 0)
                    //     {
                    //         CreateChoices();
                    //     }
                    // }
                    */

            }
        }
        /*Possible New Skip Function in the future
        Basic idea is that:

            When we press the left mouse button, we put the dialouge inside a coroutine 
                that will turn the isCoroutineDone bool to true when the coroutine is done
            When we press the left mouse button again, it will go check if the coroutine is done 
            If the coroutine is done, then put the dialouge inside the coroutine again
            If not, then we show the full dialouge 
            
            if(Input.MouseButtonDown)
            {
                if (story.canContinue)
                {
                    // Make sures that the click count is counted
                    clickCount_Dialouge++;

                    // When there is no coroutine, then load it first
                    // This will skip if the coroutine is still going
                    if(dialougeCo == null)
                    {
                        dialougeCo = StartCoroutine(LoadDialouge());
                    }

                    // Checks if the coroutine is done or has been canceled
                    // Which will load the next batch of dialouge
                    if (isCoroutineDone == true)
                    {
                        StopCoroutine(dialougeCo);
                        dialougeCo = StartCoroutine(LoadDialouge());
                    }
                    // Will only run when the click count is 2 so that it won't 
                    // skip after 1 click
                    else if (clickCount_Dialouge == 2 && dialougeCo != null)
                    {
                        SkipAnimation(currentMode);
                        // Resets coroutine
                        dialougeCo = null;
                    }
                }
                
            }
            
                
            // Old idea===========================================================
            // // This restarts the coroutine bool huhu
            // DisplayDialouge();

            // //We need a way to do a check when the coroutine is done then do this line of code
            // // Maybe do another coroutine ? But that will only make the first one work, unless 
            // // we separate the skip function (leave it here) and then stops the coroutine (co)

            // // The coroutine bool becomes false thus skipping the dialouge
            // if (clickCount_Dialouge == 2 && isCoroutineDone == true)
            // {
            //     Debug.Log("ELLO THERE");
            //     DisplayDialouge();
                
            //     if (story.currentChoices.Count != 0)
            //     {
            //         CreateChoices();
            //     }
            //     clickCount_Dialouge = 0;
            //     //Debug.Log("Next Dialouge when the coroutine is done");
            // }
            // else if (clickCount_Dialouge == 2 && co != null)
            // {
                
            //     SkipAnimation(currentMode);
            //     clickCount_Dialouge = 0;
            //     Debug.Log("Skipping dialouge");

            // }
        */
    }
#endregion

#region Dialouge Functions
    // Advances the dialouge of the story
    private void DisplayDialouge()
    {
        currentText = story.Continue();
        ParseTags();
        
        // If the Narrative Text Box is Shown
        if(narrativeTextBoxAnim.GetBool("isShown") == true)
        {
            // Then Show Text based on the current text mode
            CurrentTextMode(currentMode);
        }
    }

    // Refreshes the Choices Panel and shows the next line of Dialouge
    private void Refresh()
    {
        ClearChoices();
        DisplayDialouge();
    }
    
    // Loads the top tags and moves the dialouge
    public IEnumerator LoadTopTags()
    {
        yield return TASManagement.PlayTransition("start");
        DisplayDialouge();
        yield return TASManagement.PlayTransition("end");
    }

    // Checks the current mode of the story
    //  and displays dialouge based on the mode
    public void CurrentTextMode(string textMode)
    {
        //Check if the coroutine has a Coroutine
        if (co != null)
        {
            // Stop it to have a fresh batch of text
            StopCoroutine(co);  
        }

        // Starts showing the dialouge based on the mode
        switch (textMode.ToLower())
        {
            // If the phone is not visible
            case "nophone":
                co = StartCoroutine(EnumerateNarrativeDialouge());
                break;
            // If the phone is visible
            case "phone":
                // Check if the dialouge has a TextMeshProGUI componenet
                if (dialouge != null)
                {
                    co = StartCoroutine(TypingMessage());
                }
                break;
            // If the dialouge should be narrated while the phone is active
            case "narrativewithphone":
                co = StartCoroutine(EnumerateNarrativeDialouge());
                break;
            default:
                break;
        }
        // Sets the current mode 
        currentMode = textMode;
    }

    // Types out an ellipsis for a certain amount of time
    //  then the message
    public IEnumerator TypingMessage()
    {
        // Variables for typing the ellipsis
        string ellipses = "...";
        int cycleCount = 1;
        int currentCycleCount = 0;
        float cycleDuration = 0.2f;

        // First Scroll down when their is a message game object
        StartCoroutine(ScrollToBottom());

        while (currentCycleCount < cycleCount)
        {
            // Add an ellipsis in the dialouge after the set duration
            foreach (char ellipsis in ellipses.ToCharArray())
            {
                dialouge.text += ellipsis;
                yield return new WaitForSeconds(cycleDuration);
            }

            // Clears the dialouge and increments the currentCycleCount
            dialouge.text = "";
            currentCycleCount++;
        }

        // Ends the coroutine
        yield return null;

        // Sets the current text of the story to the message box
        // and scrolls it down for focus
        dialouge.text = currentText;
        PlayRandomMessageTune();
        StartCoroutine(ScrollToBottom());
    }

    // Enumerates the text in a type writer fashion
    public IEnumerator EnumerateNarrativeDialouge()
    {
        // Grabs the current text and cleans out the narrative text box text
        string sentence = currentText;
        narrativeTextBoxText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            // Add a letter to the narrative text box after a certain duration
            narrativeTextBoxText.text += letter;
            SFX_NarrativeVoice.Play();
            yield return new WaitForSeconds(0.02f);
        }

        // End the couroutine
        yield return null;
    }

    // Old skip anim show dialouge code ==========================================
    // Skips the animation and shows the full current text
    public void SkipAnimation(string mode)
    {
        // Stops the coroutine to clean out the text component
        StopCoroutine(co);

        switch (mode.ToLower())
        {   
            case "nophone":
                narrativeTextBoxText.text = currentText;
                break;
            case "phone":
                // Checks if the dialouge has a textMeshProGUI component
                if (dialouge != null)
                {
                    dialouge.text = currentText;
                }
                break;
            case "narrativewithphone":
                narrativeTextBoxText.text = currentText;
                break;
            default:
                break;
        }
    }

#endregion

#region Tags Functions

    //Breaks up the Tag into 2 components
    private void ParseTags()
    {
        // Grabs the current tags and creates a new tag Array
        tags = story.currentTags;
        string[] tagArray = new string[2];

        foreach (string tag in tags)
        {
            //Splits the Tag and puts it into the array
            //Check GameJam Inkle old ver of this script for a more flexible parser
            tagArray = tag.Split(char.Parse(" "));

            // Places the respective values into the respective variables
            string prefix = tagArray[0];
            string parameter = tagArray[1];

            // Checks what is the prefix that determines the method to be used
            switch(prefix.ToLower())
            {
                case "mode":
                    SetGameMode(parameter.ToLower());
                    break;
                case "bg":
                    SetBG(parameter.ToLower());
                    break;
                case "addpoint":
                    AddPointToCharacter(parameter.ToLower());
                    break;
                case "ending":
                    TASManagement.ShowEnding(parameter.ToLower());
                    break;
                case "showpm":
                    ShowPrivateConvo(parameter.ToLower());
                    break;
                case "user":
                    CreateMessageBox(parameter.ToLower());
                    break;
                case "phonetime":
                    SetPhoneTime(parameter);
                    break;
                case "phoneday":
                    SetPhoneDay(parameter);
                    break;
                case "transition":
                    StartCoroutine(TASManagement.PlayTransition(parameter.ToLower()));
                    break;
                case "skip":
                    StartCoroutine(LoadTopTags());
                    break;
                case "showsubtitle":
                    subGameObject.SetActive(true);
                    subtitle.subtitleMode = parameter.ToLower();
                    subtitle.FadeIn();
                    break;
                default:
                    Debug.Log($"This prefix is not included: {prefix} and the parameter is: {parameter}");
                    break;
            }
        }
    }

#endregion

#region Choices Functions
    // Creates the choices buttons depending on the
    //  no. of choices in the ink file
    private void CreateChoices()
    {
        foreach (Choice choice in story.currentChoices)
        {
            // Instantiates a button and sets the button location
            //  to the Parent choicesPanel
            Button choiceButton = Instantiate(btn) as Button;
            choiceButton.transform.SetParent(choicesPanel.transform, false);

            // Sets the text of the choice
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;

            // Sets the listener
            choiceButton.onClick.AddListener(delegate
            {
                OnClickChoiceButton(choice);
            });
        }
    }

    // Removes the choices
    private void ClearChoices()
    {
        // Grabs the number of childern in the choicesPanel
        int childCount = choicesPanel.transform.childCount;

        // Note: Minuses the childCount so that it will be exact
        // Destroys all children in the ChoicesPanel in a for loop
        for(int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(choicesPanel.transform.GetChild(i).gameObject);
        }
    }

    // When the choice button has been clicked
    private void OnClickChoiceButton(Choice choice)
    {
        // Choose the index that represents the choice and refresh the choices panel
        story.ChooseChoiceIndex(choice.index);
        SFX_PressedButton.Play();
        Refresh();
    }
#endregion

#region Game Mechanics Functions
    // Creates the Message Box of the designated character
    public void CreateMessageBox(string user)
    {
        // Make sure to start off null so that it will always be empty
        //  everytime the method is called
        GameObject newMessage = null;
        
        // Checks who is the user and creates the message box 
        //  prefab designated to them.
        switch (user)
        {
            case "mc":
                newMessage = Instantiate(msg_MC,new Vector3(0,0,0), Quaternion.identity);
                break;
            case "v":
                newMessage = Instantiate(msg_Vivi,new Vector3(0,0,0), Quaternion.identity);
                break;
            case "a":
                newMessage = Instantiate(msg_Abe,new Vector3(0,0,0), Quaternion.identity);
                break;
            default:
                Debug.Log("Not in list");
                break;
        }

        // If the message box exists (Not equal to null)
        if (newMessage != null)
        {
            // Set the Game object location to the messagePanel Parent
            newMessage.transform.SetParent(messagePanel.transform, false);

            // Grabs the text of the instantiated Game Object
            dialouge = newMessage.transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            
            // Then Checks the Current Text Mode on where the text will be shown
            CurrentTextMode(currentMode);
        }
        currentUser = user;
        //Debug.Log($"Creating Message box for {user} with the content {currentText}");
    }

    // Scrolls to the bottom of the scroll rect
    public IEnumerator ScrollToBottom()
    {
        // Sets the variables for the timer
        // and the current verticalNormalizedposition and target verticalNormalizedposition
        // Notes: verticalNormalizedPosition basically turns the large vertical number into within 0 or 1
        float time = 0;
        float duration = 1f;
        float endScrollPos = 0f;
        currentScrollPos = messageScroll.verticalNormalizedPosition;

        // While the time is less than the duration
        while(time < duration)
        {
            // Sets the current value based on the Lerp Duration
            messageScroll.verticalNormalizedPosition = Mathf.Lerp(currentScrollPos, endScrollPos, time/duration);
            // Adds the time based on current time 
            time += Time.deltaTime;
            yield return null;
        }
    }

    // Sets the Phone Time text variable
    public void SetPhoneTime(string time)
    {
        TextMeshProUGUI currentTime = topBarPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        currentTime.text = time;
    }

    // Sets the Phone Day text variable
    public void SetPhoneDay(string day)
    {
        TextMeshProUGUI currentDay = topBarPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        currentDay.text = day;
    }

    // Adds the point towards the character variable in ink
    public void AddPointToCharacter(string user)
    {
        switch (user)
        {
            case "v":
                V_Points++;
                story.variablesState["V_points"] = V_Points;
                break;
            case "a":
                Abe_Points++;
                story.variablesState["Abe_points"] = Abe_Points;
                break;
            default:
                break;
        }
    }

    /*  Sets the current game mode that will change what gets displayed
            and saves the current mode
        Adjusts the ff.:
            Animation of Phone
            Animation of Narrative Text Box
            BGColor (Dims when Phone is on focus)   */
    public void SetGameMode(string mode)
    {
        switch (mode)
        {
            case "nophone":
                // Hides the phone and lightens the background
                phoneAnim.SetBool("isShown", false);
                narrativeTextBoxAnim.SetBool("isShown", true);
                StartCoroutine(ChangeBGColor("Lighten"));
                break;

            case "phone":
                // If the Phone is already shown
                if(phoneAnim.GetBool("isShown") == true)
                {
                    // Then hide the narrative TextBox instead
                    narrativeTextBoxAnim.SetBool("isShown", false);
                }
                else
                {
                    // Else show the phone and hide the narrative text box
                    // Also dim the bg to focus on the phone
                    phoneAnim.SetBool("isShown", true);
                    narrativeTextBoxAnim.SetBool("isShown", false);
                    StartCoroutine(ChangeBGColor("Dim"));
                }
                break;

            case "narrativewithphone":
                // Shows both the phone and narrative text box
                // and dims the bg
                phoneAnim.SetBool("isShown", true);
                narrativeTextBoxAnim.SetBool("isShown", true);
                StartCoroutine(ChangeBGColor("Dim"));
                break;
            default:
                break;
        }
        // Saves the current mode
        CurrentTextMode(mode);
    }

    // Changes the BG Color using a Lerp Function
    public IEnumerator ChangeBGColor(string mode)
    {
        // Sets the first position of the Lerp Duration (float a = 0)
        float bg_Lerp_Duration = 0f;

        switch (mode.ToLower())
        {
            case "dim":
                // Check if the bg color is not dark, then do the code
                if(bg.color != bg_Dark)
                {
                    // While the Lerp duration is less than or equal to 1
                    while(bg_Lerp_Duration <= 1)
                    {
                        // Set the current color of the lerp
                        bg.color = Color32.Lerp(bg_Light, bg_Dark, bg_Lerp_Duration);
                        
                        // Increment the lerp duration and repeat until the condition has been met
                        bg_Lerp_Duration += Time.deltaTime/bg_Fade_Duration;
                        yield return null; 
                    }
                    // When the lerp duration is done, then snap the color 
                    bg.color = bg_Dark;
                }
                break;
            case "lighten":
                // Check if the bg color is not light, then do the code
                if(bg.color != bg_Light)
                {
                    while(bg_Lerp_Duration <= 1)
                    {
                        // Set the current color of the lerp
                        bg.color = Color32.Lerp(bg_Dark, bg_Light, bg_Lerp_Duration);

                        // Increment the lerp duration and repeat until the condition has been met
                        bg_Lerp_Duration += Time.deltaTime/bg_Fade_Duration;
                        yield return null; 
                    }
                    // When the lerp duration is done, then snap the color 
                    bg.color = bg_Light;
                }
                break;
            default:
                break;
        }
    }

    // Sets the background of the game
    public void SetBG(string bg_mode)
    {
        switch (bg_mode)
        {
            case "day":
                bg.sprite = bg_Day;
                break;
            case "night":
                bg.sprite = bg_Night;
                break;
            default:
                break;
        }
    }

    // Removes previous messages and then shows the PM convo
    public void ShowPrivateConvo(string user)
    {
        TextMeshProUGUI gcName = topBarPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        int childCount = messagePanel.transform.childCount;
        for(int i = childCount - 1; i >= 0; --i)
        {
            GameObject.Destroy(messagePanel.transform.GetChild(i).gameObject);
        }
        switch (user)
        {
            case "a":
                gcName.text = "PM with A";
                break;
            case "v":
                 gcName.text = "PM with V";
                break;
            default:
                break;
        }
    }

    // Plays a random tune whenever a message is sent
    public void PlayRandomMessageTune()
    {
        int i = Random.Range(1,6);

        SFX_NewMessage.clip = SFX_NewMessageList[i];
        SFX_NewMessage.Play();
    }

#endregion
}
