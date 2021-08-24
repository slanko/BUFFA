using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstantiator : MonoBehaviour
{
    [SerializeField] UniversalPlayerScript playerScript;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform instPoint;

    public void instProjectile()
    {
        if (playerScript.leftOfTarget) Instantiate(projectile, instPoint.position, instPoint.rotation);
        else Instantiate(projectile, instPoint.position, Quaternion.Inverse(instPoint.rotation));
    }

    void FixedUpdate()
    {
    }
}
