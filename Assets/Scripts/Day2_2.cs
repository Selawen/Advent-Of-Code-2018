using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Day2_2 : MonoBehaviour
{
    [SerializeField] private TextAsset input;
    private string[] textArray;
    private int checkSum = 0;
    private int condition1Amount;
    private int condition2Amount;
    private bool condition1Satisfied;
    private bool condition2Satisfied;
    private char currentLetter;
    private string pattern;

// Start is called before the first frame update
void Start()
    {
        DateTime sample = DateTime.Now;
        textArray = input.text.Split('\n');

        foreach (string IDString in textArray)
        {
            condition1Satisfied = false;
            condition2Satisfied = false;

            for (int i = 0; i < 26; i++)
            {
                currentLetter = (char)(97 + i);

                if (!condition1Satisfied)
                {
                    pattern = @"^(\D*)" + @currentLetter + @"(\D*)" + @currentLetter + @"(\D*)$/m";
                    foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(IDString, pattern))
                    {
                        condition1Amount++;
                        condition1Satisfied = true;
                    }
                }

                if (!condition2Satisfied)
                {
                    pattern = @"^(\D*)" + @currentLetter + @"(\D*)" + @currentLetter + @"(\D*)" + @currentLetter + @"(\D*)$/m";
                    foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(IDString, pattern))
                    {
                        condition2Amount++;
                        condition2Satisfied = true;
                    }
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
