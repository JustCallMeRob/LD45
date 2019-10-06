using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empty : MonoBehaviour
{
    public Vector2 gridPosition = new Vector2();

    public void SetGridPosition(float x, float y)
    {
        gridPosition.x = x;
        gridPosition.y = y;
    }
}
