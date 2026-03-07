using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player_01_Controls : MonoBehaviour
{

    [Header("Shoot")]

    public GameObject Player_01_Soul;
    public GameObject P1_firePoint;
    public GameObject P1_firePoint_Arrow;
    [SerializeField] private float Shoot_power_P_01;
    [SerializeField] private float Min_Charge_power_P_01;
    [SerializeField] private float Max_Charge_power_P_01;
    [SerializeField] private float Charge = 10f;

    private float currentCharge;
    private bool isCharging = false;

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

    [Header("Slow")]
    public float slowFactor = 0.05f;
    private Rigidbody2D rb;
    private float originalGravity;

    [Header("Animatiom")]
    public Animator animator;


    private void Start()
    {
        originalSoulScale = Soul_Life_p1 / Max_SLP1;
        currentSoulScale = originalSoulScale;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
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


        if (isCharging)
        {

            rb.linearVelocity *= slowFactor;
            rb.gravityScale = originalGravity * slowFactor;
            currentCharge += Charge * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, Min_Charge_power_P_01, Max_Charge_power_P_01);

            float chargePercent = currentCharge / Max_Charge_power_P_01;
            float scaleX = Mathf.Lerp(1f, 1.8f, chargePercent);
            P1_firePoint_Arrow.transform.localScale = new Vector3(scaleX, 1f, 1f);
        }
        else
        {
            //animator.SetInteger("Shoot", 0);
            rb.gravityScale = originalGravity;
            P1_firePoint_Arrow.transform.localScale = Vector3.one;
        }
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
        if (context.started)
        {
            if (Soul_Life_p1 > 0 && bullet_P_01 == null)
            {
                isCharging = true;
                currentCharge = Min_Charge_power_P_01;
                animator.SetInteger("Shoot", 1);
            }
            else if (Soul_Life_p1 <= 0)
            {
                StartCoroutine(Regain());
            }
        }
        if (context.canceled && isCharging)
        {
            isCharging = false;
            animator.SetInteger("Shoot", 2);
            Shoot(currentCharge);
            Soul_Life_p1 -= 1;
        }
    }
    public void Shoot(float SP)
    {
        if (bullet_P_01 != null)
        {
            return;
        }
        bullet_P_01 = Instantiate(Player_01_Soul, P1_firePoint.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet_P_01.GetComponent<Rigidbody2D>();
        rb.linearVelocity = lastAimDirection * currentCharge;
        ShootDirection = lastAimDirection;
        rb.linearVelocity = ShootDirection * SP;

        SoulScrpit_01 owner1 = bullet_P_01.AddComponent<SoulScrpit_01>();
        owner1.player1 = this;

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
            prb.linearVelocity = ShootDirection * currentCharge;
            Destroy(bullet_P_01);
            bullet_P_01   = null;
        }
    }
    public void Orb_Absorb()
    {
        Soul_Life_p1 = Max_SLP1;
        currentSoulScale = 1f;
    }
}



