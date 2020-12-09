using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Day5_1 : MonoBehaviour
{
    [SerializeField] private TextAsset input;
    private string output;

    private int polymerLength;
    private char char1L, char2U, char1U, char2L;

    private int pairsRemoved = 0;
    private int pairsRemoved2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        DateTime sample = DateTime.Now;

        string pattern = @"(((?'char1U'[A-Z])(?'char2L'[a-z]))|((?'char1L'[a-z])(?'char2U'[A-Z])))";
        //string pattern = @"([a-z])([A-Z])";
        //string pattern2 = @"([A-Z])([a-z])";

        output = input.text;

    loop:
        pairsRemoved = 0;
        //pairsRemoved2 = 0;
        //output = output;

        foreach (System.Text.RegularExpressions.Match m in
        System.Text.RegularExpressions.Regex.Matches(output, pattern))
        {
            //Debug.Log(char.Parse(m.Groups["char2L"].Value));

            char.TryParse(m.Groups["char1L"].Value, out char1L);
            char.TryParse(m.Groups["char2U"].Value, out char2U);
            char.TryParse(m.Groups["char1U"].Value, out char1U);
            char.TryParse(m.Groups["char2L"].Value, out char2L);

            //Debug.Log(char1 + char2);

            //if ((int)char1 == ((int)char2 + 32) || (int)char1 == ((int)char2 - 32))
            if ((int)char1L == ((int)char2U + 32) || (int)char1U == ((int)char2L - 32))
            {
                //Debug.Log("reagent found at " + m.Index + " for " + m.Length + " chars");
                output = output.Remove((m.Index-(2*pairsRemoved)),m.Length);
                pairsRemoved++;
                //goto loop;
            }
        }

        /*
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
            }
        }*/

        //if (pairsRemoved > 0 || pairsRemoved2 > 0)
        if (pairsRemoved > 0)
        {
            goto loop;
        }

        polymerLength = output.Length;

        //Debug.Log(output);
        Debug.Log("length polymer: " + polymerLength);

        TimeSpan elapsed = (DateTime.Now - sample);
        Debug.Log(string.Format("This took {0} milliseconds", elapsed.TotalMilliseconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
