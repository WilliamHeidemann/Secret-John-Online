using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JS : MonoBehaviour
{
    public void ChangeBackground()
    {
        Camera.main.backgroundColor = Color.yellow;
        Console.WriteLine("Writing from console writeline");
        Debug.Log("Writing from debug log");
        print("Writing from print");
    }
}
