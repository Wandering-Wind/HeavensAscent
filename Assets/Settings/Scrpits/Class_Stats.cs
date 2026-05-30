using UnityEngine;

[System.Serializable]
public class Class_Stats
{
    public PlayerClassEnum classType;

    [Header("Soul")]
    public float maxSoulLife;

    [Header("Movement")]
    public float moveSpeed;

    [Header("Shooting")]
    public float minChargePower;
    public float maxChargePower;
    public float chargeSpeed;

    [Header("Special")]
    public float teleportBoost;
}
