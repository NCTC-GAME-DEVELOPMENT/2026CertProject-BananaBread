using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
public class Grid
{
    private int width;
    private int height;
    private int[,] gridArray;
    private float cellSize;
    Vector3 originPosistion;
    private TextMesh[,] DebugTextArray;

    public const int sortingOrderDefault = 5000;

    // grid size
    public Grid(int width, int height, float cellSize, Vector3 originPosition, GameObject volume)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosistion = originPosition;

        //GameObject sensor = volume;

        gridArray = new int[width, height];
        DebugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                DebugTextArray[x, y] = CreateWorldText(gridArray[x, y].ToString(), null, GetWorldPosition(x, y) + new Vector3(cellSize, 0, cellSize) * .5f, 30, Color.white, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);

                //Code to be used for trigger sensors. Not yet fully functionl
                //GameObject s = GameObject.Instantiate(sensor, GetWorldPosition(x, y) + new Vector3(cellSize, 1.5f, cellSize) * .5f, Quaternion.Euler(new Vector3(0, 0, 0)));
                //GridSensor gs = s.GetComponent<GridSensor>();
                //gs.SetValues(x, y);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

    }


    //Create Text in the World
    public static TextMesh CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left, int sortingOrder = sortingOrderDefault)
    {
        if (color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
    }

    // Create Text in the World
    public static TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        GameObject newText= new GameObject("World_Text", typeof(TextMesh));

        newText.transform.SetParent(parent, false);
        newText.transform.localPosition = localPosition;
        newText.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
        TextMesh textMesh = newText.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }

    //gets the world position and the x y
    //y will be used to represent the z axis
    public Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, 0, y) * cellSize + originPosistion;
    }
   public void GetXY(Vector3 worldPosition, out int x, out int y)
    {
        x = Mathf.FloorToInt((worldPosition - originPosistion).x / cellSize);
        y = Mathf.FloorToInt((worldPosition - originPosistion).z / cellSize);
    }
    public Vector2 GetXYAsV2(Vector3 worldPosition)
    {
        Vector2 result = Vector2.zero;
        result.x = Mathf.FloorToInt((worldPosition - originPosistion).x / cellSize);
        result.y = Mathf.FloorToInt((worldPosition - originPosistion).z / cellSize);
        return result;
    }

    public void SetValue(int x, int y, int value)
    {
        if(x >= 0 && y >= 0 && x < width && y < height)
        {
            gridArray[x, y] = value;
            DebugTextArray[x, y].text = gridArray[x, y].ToString();
        }
       
    }

    public void SetValue(Vector3 worldPosition, int value)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        SetValue(x, y, value);
    }

    public void AddValue(int x, int y, int value)
    {
        SetValue(x, y, GetValue(x, y) + value);
    }

    // checks if a input is in range and what to do if it is not
    public int GetValue(int x, int y)
    {
        if((x >= 0) && (y >= 0) && (x < width) && (y < height))
        {
            return gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }
     
    public int GetValue(Vector3 worldPosition)
    {
        int x, y;
        GetXY(worldPosition, out x, out y);
        return GetValue(x, y);
    }
}

