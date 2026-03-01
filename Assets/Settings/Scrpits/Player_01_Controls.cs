using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player_01_Controls : MonoBehaviour
{

    [Header("Shoot")]

    public GameObject Player_01_Soul;
    public GameObject P1_firePoint;
    [SerializeField] private float Shoot_power_P_01;

    public float Soul_Life_p1 = 5f;
    public float Max_SLP1 = 5f;

    private float currentSoulScale;
    private float originalSoulScale;


    [Header("Aim")]
    public GameObject aimArrow;
    public float arrowDistance = 1f;
    public float deadzone = 0.2f;

    private Vector2 aimInput;
    private Vector2 lastAimDirection = Vector2.right;
    private Vector2 ShootDirection;
    private GameObject bullet_P_01;

    public GameObject Player_01;


    private void Start()
    {
        originalSoulScale = Soul_Life_p1/Max_SLP1;
        currentSoulScale = originalSoulScale;
    }
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
    IEnumerator Regain()
    {
        yield return new WaitForSeconds(5);
        Soul_Life_p1 = Max_SLP1;
        currentSoulScale = originalSoulScale;
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
        if (Soul_Life_p1 > 0)
        {
            Shoot();
            Soul_Life_p1 -= 1;
        }
        else if(Soul_Life_p1 <= 0)
        {
            StartCoroutine(Regain());
        }
    }
    public void Shoot()
    {
        if (bullet_P_01 != null){
            return;
        }

        bullet_P_01 = Instantiate(Player_01_Soul, P1_firePoint.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet_P_01.GetComponent<Rigidbody2D>();
        ShootDirection = lastAimDirection;
        rb.linearVelocity = ShootDirection * Shoot_power_P_01;

        bullet_P_01.transform.localScale = Vector3.one * currentSoulScale;
        currentSoulScale = (Soul_Life_p1 / Max_SLP1);

    }
    public void Teleport()
    {
        print("Asdndfsfljgb");
        if (bullet_P_01 != null)
        {
            transform.position = bullet_P_01.transform.position;
            Rigidbody2D prb = Player_01.GetComponent<Rigidbody2D>();
            prb.linearVelocity = ShootDirection * Shoot_power_P_01;
            Destroy(bullet_P_01);
            bullet_P_01 = null;
        }
    }
    public void Orb_Absorb()
    {
        Soul_Life_p1 = Max_SLP1;
        currentSoulScale = originalSoulScale;
    }
}


