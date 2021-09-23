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
        if (playerScript.leftOfTarget)
        {
            buildTheProjectile(false);
        }
        else 
        {
            buildTheProjectile(true);
        }
    }

    void buildTheProjectile(bool reverse)
    {
        GameObject newProjectile;
        if (!reverse)
        {
            newProjectile = Instantiate(projectile, instPoint.position, instPoint.rotation);
        }
        else
        {
            newProjectile = Instantiate(projectile, instPoint.position, instPointInverse.rotation);
        }
        HitboxScript hitbox;
        hitbox = newProjectile.GetComponent<HitboxScript>();
        hitbox.myHit.P2 = playerScript.P2;
    }
}
