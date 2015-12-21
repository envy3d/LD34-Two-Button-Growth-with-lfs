using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TitleScreenInteraction : MonoBehaviour
{
	
	void Update()
    {
	    if (Input.GetButtonDown("Submit") || Input.GetButtonDown("P1B1") || Input.GetButtonDown("P2B1") || Input.GetButtonDown("P3B1") || Input.GetButtonDown("P4B1"))
        {
            SceneManager.LoadScene("Selection");
        }
	}
}
