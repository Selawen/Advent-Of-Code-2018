using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Day3_1 : MonoBehaviour
{
    [SerializeField] private TextAsset input;
    private HashSet<Vector2Int> clothClaimed;
    private HashSet<Vector2Int> clothClaimedTwice;
    private Vector2Int rectPos;
    private int rectWidth;
    private int rectHeight;

    // Start is called before the first frame update
    void Start()
    {
        DateTime sample = DateTime.Now;
        clothClaimed = new HashSet<Vector2Int>();
        clothClaimedTwice = new HashSet<Vector2Int>();

        string pattern = @"#(\d+)\s?@\s?(\d+),(\d+):\s?(\d+)x(\d+)";

        foreach (System.Text.RegularExpressions.Match m in
        System.Text.RegularExpressions.Regex.Matches(input.text, pattern))
        {
            rectPos = new Vector2Int(int.Parse(m.Groups[2].Value), int.Parse(m.Groups[3].Value));
            rectWidth = int.Parse(m.Groups[4].Value);
            rectHeight = int.Parse(m.Groups[5].Value);

            for (int x = 0; x <= rectWidth - 1; x++)
            {
                for (int y = 0; y <= rectHeight - 1; y++)
                {
                    Vector2Int inchClaimed = rectPos + new Vector2Int(x, y);
                    if (!clothClaimed.Contains(inchClaimed))
                    {
                        clothClaimed.Add(inchClaimed);
                    }
                    else if (!clothClaimedTwice.Contains(inchClaimed))
                    {
                        clothClaimedTwice.Add(inchClaimed);
                    }
                }
            }
        }


        Debug.Log("overlapped inches: " + clothClaimedTwice.Count);

        TimeSpan elapsed = (DateTime.Now - sample);
        Debug.Log(string.Format("This took {0} milliseconds", elapsed.TotalMilliseconds));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
