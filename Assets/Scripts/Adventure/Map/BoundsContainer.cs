using NaughtyAttributes;
using UnityEngine;

public class BoundsContainer : MonoBehaviour {
    [OnValueChanged(nameof(UpdateSpriteRenderers))][SerializeField] bool setVisible = true;

    void UpdateSpriteRenderers() {
        GetSpriteRenderers(transform);
    }

    void GetSpriteRenderers(Transform transform) {
        for (int i = 0; i < transform.childCount; i++) {
            Transform child = transform.GetChild(i);
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) {
                Color c = spriteRenderer.color;
                spriteRenderer.color = new Color(c.r, c.g, c.g, setVisible ? 0.5f : 0f);
            }
            if (child.childCount > 0) GetSpriteRenderers(child);
        }
    }
}
