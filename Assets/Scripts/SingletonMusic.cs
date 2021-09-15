using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMusic : MonoBehaviour
{

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BgMusic");

        // Checks if the number of objects (Length)
        //      in the array is greater than 1 
        if (objs.Length > 1)
        {
            // Destroys excess gameobjects that holds the bg music
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

}
