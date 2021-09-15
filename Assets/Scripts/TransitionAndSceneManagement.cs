using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionAndSceneManagement : MonoBehaviour
{
    private Animator transitionAnim;
    private float transitionSeconds = 2f;


    // Start is called before the first frame update
    void Start()
    {
        transitionAnim = this.GetComponent<Animator>();
    }

#region Main Functions
    // Plays the animation transition for a certain duration
    public IEnumerator PlayTransition(string action)
    {
        switch (action)
        {
            case "start":
                transitionAnim.SetTrigger("start");
                yield return new WaitForSeconds(transitionSeconds);
                break;
            case "end":
                transitionAnim.SetTrigger("end");
                yield return new WaitForSeconds(transitionSeconds);
                break;
            default:
                Debug.Log("Not in Switch Case");
                break;
        }
    }

    // Loads the scene based off the scene numbers
    public IEnumerator LoadScene(int sceneNumber)
    {
        yield return (PlayTransition("start"));
        switch (sceneNumber)
        {
            // Main Menu
            case 0:
                SceneManager.LoadScene(0);
                break;
            // Game Play
            case 1:
                SceneManager.LoadScene(1);
                break;
            //Ending
            case 2:
                SceneManager.LoadScene(2);
                break;
            default:
                Debug.Log("Scene does not exist");
                break;
        }
        
    }
#endregion

#region Public Functions 
    /*================= 
    Public Functions for the buttons
    =================*/
    public void StartGame()
    {
        StartCoroutine(LoadScene(1));
    }

    // Loads Ending Scene and saves the ending 
    public void ShowEnding(string ending)
    {
        PlayerPrefs.SetString("ending", ending);
        StartCoroutine(LoadScene(2));
    }

    // Loads the menu and removes the ending
    public void BackToMainMenu()
    {
        StartCoroutine(LoadScene(0));
        PlayerPrefs.DeleteKey("ending");
    }

    public void ExitGame()
    {
        StartCoroutine(Exit());
    }

    // Plays out the start transition then exits the app
    public IEnumerator Exit()
    {
        yield return (PlayTransition("start"));
        Application.Quit();
    }
#endregion 
    
}
