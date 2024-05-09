using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HorizontalSpin
{
    public class HorizontalSpin : MonoBehaviour
    {
        [SerializeField] private List<ItemData> _items;
        [SerializeField] private List<Tile> _tiles;

        private float durationDeflate = 0.3f;
        private float durationAppearance = 0.2f;
        private Vector3 _force = new Vector3(1.2f, 1.2f);
        
        private void Start() => 
            InitTiles();

        public void Spin(Action<ItemData> onEnd)
        {
            RegenerateItems();
            AppearanceAnimation(_tiles, (() => { FindCombinations((item => onEnd?.Invoke(item))); }));
        }

        private void InitTiles() => RegenerateItems();

        private void RegenerateItems()
        {
            foreach (var tile in _tiles)
            {
                tile.SetItem(_items[Random.Range(0, _items.Count)]);
                tile.UpdateIcon();
            }
        }

        private void FindCombinations(Action<ItemData> onEnd)
        {
            if (AllTilesNotRepeating(_tiles))
                ToAnimateCombinations(_tiles, (() => onEnd?.Invoke(_tiles.First().ItemData)));
            else
                ToAnimateCombinations(_tiles, (() => onEnd?.Invoke(null)));
        }

        private bool AllTilesNotRepeating(List<Tile> tiles)
        {
            var firstTile = tiles.FirstOrDefault();
            return firstTile != null && tiles.All(tile => tile.ItemData == firstTile.ItemData);
        }

        private void ToAnimateCombinations(List<Tile> tiles, Action onEnd)
        {
            var sequence = DOTween.Sequence();

            InitDeflateAnimation(sequence, tiles);

            sequence.AppendCallback(() => { onEnd?.Invoke(); });

            sequence.Play();
        }

        private void InitDeflateAnimation(Sequence sequence, List<Tile> tiles)
        {
            var sequenceTiles = DOTween.Sequence();

            foreach (var tile in tiles)
            {
                sequenceTiles.Join(tile.GetIcon().transform.DOPunchScale(_force, durationDeflate, 1)
                    .SetLoops(2, LoopType.Yoyo));
            }

            sequence.Append(sequenceTiles);
        }

        private void AppearanceAnimation(List<Tile> tiles, Action onEnd)
        {
            var sequence = DOTween.Sequence();

            foreach (var tile in tiles)
            {
                tile.GetIcon().transform.localScale = Vector3.zero;

                sequence.Append(
                    tile.GetIcon().transform.DOScale(Vector3.one, durationAppearance)
                        .SetEase(Ease.InOutBack)
                );
            }

            sequence.OnComplete(() => onEnd?.Invoke());
        }
    }
}