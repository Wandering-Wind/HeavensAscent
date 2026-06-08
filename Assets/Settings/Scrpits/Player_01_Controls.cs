using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player_01_Controls : MonoBehaviour
{

    [Header("Shoot")]

    public GameObject Player_01_Soul;
    public GameObject P1_firePoint;
    public GameObject P1_firePoint_Arrow;
    [SerializeField] private float Shoot_power_P_01;
    [SerializeField] private float Min_Charge_power_P_01;
    [SerializeField] private float Max_Charge_power_P_01;
    [SerializeField] private float Charge = 10;

    public float currChargep1;

    private float currentCharge;
    private bool isCharging = false;

    public float Soul_Life_p1 = 5f;
    public float Max_SLP1 = 5f;

    private float currentSoulScale;
    private float originalSoulScale;

    public GameObject[] soulChargeIcons;
    public GameObject extraChargeObject;
    public TextMeshProUGUI extraChargeText;
    public int extraCharges;

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
    private Animator animator;

    [Header("Character Visuals")]
    public GameObject angelCharacter;
    public GameObject devilCharacter;

    [Header("Flip")]
    private bool facingRight;
    public GameObject flipTarget;

    [Header("Class")]
    public PlayerClassEnum selectedClass;
    public Class_Stats[] availableClasses;
    private Class_Stats currentClass;
    public bool isStunned = false;
    public float LoseMulti = 1f;
    public float SoulSizeMultiplier = 1f;

    [Header("VFX")]
    public GameObject hitVFX;
    public GameObject teleportVFX;
    public float vfxLifetime = 0.5f;
    public GameObject stunVFXPrefab;
    public Vector3 stunVFXOffset = new Vector3(0f, 1.5f, 0f);

    private GameObject activeStunVFX;


    private void Start()
    {
        selectedClass = SelectManager.Instance.player1Class;
        LoadClass(selectedClass);

        UpdateCharacterVisual();

        originalSoulScale = Soul_Life_p1 / Max_SLP1;
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
            print("Pl1Stun");
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
            currentCharge = Mathf.Clamp(currentCharge, Min_Charge_power_P_01, Max_Charge_power_P_01);

            float chargePercent = currentCharge / Max_Charge_power_P_01;
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
        /*if (collision.gameObject.CompareTag("Player_02_Soul") || collision.gameObject.CompareTag("P2"))
        {
            if (hitVFX != null)
            {
                Vector3 hitPos = transform.position;

                if (collision.contactCount > 0)
                    hitPos = collision.GetContact(0).point;

                GameObject vfx = Instantiate(hitVFX, hitPos, Quaternion.identity);
                vfx.transform.localScale = Vector3.one * 1.2f;
                Destroy(vfx, vfxLifetime);
            }
        }*/
        if (currentClass.classType == PlayerClassEnum.Devil)
        {
            Player_02_Controls p2 = collision.gameObject.GetComponent<Player_02_Controls>();

            if (p2 != null)
            {
                if (p2.currChargep2 > 0)
                {
                    print("Player 2 hit Player 1");

                    p2.currChargep2--;
                    currChargep1++;
                }

                StartCoroutine(StunOther(p2, 2f));
            }
        }
        else if (currentClass == null || currentClass.classType == PlayerClassEnum.Angel)
        {
            return;
        }
    }
    IEnumerator StunOther(Player_02_Controls target, float duration)
    {
        target.isStunned = true;
        target.ShowStunVFX();

        yield return new WaitForSeconds(duration);

        target.isStunned = false;
        target.HideStunVFX();
    }
    IEnumerator Regain()
    {
        float regainTime = 5f;

        if (selectedClass == PlayerClassEnum.Angel)
        {
            regainTime = 3f;
        }

        yield return new WaitForSeconds(regainTime);

        Soul_Life_p1 = Max_SLP1;
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
            UpdateSoulBar();
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

        SoulScrpit_01 owner1 = bullet_P_01.GetComponent<SoulScrpit_01>();
        owner1.player1 = this;

        bullet_P_01.transform.localScale = Vector3.one * currentSoulScale * SoulSizeMultiplier;
        currentSoulScale = (Soul_Life_p1 / Max_SLP1);
    }
    public void Teleport()
    {
        if (bullet_P_01 != null)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = bullet_P_01.transform.position;

            if (teleportVFX != null)
            {
                Destroy(Instantiate(teleportVFX, startPos, Quaternion.identity), vfxLifetime);
            }

            transform.position = endPos;

         /*   if (teleportVFX != null)
            {
                Destroy(Instantiate(teleportVFX, endPos, Quaternion.identity), vfxLifetime);
            }*/

            Rigidbody2D prb = Player_01.GetComponent<Rigidbody2D>();
            prb.linearVelocity = ShootDirection * currentCharge;

            Destroy(bullet_P_01);
            bullet_P_01 = null;
        }
    }
    public void Orb_Absorb()
    {
        if (currentClass.classType == PlayerClassEnum.Devil)
        {
            StartCoroutine(StunSelf(2f));
            return;
        }

        Max_SLP1++;
        Soul_Life_p1 = Max_SLP1;
        print(Max_SLP1);
        extraCharges++;
        UpdateSoulBar();
    }

    IEnumerator StunSelf(float duration)
    {
        isStunned = true;

        ShowStunVFX();

        yield return new WaitForSeconds(duration);

        isStunned = false;

        HideStunVFX();
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
        foreach(Class_Stats stats in availableClasses)
        {
            if(stats.classType == playC)
            {
                currentClass = stats;

                Max_SLP1 = stats.maxSoulLife;
                Soul_Life_p1 = stats.maxSoulLife;

                Min_Charge_power_P_01 = stats.minChargePower;
                Max_Charge_power_P_01 = stats.maxChargePower;

                Charge = stats.chargeSpeed;
                currChargep1 = stats.currCharge;
                break;
            }
        }
    }
    private void UpdateSoulBar()
    {
        int normalCharges = Mathf.Clamp((int)Soul_Life_p1, 0, 4);

        for (int i = 0; i < soulChargeIcons.Length; i++)
        {
            soulChargeIcons[i].SetActive(i < normalCharges);
        }

        if (extraCharges > 0)
        {
            extraChargeObject.SetActive(true);
            extraChargeText.text = "x" + extraCharges;
        }
        else
        {
            extraChargeObject.SetActive(false);
        }
    }
    private void UpdateCharacterVisual()
    {
        angelCharacter.SetActive(false);
        devilCharacter.SetActive(false);

        switch (selectedClass)
        {
            case PlayerClassEnum.Angel:
                angelCharacter.SetActive(true);
                animator = angelCharacter.GetComponent<Animator>();
                flipTarget = angelCharacter;
                break;

            case PlayerClassEnum.Devil:
                devilCharacter.SetActive(true);
                animator = devilCharacter.GetComponent<Animator>();
                flipTarget = devilCharacter;
                break;
        }
    }
    public void ShowStunVFX()
    {
        if (stunVFXPrefab == null || activeStunVFX != null)
            return;
         activeStunVFX = Instantiate(stunVFXPrefab,transform.position,Quaternion.identity,transform);
    }

    public void HideStunVFX()
    {
        if (activeStunVFX != null)
        {
            Destroy(activeStunVFX);
            activeStunVFX = null;
        }
    }
}



