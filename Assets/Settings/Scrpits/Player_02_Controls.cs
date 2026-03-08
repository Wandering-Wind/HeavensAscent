using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player_02_Controls : MonoBehaviour
{

    [Header("Shoot")]

    public GameObject Player_02_Soul;
    public GameObject P2_firePoint;
    public GameObject P1_firePoint_Arrow;
    [SerializeField] private float Shoot_power_P_02;
    [SerializeField] private float Min_Charge_power_P_02;
    [SerializeField] private float Max_Charge_power_P_02;
    [SerializeField] private float Charge = 10f;

    private float currentCharge;
    private bool isCharging = false;

    public float Soul_Life_p2 = 5f;
    public float Max_SLP2 = 5f;

    private float currentSoulScale;
    private float originalSoulScale;

    [Header("Aim")]
    public GameObject aimArrow;
    public float arrowDistance = 1f;
    public float deadzone = 0.2f;

    private Vector2 aimInput;
    private Vector2 lastAimDirection = Vector2.right;
    private Vector2 ShootDirection;
    private GameObject bullet_P_02;

    public GameObject Player_02;

    [Header("Slow")]
    public float slowFactor = 0.4f;
    private Rigidbody2D rb;
    private float originalGravity;
    [Header("Animatiom")]
    public Animator animator;

    [Header("Flip")]
    private bool facingRight;
    public GameObject flipTarget;


    private void Start()
    {
        originalSoulScale = Soul_Life_p2 / Max_SLP2;
        currentSoulScale = originalSoulScale;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        facingRight = true;
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
            currentCharge = Mathf.Clamp(currentCharge, Min_Charge_power_P_02, Max_Charge_power_P_02);

            float chargePercent = currentCharge / Max_Charge_power_P_02;
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
        Soul_Life_p2 = Max_SLP2;
        currentSoulScale = originalSoulScale;
    }
    public void OnAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();

        if (aimInput.x > deadzone && !facingRight)
        {
            Flip();
        }
        else if (aimInput.x < -deadzone && facingRight)
        {
            Flip();
        }
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
            if (Soul_Life_p2 > 0 && bullet_P_02 == null)
            {
                isCharging = true;
                currentCharge = Min_Charge_power_P_02;
                animator.SetInteger("Shoot", 1);
            }
            else if (Soul_Life_p2 <= 0)
            {
                StartCoroutine(Regain());
            }
        }
        if (context.canceled && isCharging)
        {
            isCharging = false;
            animator.SetInteger("Shoot", 2);
            Shoot(currentCharge);
            Soul_Life_p2 -= 1;
        }
    }
    public void Shoot(float SP)
    {
        if (bullet_P_02 != null)
        {
            return;
        }
        bullet_P_02 = Instantiate(Player_02_Soul, P2_firePoint.transform.position, Quaternion.identity);
        Rigidbody2D rb = bullet_P_02.GetComponent<Rigidbody2D>();
        rb.linearVelocity = lastAimDirection * currentCharge;
        ShootDirection = lastAimDirection;
        rb.linearVelocity = ShootDirection * SP;

        Soul_Scrpit owner = bullet_P_02.AddComponent<Soul_Scrpit>();
        owner.player2 = this;

        bullet_P_02.transform.localScale = Vector3.one * currentSoulScale;
        currentSoulScale = (Soul_Life_p2 / Max_SLP2);
    }
    public void Teleport()
    {
        print("Asdndfsfljgb");
        if (bullet_P_02 != null)
        {
            transform.position = bullet_P_02.transform.position;
            Rigidbody2D prb = Player_02.GetComponent<Rigidbody2D>();
            prb.linearVelocity = ShootDirection * currentCharge;
            Destroy(bullet_P_02);
            bullet_P_02 = null;
        }
    }
    public void Orb_Absorb()
    {
        Soul_Life_p2 = Max_SLP2;
        currentSoulScale = 1f;
    }

    public void Flip() //Youtu.be. (2026). Available at: https://youtu.be/Cr-j7EoM8bg?si=IjMERP-pLs5SwuNJ [Accessed 8 Mar. 2026].
    {
        Vector3 currentScale = flipTarget.transform.localScale;
        currentScale.x *= -1;
        flipTarget.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}