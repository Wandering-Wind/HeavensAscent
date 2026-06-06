using UnityEngine;

// This struct bundles the layer and its settings together so it looks clean in the Inspector.
[System.Serializable]
public class ParallaxLayerData
{
    [Tooltip("Drag the background layer GameObject here.")]
    public Transform layerTransform;

    [Tooltip("0 = Static. 1 = Moves with camera. E.g., X: 0.8, Y: 0.1 for far backgrounds.")]
    public Vector2 parallaxMultiplier;

    // We hide this because the script calculates it automatically; you don't need to see it.
    [HideInInspector]
    public Vector3 startPosition;
}

public class ParallaxManager : MonoBehaviour
{
    [Header("Assign Layers Here")]
    [Tooltip("Add all the layers you want to apply parallax to.")]
    public ParallaxLayerData[] layers;

    public Transform cam;
    private Vector3 startCameraPosition;

    void Start()
    {
        // Cache camera starting position
       // cam = Camera.main.transform;
        startCameraPosition = cam.position;

        // Loop through the list and record the starting position of every assigned layer
        foreach (ParallaxLayerData layer in layers)
        {
            if (layer.layerTransform != null)
            {
                layer.startPosition = layer.layerTransform.position;
            }
            else
            {
                Debug.LogWarning("Parallax Manager: A layer transform is missing!");
            }
        }
    }

    void LateUpdate()
    {
        // Calculate camera movement once per frame
        Vector3 distanceMoved = cam.position - startCameraPosition;

        // Apply the movement to every layer in the list based on its specific multiplier
        foreach (ParallaxLayerData layer in layers)
        {
            if (layer.layerTransform != null)
            {
                float newX = layer.startPosition.x + (distanceMoved.x * layer.parallaxMultiplier.x);
                float newY = layer.startPosition.y + (distanceMoved.y * layer.parallaxMultiplier.y);

                layer.layerTransform.position = new Vector3(newX, newY, layer.layerTransform.position.z);
            }
        }
    }
}