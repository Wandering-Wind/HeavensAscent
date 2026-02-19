using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Player_01_Controls : MonoBehaviour
{

    [Header("Movment")]




    public GameObject Player_01_Soul;
    public GameObject P1_firePoint;
    [SerializeField] private float Shoot_power_P_01;

    private float shootInput;
    private Vector2 moveInput;

    private void Update()
    {

    }

    public void OnMovment(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Shoot();
    }
    void Shoot()
    {
        GameObject bulletInstance = Instantiate(Player_01_Soul, P1_firePoint.transform.position, P1_firePoint.transform.rotation);


    }
}

