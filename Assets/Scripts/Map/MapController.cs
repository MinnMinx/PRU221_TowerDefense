using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField]
    private Tilemap placeableMap;

    private MapPreviewer previewer;
    private Camera mainCam;

    private void Awake()
    {
        previewer = new MapPreviewer(placeableMap);
        mainCam = Camera.main;
    }

    private void Update()
    {
        Vector3Int? hoverGrid = null;
        if (Input.mousePresent)
        {
            hoverGrid = placeableMap.WorldToCell(mainCam.ScreenToWorldPoint(Input.mousePosition));
        }
        previewer.Update(hoverGrid);
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
    }
}
