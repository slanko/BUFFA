using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour
{
    public enum attackHeight
    {
        LOW,
        MID,
        HIGH,
        SWEEP
    }
    public bool applied, launch, P2, destroyObject;
    public float xChangeAmount, damage;
    public attackHeight height;
}
