using UnityEngine;

namespace HorizontalSpin
{
    public class Row : MonoBehaviour
    {
        [SerializeField] private Tile[] _tiles;

        public Tile[] GetTiles() => 
            _tiles;

        public void SetTiles(Tile[] tiles) => 
            _tiles = tiles;
    }
}