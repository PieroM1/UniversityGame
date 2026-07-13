using UnityEngine;

public class ToggleBlock : MonoBehaviour
{
    private SpriteRenderer sr;
    private Collider2D col;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        ToggleBlockManager.OnToggleBlocks += SwitchState;
    }

    private void OnDisable()
    {
        ToggleBlockManager.OnToggleBlocks -= SwitchState;
    }

    private void SwitchState(bool isActive)
    {
        sr.sprite = isActive ? activeSprite : inactiveSprite;
        col.enabled = isActive;
    }
}
