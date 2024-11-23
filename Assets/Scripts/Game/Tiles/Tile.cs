using UnityEngine;

namespace Game.Tiles
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Tile : MonoBehaviour
    {
       public TileType TileType { get; private set; }
       
       public bool IsInteractable { get; private set; }
       
       public bool IsMatched{ get; private set; }

       public void SetType(TileType tileType)
       {
           TileType = tileType;
           IsInteractable = tileType.IsInteractable;
           IsMatched = false;
           GetComponent<SpriteRenderer>().sprite = tileType.Sprite;
       }

       public bool SetMatch(bool value) => IsMatched = value;

    }
}
