using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class PrototypeInkScript : MonoBehaviour
{
    [Header("Dialouge")]
    public Button btn;
    public TextMeshProUGUI dialouge;
    public GameObject choicesPanel;

    [Header("Ink")]
    public TextAsset inkJSONAsset;
    private Story story;
    private List<string> tags;

    [Header("Ui Elements")]
    public Slider moodMeter;
    

#region Default Functions
    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSONAsset.text);
        tags = new List<string>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(story.canContinue)
            {
                DisplayDialouge();
                if (story.currentChoices.Count != 0)
                {
                    // Create choices
                    CreateChoices();
                }
            }
        }
    }
#endregion

#region Dialouge Functions
    private void DisplayDialouge()
    {
        Debug.Log("Advancing Dialouge");
        string currentText = story.Continue();
        dialouge.text = currentText;
        ParseTags();
    }

    private void Refresh()
    {
        ClearChoices();
        DisplayDialouge();
    }
#endregion

#region Tags Functions
    private void ParseTags()
    {
        tags = story.currentTags;
        string[] tagArray = new string[2];
        int thirdIndex = 3;

        foreach (string tag in tags)
        {
            tagArray = tag.Split(char.Parse(" "));
            string prefix = tagArray[0];
            string parameter = tagArray[1];

            // Checks if the current tagArray length is less or equal to the third index
            if(thirdIndex <= tagArray.Length)
            {
                Debug.Log("Third Paramenter");
                string moodValue = tagArray[2];
            }

            switch(prefix.ToLower())
            {
                // Have a separate addMood and  decreaseMood methods and pass in the char name to the method and value?
                case "bg":
    
                    break;
                case "mood++":
                    
                    break;
                case "mood--":

                    break;
                case "moodAll++":

                    break;
                case "moodAll--":

                    break;
                default:
                    Debug.Log($"This prefix is not included: {prefix} and the parameter is: {parameter}");
                    break;
            }
            Debug.Log($"The prefix is: {prefix} and the parameter is: {parameter}");
        }
    }
#endregion

#region Choices Functions
    private void CreateChoices()
    {
        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(btn) as Button;
            choiceButton.transform.SetParent(choicesPanel.transform, false);

            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
            choiceText.text = choice.text;

            // Set listener
            choiceButton.onClick.AddListener(delegate
            {
                OnClickChoiceButton(choice);
            });
        }
        //StartAnxietyMeter();
    }

    private void ClearChoices()
    {
        int childCount = choicesPanel.transform.childCount;
       //Debug.Log(childCount);
        for(int i = childCount - 1; i >= 0; --i)
        {
            //Debug.Log(i);
            GameObject.Destroy(choicesPanel.transform.GetChild(i).gameObject);
        }
    }

    private void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        //StopAnxietyMeter();
        Refresh();
    }
#endregion

#region Game Mechanics Functions



    // public void StopAnxietyMeter()
    // {
    //     Debug.Log("Stopping Coroutine");
    //     StopCoroutine(AMManager.StartAnxietyMeter());
    //     AMManager.HideAMChild(); 
    // }


    // public void StartAnxietyMeter()
    // {
    //     AMManager.ShowAMChild();
    //     StartCoroutine(AMManager.StartAnxietyMeter());
    // }
    


#endregion








}
