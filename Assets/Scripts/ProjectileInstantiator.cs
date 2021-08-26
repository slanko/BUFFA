using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstantiator : MonoBehaviour
{
    [SerializeField] UniversalPlayerScript playerScript;
    [SerializeField] GameObject projectile;
    [SerializeField] Transform instPoint, instPointInverse;

    public void instProjectile()
    {
        if (playerScript.leftOfTarget) Instantiate(projectile, instPoint.position, instPoint.rotation);
        else Instantiate(projectile, instPoint.position, instPointInverse.rotation);
    }
}
