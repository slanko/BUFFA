using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInstantiator : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] Transform instPoint;

    public void instProjectile()
    {
        Instantiate(projectile, instPoint.position, instPoint.rotation);
    }
}
