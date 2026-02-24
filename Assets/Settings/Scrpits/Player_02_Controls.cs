using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player_02_Controls : MonoBehaviour
{

    [Header("Shoot")]

    public GameObject Player_02_Soul;
    public GameObject P2_firePoint;
    [SerializeField] private float Shoot_power_P_02;


    [Header("Aim")]
    public GameObject aimArrow;
    public float arrowDistance = 1f;
    public float deadzone = 0.2f;

    private Vector2 aimInput;
    private Vector2 lastAimDirection = Vector2.right;
    private GameObject bullet_P_02;


    private void Update()
    {
        if (aimInput.magnitude > deadzone)
        {
            lastAimDirection = aimInput.normalized;
        }

        // Rotate arrow
        float angle = Mathf.Atan2(lastAimDirection.y, lastAimDirection.x) * Mathf.Rad2Deg;
        aimArrow.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        // Position arrow in front of player
        aimArrow.transform.position = transform.position + (Vector3)(lastAimDirection * arrowDistance);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }
    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Teleport();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Shoot();
    }
    public void Shoot()
    {
        if (bullet_P_02 != null)
        {
            return;
        }
        bullet_P_02 = Instantiate(Player_02_Soul, P2_firePoint.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet_P_02.GetComponent<Rigidbody2D>();
        rb.linearVelocity = lastAimDirection * Shoot_power_P_02;

    }
    public void Teleport()
    {
        print("Asdndfsfljgb");
        if (bullet_P_02 != null)
        {
            transform.position = bullet_P_02.transform.position;
            Destroy(bullet_P_02);
            bullet_P_02  = null;
        }
    }
}


