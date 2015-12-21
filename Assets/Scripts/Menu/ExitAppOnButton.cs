using UnityEngine;

public class ExitAppOnButton : MonoBehaviour
{

    public string buttonName = "Cancel";

    void Update()
    {
        if (Input.GetButtonDown(buttonName))
            Application.Quit();
    }
}