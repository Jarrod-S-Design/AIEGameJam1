using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class HelpUI : MonoBehaviour
{
    public GameObject helpCanvas;

    [SerializeField] public XboxController controllerNumber = 0;

    public bool isPaused = false;

    private void Start()
    {
        isPaused = true;

    }

    private void Update()
    {
        if(XCI.GetButtonDown(XboxButton.Start) && isPaused == false)
        {
            isPaused = true;
        }

        if (XCI.GetButtonDown(XboxButton.Start) && isPaused == true)
        {
            isPaused = false;
        }

            if (isPaused)
        {
            Time.timeScale = 0;
            helpCanvas.SetActive(true);
        }

        else
        {
            Time.timeScale = 1;
            helpCanvas.SetActive(false);
        }
    }

    
    //public void buttonHelpClick()
    //{
    //    isPaused = false;

    //}
}
