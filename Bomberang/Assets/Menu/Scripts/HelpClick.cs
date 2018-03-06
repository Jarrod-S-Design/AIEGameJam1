using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;


public class HelpClick : MonoBehaviour
{

    [SerializeField] public XboxController controllerNumber = 0;
    [SerializeField] XboxButton continueButton = XboxButton.A;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(XCI.GetButtonUp(continueButton))
        {
            SceneManager.LoadSceneAsync("Main", LoadSceneMode.Single);
        }
    }
}
