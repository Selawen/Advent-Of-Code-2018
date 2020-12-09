using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Day6_0 : MonoBehaviour
{
    [SerializeField] private TextAsset input;

    private int biggestAreaSize;

    private int posX, posY;
    private int maxX, maxY;

    private List<Vector2Int> coordinates;
    private HashSet<Vector2Int> coordinatesHash;
    private List<int> areaSize;

    private int?[,] grid;
    private bool expandedAreas;

    private int biggestArea;

    // Start is called before the first frame update
    void Start()
    {
        System.DateTime sample = System.DateTime.Now;

        coordinates = new List<Vector2Int>();
        areaSize = new List<int>();

    string pattern = @"(?<posX>\d+),\s(?<posY>\d+)";

        foreach (System.Text.RegularExpressions.Match m in
        System.Text.RegularExpressions.Regex.Matches(input.text, pattern))
        {
            posX = int.Parse(m.Groups["posX"].Value);
            posY = int.Parse(m.Groups["posY"].Value);

            coordinates.Add(new Vector2Int(posX, posY));
            areaSize.Add(1);

            if (posX > maxX)
            {
                maxX = posX;
            }

            if (posY > maxY)
            {
                maxY = posY;
            }
            //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = new Vector3(posX, 0, posY);
            //cube.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        }

        grid = new int?[maxX+1, maxY+1];

        for (int i = 0; i < coordinates.Count; i++)
        {
            posX = coordinates[i].x;
            posY = coordinates[i].y;
            grid[posX, posY] =  i;
        }

        for (int x = 0; x <= maxX; x++)
        {
            for (int y = 0; y <= maxY; y++)
            {
                if (grid[x, y] == null)
                {
                    float smallestDistance = Mathf.Infinity;

                    for (int i = 0; i < areaSize.Count; i++)
                    {
                        float distanceH = Mathf.Abs(x- coordinates[i].x) + Mathf.Abs(y - coordinates[i].y);
                        if (distanceH < smallestDistance)
                        {
                            smallestDistance = distanceH;
                            grid[x, y] = i;
                        }
                        else if (distanceH == smallestDistance)
                        {
                            grid[x, y] = null;
                        }
                    }

                    if (!(x == 0 || x == maxX || y == 0 || y == maxY) && (grid[x, y] != null) && (areaSize[(int)grid[x, y]] != 0))
                    {
                        areaSize[(int)grid[x, y]]++;

                        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        //cube.transform.position = new Vector3(x, 0, y);
                        //cube.GetComponent<Renderer>().material.color = new Color(1 / ((float)grid[x, y] / 5), 1 / ((float)grid[x, y] / 5), 1 / ((float)grid[x, y]/5));
                    }
                    else if ((x == 0 || x == maxX || y == 0 || y == maxY) && (grid[x, y] != null))
                    {
                        areaSize[(int)grid[x, y]] = 0;
                        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        //cube.transform.position = new Vector3(x, 0, y);
                        //cube.GetComponent<Renderer>().material.color = new Color(0,0,0);
                    }
                }
            }
        }

        biggestAreaSize = areaSize[0];

        for (int i = 1; i < areaSize.Count; i++)
        {
            if (areaSize[i] > biggestAreaSize)
            {
                biggestAreaSize = areaSize[i];
                //biggestArea = i;
            }
        }

        Debug.Log("Size biggest area: " + biggestAreaSize);
        //Debug.Log("Biggest area: " + biggestArea);

        System.TimeSpan elapsed = (System.DateTime.Now - sample);
        Debug.Log(string.Format("This took {0} milliseconds", elapsed.TotalMilliseconds));
    }
//
//    private class Node
//    {
//        public bool IsOrigin { get; private set; }
//        public int areaCode;
//
//        public int DToClosestCoord { get; private set; }
//
//        public Node(bool isOrigin, int area, int distance)
//        {
//            IsOrigin = isOrigin;
//            areaCode = area;
//            DToClosestCoord = distance;
//        }
//    }
}
