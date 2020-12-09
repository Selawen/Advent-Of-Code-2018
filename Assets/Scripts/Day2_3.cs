using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Day2_3 : MonoBehaviour
{
    [SerializeField] private TextAsset input;
    private string[] textArray;
    private int checkSum = 0;
    private int condition1Amount;
    private int condition2Amount;
    private bool condition1Satisfied;
    private bool condition2Satisfied;

    // Start is called before the first frame update
    void Start()
    {
        DateTime sample = DateTime.Now;
        textArray = input.text.Split('\n');

        foreach(string IDString in textArray)
        {
            condition1Satisfied = false;
            condition2Satisfied = false;

            for (int i = 0; i < 26; i++)
            {
                int letterCount = 0;
                char currentLetter = (char)(97 + i);
                
                foreach (Char letter in IDString)
                {
                    if (currentLetter == letter)
                    {
                        letterCount++;
                    }
                }

                if (letterCount < 2) continue;

                if (letterCount == 2 && !condition1Satisfied)
                {
                    condition1Amount++;
                    condition1Satisfied = true;
                }
                else if (letterCount == 3 && !condition2Satisfied)
                {
                    condition2Amount++;
                    condition2Satisfied = true;
                }
            }
        }

        checkSum = condition1Amount * condition2Amount;
        Debug.Log("checksum: " + checkSum);

        TimeSpan elapsed = (DateTime.Now - sample);
        Debug.Log(string.Format("This took {0} milliseconds", elapsed.TotalMilliseconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
