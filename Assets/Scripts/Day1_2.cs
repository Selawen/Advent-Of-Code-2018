using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Day1_2 : MonoBehaviour
{
    [SerializeField] private TextAsset input;
    private int frequency = 0;
    private int value;

    // Start is called before the first frame update
    void Start()
    {
        DateTime sample = DateTime.Now;

        string pattern = @"([-+*/])(\d+)";
        
        foreach (System.Text.RegularExpressions.Match m in
        System.Text.RegularExpressions.Regex.Matches(input.text, pattern))
        {
            value = int.Parse(m.Groups[2].Value);
            switch (m.Groups[1].Value)
            {
                case "+":
                    frequency += value;
                    break;
                case "-":
                    frequency -= value;
                    break;
            }
        }
        
        
        Debug.Log("Frequency: " + frequency);

        TimeSpan elapsed = (DateTime.Now - sample);
        Debug.Log(string.Format("This took {0} milliseconds", elapsed.TotalMilliseconds));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
