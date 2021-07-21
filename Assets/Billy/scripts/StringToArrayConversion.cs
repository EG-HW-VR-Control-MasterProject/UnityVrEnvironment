using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StringToArrayConversion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string textArray = "Item1-Item2-Item3";
        string[] splitArray = textArray.Split(char.Parse("-"));
        List<string> items = new List<string>();

        for (int index = 0; index < splitArray.Length; index++)
        {
            items.Add(splitArray[index]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
