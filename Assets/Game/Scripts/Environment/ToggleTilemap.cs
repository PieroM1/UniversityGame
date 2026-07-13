using UnityEngine;
using UnityEngine.Tilemaps;

public class ToggleTilemap : MonoBehaviour
{
    public enum TilemapColor { Primary, Secondary }
    public TilemapColor type;
    private Tilemap tilemap;
    private TilemapCollider2D col;
    [SerializeField] private Tile activeTile;
    [SerializeField] private Tile inactiveTile;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        col = GetComponent<TilemapCollider2D>();
    }

    private void OnEnable()
    {
        ToggleBlockManager.OnToggleBlocks += OnToggle;
    }

    private void OnDisable()
    {
        ToggleBlockManager.OnToggleBlocks -= OnToggle;
    }

    private void OnToggle(bool primaryIsActive)
    {
        bool isActive = (type == TilemapColor.Primary) ? primaryIsActive : !primaryIsActive;
        ReplaceTiles(isActive);
        ToggleCollider(isActive);
        if (col != null)
            col.enabled = isActive;
    }

    private void ReplaceTiles(bool active)
    {
        Tile replacement = active ? activeTile : inactiveTile;

        foreach (var pos in tilemap.cellBounds.allPositionsWithin)
        {
            if (!tilemap.HasTile(pos)) continue;

            tilemap.SetTile(pos, replacement);
        }
    }

    private void ToggleCollider(bool active)
    {
        var collider = GetComponent<TilemapCollider2D>();
        if (collider != null) collider.enabled = active;
    }
}