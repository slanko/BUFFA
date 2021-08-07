using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] float moveSpeed, killTime;
    private void Start()
    {
        if(killTime != 0) Invoke("destroyMe", killTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void destroyMe()
    {
        Destroy(this.gameObject);
    }
}
