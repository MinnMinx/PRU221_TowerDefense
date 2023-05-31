using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestInputMap : MonoBehaviour
{
    public Tilemap selector;
    private Vector3Int? prevGrid;

    private void Update()
    {
        if (selector != null && Input.mousePresent)
        {
            var clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var gridPos = selector.WorldToCell(clickPos);
            if (prevGrid.HasValue && prevGrid.Value != gridPos)
            {
                selector.SetColor(prevGrid.Value, new Color
                {
                    r = 1,
                    g = 1,
                    b = 1,
                    a = 0.3f
                });
                prevGrid = null;
            }

            var tile = selector.GetTile<PlaceableTile>(gridPos);
            if (tile != null)
            {
                prevGrid = gridPos;
                selector.SetColor(prevGrid.Value, new Color
                {
                    r = 1, g = 1, b = 1, a = 1
                });
            }
        }
    }
}
