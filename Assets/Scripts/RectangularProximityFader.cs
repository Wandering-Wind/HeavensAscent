using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RectangularProximityFader : MonoBehaviour
{
    [Header("Player Targets")]
    public Transform player1;
    public Transform player2;

    [Header("Rectangular Detection Settings")]
    [Tooltip("The width and height of the core wall where opacity will be at maximum.")]
    public Vector2 coreWallSize = new Vector2(2f, 15f);

    [Tooltip("How far from the wall's edge the player needs to be to start the fade effect.")]
    public float fadeDistance = 5f;

    [Tooltip("The maximum opacity the wall will reach (0 to 1).")]
    [Range(0f, 1f)]
    public float maxAlpha = 0.8f;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetAlpha(0f);
    }

    void Update()
    {
        if (player1 == null || player2 == null) return;

        // Calculate distance from both players to the rectangular edge
        float dist1 = GetDistanceToBoxEdge(player1.position);
        float dist2 = GetDistanceToBoxEdge(player2.position);

        // We only care about the player who is closest to the wall
        float closestDistance = Mathf.Min(dist1, dist2);

        // If the closest player is within the fade padding, calculate the fade
        if (closestDistance < fadeDistance)
        {
            // Percentage: 0 distance (touching) = 1. Max distance (edge of padding) = 0.
            float fadePercentage = 1f - (closestDistance / fadeDistance);

            float finalAlpha = fadePercentage * maxAlpha;
            SetAlpha(finalAlpha);
        }
        else
        {
            SetAlpha(0f);
        }
    }

    // Mathematical helper to find the exact distance from a point to a 2D box edge
    private float GetDistanceToBoxEdge(Vector2 playerPos)
    {
        Vector2 center = transform.position;

        // Calculate the absolute distance from the center on both axes, minus half the box size
        float distanceX = Mathf.Max(Mathf.Abs(playerPos.x - center.x) - (coreWallSize.x / 2f), 0);
        float distanceY = Mathf.Max(Mathf.Abs(playerPos.y - center.y) - (coreWallSize.y / 2f), 0);

        // Use Pythagoras to get the final distance (this automatically handles corners perfectly)
        return new Vector2(distanceX, distanceY).magnitude;
    }

    private void SetAlpha(float alphaValue)
    {
        Color color = spriteRenderer.color;
        color.a = alphaValue;
        spriteRenderer.color = color;
    }

    // Draws helpful boxes in the editor
    private void OnDrawGizmosSelected()
    {
        // 1. Draw the Core Wall (Solid Red)
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawCube(transform.position, coreWallSize);

        // 2. Draw the Fade Approach Area (Wireframe Yellow)
        // We expand the core box by the fade distance on all sides to show where the detection begins
        Gizmos.color = Color.yellow;
        Vector2 fadeAreaSize = new Vector2(coreWallSize.x + (fadeDistance * 2), coreWallSize.y + (fadeDistance * 2));
        Gizmos.DrawWireCube(transform.position, fadeAreaSize);
    }
}