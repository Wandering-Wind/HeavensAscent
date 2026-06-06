using UnityEngine;

public class CloudScroller : MonoBehaviour
{
    [Tooltip("How fast the background moves to the left.")]
    public float speed = 2f;

    [Tooltip("The exact X position on the left where the cloud should teleport.")]
    public float despawnX = -10f;

    [Header("Loop Settings")]
    [Tooltip("How many total cloud sprites make up this scrolling line?")]
    public int totalClouds = 4; // Set this to however many clouds you have!

    private float spriteWidth;

    void Start()
    {
        // Grab the exact width of the sprite in Unity Units
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        // Move the cloud left over time
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // If the cloud crosses the invisible despawn line on the left
        if (transform.position.x <= despawnX)
        {
            // Snap it precisely to the back of the line based on the total number of clouds
            transform.position = new Vector3(transform.position.x + (spriteWidth * totalClouds), transform.position.y, transform.position.z);
        }
    }
}