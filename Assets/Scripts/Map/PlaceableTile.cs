using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "PlaceableTile", menuName = "Tilemap/Placeable tile", order = 5)]
public class PlaceableTile : TileBase
{
    public Sprite _displaySprite;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        // base.GetTileData(position, tilemap, ref tileData);
        //tileData.color = new Color
        //{
        //    r = 1,
        //    b = 1,
        //    g = 1,
        //    a = 0.5f,
        //};
        tileData.sprite = _displaySprite;
    }
}
