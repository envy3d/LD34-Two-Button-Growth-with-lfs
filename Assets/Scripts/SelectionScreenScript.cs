using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SelectionScreenScript : MonoBehaviour
{
    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    public GameObject p12;
    public GameObject p22;
    public GameObject p32;
    public GameObject p42;

    public GameObject startButton;

    public int readiedPlayers = 0;

    void Start()
    {
	
	}

	void Update()
    {
	    if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Title");
        }

        if (p1 != null && Input.GetButtonDown("P1B1"))
        {
            p1.SetActive(false);
            p1 = null;
            p12.SetActive(false);
            readiedPlayers++;
        }
        if (p2 != null && Input.GetButtonDown("P2B1"))
        {
            p2.SetActive(false);
            p2 = null;
            p22.SetActive(false);
            readiedPlayers++;
        }
        if (p3 != null && Input.GetButtonDown("P3B1"))
        {
            p3.SetActive(false);
            p3 = null;
            p32.SetActive(false);
            readiedPlayers++;
        }
        if (p4 != null && Input.GetButtonDown("P4B1"))
        {
            p4.SetActive(false);
            p4 = null;
            p42.SetActive(false);
            readiedPlayers++;
        }


        if (readiedPlayers >= 2)
        {
            //startButton.SetActive(true);
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene("jeremy_dev_2");
            }
        }

	}
}
