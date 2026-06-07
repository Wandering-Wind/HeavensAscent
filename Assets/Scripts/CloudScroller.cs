using UnityEngine;

public class CloudScroller : MonoBehaviour
{
    public float speed = 2f;
    public int totalClouds = 4;
    public float offscreenPadding = 2f;

    [Header("Polish")]
    [Tooltip("Forces the clouds to overlap slightly to hide rendering gaps.")]
    public float gapCorrection = 0.02f;

    private float spriteWidth;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        float camLeftEdge = cam.transform.position.x - (cam.orthographicSize * cam.aspect);
        float cloudRightEdge = transform.position.x + (spriteWidth / 2f);

        if (cloudRightEdge < (camLeftEdge - offscreenPadding))
        {
            // We subtract the gapCorrection so the newly teleported cloud bites slightly into the one in front of it
            float newX = transform.position.x + (spriteWidth * totalClouds) - gapCorrection;
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
        }
    }
}