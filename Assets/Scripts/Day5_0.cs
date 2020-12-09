using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Day5_0 : MonoBehaviour
{
    [SerializeField] private TextAsset input;
    private string output;

    private int polymerLength;
    private char char1;
    private char char2;

    private int loopcounter = 0;
    private int pairsRemoved = 0;
    private int pairsRemoved2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        DateTime sample = DateTime.Now;

        string pattern = @"([a-z])([A-Z])";
        string pattern2 = @"([A-Z])([a-z])";

        output = input.text;

    loop:
        pairsRemoved = 0;
        pairsRemoved2 = 0;
        loopcounter++;
            
            if (loopcounter > 2000)
            {
                Debug.Log("max loopcounter reached");
                goto breakloop;
            }

        foreach (System.Text.RegularExpressions.Match m in
        System.Text.RegularExpressions.Regex.Matches(output, pattern))
        {
            

            char1 = char.Parse(m.Groups[1].Value);
            char2 = char.Parse(m.Groups[2].Value);

            //Debug.Log(char1 + char2);

            //if ((int)char1 == ((int)char2 + 32) || (int)char1 == ((int)char2 - 32))
            if ((int)char1 == ((int)char2 + 32))
            {
                //Debug.Log("reagent found at " + m.Index + " for " + m.Length + " chars");
                output = output.Remove((m.Index-(2*pairsRemoved)),m.Length);
                pairsRemoved++;
                //goto loop;
                 //use continue?
            }
        }

        foreach (System.Text.RegularExpressions.Match m in
        System.Text.RegularExpressions.Regex.Matches(output, pattern2))
        {
            //loopcounter++;

            //if (loopcounter > 15000)
            //{
            //    Debug.Log("max loopcounter reached");
            //    break;
            //}

            char1 = char.Parse(m.Groups[1].Value);
            char2 = char.Parse(m.Groups[2].Value);

            //Debug.Log(char1 + char2);

            //if ((int)char1 == ((int)char2 + 32) || (int)char1 == ((int)char2 - 32))
            if ((int)char1 == ((int)char2 - 32))
            {
                //Debug.Log("reagent found at " + m.Index + " for " + m.Length + " chars");
                output = output.Remove((m.Index - (2 * pairsRemoved2)), m.Length);
                pairsRemoved2++;
                //goto loop;
                 //use continue?
            }
        }
        if (pairsRemoved > 0 || pairsRemoved2 > 0)
        {
            goto loop;
        }

    breakloop:
        polymerLength = output.Length;

        Debug.Log(output);
        Debug.Log("length polymer: " + polymerLength);

        TimeSpan elapsed = (DateTime.Now - sample);
        Debug.Log(string.Format("This took {0} milliseconds", elapsed.TotalMilliseconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
