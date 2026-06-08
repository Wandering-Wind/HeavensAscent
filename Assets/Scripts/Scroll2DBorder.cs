using UnityEngine;

public class Scroll2DBorder : MonoBehaviour
{
    [SerializeField] private float scrollSpeedX = 0.2f;
    [SerializeField] private float scrollSpeedY = 0.2f;

    private Material borderMaterial;
    private Vector2 currentOffset = Vector2.zero;

    void Start()
    {
        // Target the 2D Sprite Renderer directly
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            borderMaterial = spriteRenderer.material;
        }
        else
        {
            Debug.LogError("Scroll2DBorder requires a SpriteRenderer component on this object!");
        }
    }

    void Update()
    {
        if (borderMaterial == null) return;

        // Smoothly update coordinates over time
        currentOffset.x += scrollSpeedX * Time.deltaTime;
        currentOffset.y += scrollSpeedY * Time.deltaTime;

        // Shift the texture coordinates inside the 2D material
        borderMaterial.mainTextureOffset = currentOffset;
    }
}