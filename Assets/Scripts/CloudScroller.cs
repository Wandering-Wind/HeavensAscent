using UnityEngine;


public class CloudScroller : MonoBehaviour
{
    [Tooltip("How fast the background moves to the left.")]
    public float speed = 2f;

    [Tooltip("The exact X position on the left where the cloud should teleport.")]
    public float despawnX = -10f;

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
            // Snap it precisely to the back of the 3-piece line on the right
            transform.position = new Vector3(transform.position.x + (spriteWidth * 3f), transform.position.y, transform.position.z);
        }
    }
}