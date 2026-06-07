using UnityEngine;

public class DynamicCameraController : MonoBehaviour
{
    [Header("Player Targets")]
    public Transform player1;
    public Transform player2;

    [Header("Camera Dynamics")]
    public Vector3 offset = new Vector3(0f, 2f, -10f);
    public float smoothTime = 0.2f;

    [Header("Zoom Settings (Orthographic)")]
    public float minZoom = 5f;
    public float maxZoom = 12f;
    public float zoomLimiter = 10f;

    [Header("Level Boundaries")]
    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Vector3 velocity = Vector3.zero;
    public Camera cam;

    void Start()
    {

        if (!cam.orthographic)
        {
            Debug.LogWarning("DynamicCameraController: Main Camera is not set to Orthographic!");
        }
    }

    void LateUpdate()
    {
        if (player1 == null || player2 == null) return;

        // Calculate zoom first so we know exact camera dimensions for this frame
        ZoomCamera();
        MoveCamera();
    }

    void ZoomCamera()
    {
        float distance = Vector3.Distance(player1.position, player2.position);
        float targetZoom = Mathf.Lerp(minZoom, maxZoom, distance / zoomLimiter);

        // --- SAFETY CHECK 1: BOUNDARY-BASED ZOOM CEILING ---
        // Calculate the maximum height and width of the level box
        float levelHeight = maxBounds.y - minBounds.y;
        float levelWidth = maxBounds.x - minBounds.x;

        // The absolute maximum orthographic size that can fit within these bounds
        float maxVerticalZoom = levelHeight / 2f;
        float maxHorizontalZoom = levelWidth / (2f * cam.aspect);

        // Pick the restrictive dimension so the camera never sees past the art borders
        float absoluteMaxZoom = Mathf.Min(maxVerticalZoom, maxHorizontalZoom);

        // Clamp the target zoom before applying it
        targetZoom = Mathf.Clamp(targetZoom, minZoom, absoluteMaxZoom);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime / smoothTime);
    }

    void MoveCamera()
    {
        Vector3 centerPoint = (player1.position + player2.position) / 2f;
        Vector3 targetPosition = centerPoint + offset;

        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float clampedX, clampedY;

        // --- SAFETY CHECK 2: FLIPPED RANGE PROTECTION ---
        float minX = minBounds.x + camWidth;
        float maxX = maxBounds.x - camWidth;

        // If the camera width exceeds level width, lock to the center of the level horizontally
        if (minX > maxX)
        {
            clampedX = (minBounds.x + maxBounds.x) / 2f;
        }
        else
        {
            clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);
        }

        float minY = minBounds.y + camHeight;
        float maxY = maxBounds.y - camHeight;

        // If the camera height exceeds level height, lock to the center of the level vertically
        if (minY > maxY)
        {
            clampedY = (minBounds.y + maxBounds.y) / 2f;
        }
        else
        {
            clampedY = Mathf.Clamp(targetPosition.y, minY, maxY);
        }

        Vector3 boundPosition = new Vector3(clampedX, clampedY, targetPosition.z);
        transform.position = Vector3.SmoothDamp(transform.position, boundPosition, ref velocity, smoothTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2f, (minBounds.y + maxBounds.y) / 2f, 0f);
        Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 1f);
        Gizmos.DrawWireCube(center, size);
    }
}