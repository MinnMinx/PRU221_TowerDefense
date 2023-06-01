using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestInputMap : MonoBehaviour
{
    public Transform start, end;
    public Grid grid;
    public AstarPath path;
    public GridGraph graph;

    private void Start()
    {
        var topLeft = grid.WorldToCell(Camera.main.ViewportToWorldPoint(Vector2.one));
        var bottomRight = grid.WorldToCell(Camera.main.ViewportToWorldPoint(Vector2.zero));

        Debug.Log("Length X: " + Mathf.Abs(topLeft.x - bottomRight.x));
        Debug.Log("Length Y: " + Mathf.Abs(topLeft.y - bottomRight.y));
    }
}
