using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;
using UnityEngine.UI;
using TMPro;

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

    public float currChargep2;

    private float currentCharge;
    private bool isCharging = false;

    public float Soul_Life_p2 = 5f;
    public float Max_SLP2 = 5f;

    private float currentSoulScale;
    private float originalSoulScale;

    public TextMeshProUGUI soulText;

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

    [Header("Class")]
    public PlayerClassEnum selectedClass;
    public Class_Stats[] availableClasses;
    private Class_Stats currentClass;
    public bool isStunned;
    public float LoseMulti = 1f;
    public float SoulSizeMultiplier = 1f;


    private void Start()
    {

        selectedClass = SelectManager.Instance.player2Class;
        LoadClass(selectedClass);

        originalSoulScale = Soul_Life_p2 / Max_SLP2;
        currentSoulScale = originalSoulScale;
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        facingRight = true;
        UpdateSoulBar();
    }
    private void Update()
    {
        if (isStunned)
        {
            print("Pl2 Stun");
            return;
        }
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
            currentCharge += Charge* LoseMulti * Time.deltaTime;
            currentCharge = Mathf.Clamp(currentCharge, Min_Charge_power_P_02, Max_Charge_power_P_02);

            float chargePercent = currentCharge / Max_Charge_power_P_02;
            float scaleX = Mathf.Lerp(1.5f, 1.8f, chargePercent);
            P1_firePoint_Arrow.transform.localScale = new Vector3(scaleX, 1.5f, 1.5f);
        }
        else
        {
            //animator.SetInteger("Shoot", 0);
            rb.gravityScale = originalGravity;
            P1_firePoint_Arrow.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player_01_Soul") || collision.gameObject.CompareTag("P1"))
        {
            float impactSpeed = collision.relativeVelocity.magnitude;
            float intensity = Mathf.Clamp(impactSpeed * 0.02f, 0.05f, 0.7f);

            if (CameraShaking.Instance != null)
            {
                CameraShaking.Instance.Shake(0.2f, intensity);
            }


            if (currentClass.classType == PlayerClassEnum.Devil)
            {
                Player_01_Controls p1 =
                    collision.gameObject.GetComponent<Player_01_Controls>();

                if (p1 != null)
                {
                    if (p1.currChargep1 > 0)
                    {
                        print("Player 1 hit Player 2");

                        p1.currChargep1--;
                        currChargep2++;
                    }

                    StartCoroutine(StunOther(p1, 2f));
                }
            }
            else if (currentClass == null || currentClass.classType == PlayerClassEnum.Angel)
            {
                return;
            }
        }
    }

    IEnumerator StunOther(Player_01_Controls target, float duration)
    {
        target.isStunned = true;

        yield return new WaitForSeconds(duration);

        target.isStunned = false;
    }
    IEnumerator Regain()
    {
        yield return new WaitForSeconds(5);
        Soul_Life_p2 = Max_SLP2;
        currentSoulScale = originalSoulScale;
        UpdateSoulBar();
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
        if (isStunned)
        {
            print("Pl1Stun");
            return;
        }
        if (!context.performed) return;
        Teleport();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (isStunned)
        {
            print("Pl1Stun");
            return;
        }
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
            UpdateSoulBar();
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

        bullet_P_02.transform.localScale = Vector3.one * currentSoulScale *  SoulSizeMultiplier;
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
        if (currentClass.classType == PlayerClassEnum.Devil)
        {
            StartCoroutine(StunSelf(2f));
            return;
        }

        if (currentClass.classType == PlayerClassEnum.Angel)
        {
            Max_SLP2 += 1;
            Soul_Life_p2 = Max_SLP2;
            currentSoulScale = 1f;
            UpdateSoulBar();
        }
    }

    IEnumerator StunSelf(float duration)
    {
        isStunned = true;

        yield return new WaitForSeconds(duration);

        isStunned = false;
    }

    public void Flip() //Youtu.be. (2026). Available at: https://youtu.be/Cr-j7EoM8bg?si=IjMERP-pLs5SwuNJ [Accessed 8 Mar. 2026].
    {
        Vector3 currentScale = flipTarget.transform.localScale;
        currentScale.x *= -1;
        flipTarget.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
    private void LoadClass(PlayerClassEnum playC)
    {
        foreach (Class_Stats stats in availableClasses)
        {
            if (stats.classType == playC)
            {
                currentClass = stats;

                Max_SLP2 = stats.maxSoulLife;
                Soul_Life_p2 = stats.maxSoulLife;

                Min_Charge_power_P_02 = stats.minChargePower;
                Max_Charge_power_P_02 = stats.maxChargePower;

                stats.currCharge = stats.maxChargePower;
                Charge = stats.chargeSpeed;
                currChargep2 = stats.currCharge;

                break;
            }
        }
    }
    private void UpdateSoulBar()
    {
        soulText.text = Soul_Life_p2.ToString();
    }
}