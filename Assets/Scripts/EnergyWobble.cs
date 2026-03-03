using UnityEngine;

public class EnergyWobble : MonoBehaviour
{
    [Tooltip("How fast the energy pulses.")]
    public float wobbleSpeed = 20f;

    [Tooltip("How much the size changes during the pulse.")]
    public float wobbleAmount = 0.05f;

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        // Uses a sine wave to smoothly but rapidly scale the aura up and down
        float wave = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        transform.localScale = startScale + new Vector3(wave, wave, 0);
    }
}
