using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColour : MonoBehaviour
{
    //public GameObject goPlayer1;
    //public GameObject goPlayer2;

    public TrailRenderer Trail1;
    public TrailRenderer Trail2;

    //public Material matPlayer1;
    //public Material matPlayer2;

    // true = player 1
    // false = player 2
    //[HideInInspector]
    //public bool bPlayerHasRang;

    // Use this for initialization
    void Start()
    {
        //bPlayerHasRang = true;
    }

    // Update is called once per frame
    void Update()
    {
        //if (goPlayer1.GetComponent<PlayerController>().hasBomberang)
        //{
        //    bPlayerHasRang = true;
        //}

        //if (goPlayer2.GetComponent<PlayerController>().hasBomberang)
        //{
        //    bPlayerHasRang = false;
        //}

        //if (bPlayerHasRang)
        //{
        //    Trail1.startColor = matPlayer1.color;
        //    Trail2.startColor = matPlayer1.color;
        //}

        //else if (!bPlayerHasRang)
        //{
        //    Trail1.startColor = matPlayer2.color;
        //    Trail2.startColor = matPlayer2.color;
        //}

    }

    public void SetColour(Color colour)
    {
        Trail1.startColor = colour;
        Trail2.startColor = colour;
    }
}
