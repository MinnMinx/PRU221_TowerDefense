using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private Tilemap placeableMap;
    private MapPreviewer previewer;

    private void Awake()
    {
        previewer = new MapPreviewer(placeableMap);
    }

    private void Update()
    {
        PreviewTile(Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

    /// <summary>
    /// Check if the world position is a placeable tile in map.
    /// If it is, return true and output that tile's position
    /// </summary>
    /// <param name="worldPos">Input world position</param>
    /// <param name="tilePos">Output tile's world position</param>
    /// <param name="tilePos">Output tile's cell position in grid</param>
    /// <returns>Whether tile in the position is placeable or not</returns>
    public bool IsPlaceableTile(Vector3 worldPos, out Vector3 tilePos, out Vector3Int tileCell)
    {
        tilePos = Vector3.zero;
        tileCell = Vector3Int.zero;
        try
        {
            tileCell = placeableMap.WorldToCell(worldPos);
            PlaceableTile tile = placeableMap.GetTile<PlaceableTile>(tileCell);
            if (tile != null) {
                tilePos = placeableMap.CellToWorld(tileCell) + placeableMap.cellSize / 2f;
                return true;
            }
        } catch (System.NullReferenceException)
        {
            Debug.LogError("Placeable map is null, no tower will be placed!");
        }
        return false;
    }

    void PreviewTile(Vector3 mouseWorldPos)
    {
        previewer.Update(
            placeableMap.WorldToCell(mouseWorldPos));
    }


    private class MapPreviewer
    {
        private static Color ACTIVE_COLOR = Color.white;
        private static Color DEACTIVE_COLOR = new Color(1f, 1f, 1f, 0.3f);
        private Tilemap _placeableMap;
        private Vector3Int? _prevGrid;

        public MapPreviewer(Tilemap placeableMap)
        {
            this._placeableMap = placeableMap;
            _prevGrid = null;
        }

        public void Update(Vector3Int? input)
        {
            try
            {
                // If previous hovering grid is still active
                if (_prevGrid.HasValue && input != _prevGrid)
                {
                    _placeableMap.SetColor(_prevGrid.Value, DEACTIVE_COLOR);
                    _prevGrid = null;
                }
                // if there's tile need to be preview
                if (input.HasValue && input != _prevGrid)
                {
                    _prevGrid = input;
                    _placeableMap.SetColor(_prevGrid.Value, ACTIVE_COLOR);
                }
            }
            catch (System.NullReferenceException)
            {
                Debug.LogError("Placeale map is null!");
                _prevGrid = null;
            }
        }
    }
}
