using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] float moveSpeed, killTime;
    HitboxScript hitbox;
    private void Start()
    {
        if(killTime != 0) Invoke("destroyMe", killTime);
        hitbox = GetComponent<HitboxScript>();
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        if (hitbox.myHit.applied) destroyMe();
    }

    void destroyMe()
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Hitbox")
        {
            if (collision.gameObject.GetComponent<ProjectileScript>() != null) 
            {
                destroyMe();
            }
        }
    }
}
