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
    public HitboxHandler.hitboxStruct myHit;

    private void FixedUpdate()
    {
        if(myHit.applied && myHit.hitrate != 0)
        {
            StartCoroutine(unApplySelf());
        }
    }

    IEnumerator unApplySelf()
    {
        for(int i = 0; i < myHit.hitrate; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        myHit.applied = false;
    }
}
