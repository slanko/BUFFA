using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxHandler : MonoBehaviour
{
    public LayerMask hitboxLayer;
    public Color hitboxColour;
    [SerializeField] UniversalPlayerScript playerScript;
    [System.Serializable]
    public struct hitboxStruct
    {
        public bool active;
        public Vector2 pos, scale;
        public HitboxScript.attackHeight height;
        public bool launch;
        public float hitrate, xChangeAmount, damage, chipDamage;
        public bool P2, applied; // have to keep this applied bool for stuff like projectiles (but maybe they could be their own struct eventually?
    }

    [SerializeField, Header("Hitboxes")] hitboxStruct hitbox1;
    [SerializeField] hitboxStruct hitbox2, hitbox3;
    [SerializeField] bool box1Applied, box2Applied, box3Applied;


    private void Start()
    {
        playerScript = GetComponentInParent<UniversalPlayerScript>();
    }

    #region HITTIN'
    void makeHitbox(hitboxStruct hitbox, int hitboxNum)
    {
        hitbox.P2 = playerScript.P2;
        if (hitbox.active)
        {
            //chuck out a cube area and check everything in it.
            Collider[] hitColliders;
            if (playerScript.leftOfTarget)
            {
                hitColliders = Physics.OverlapBox(transform.position + new Vector3(hitbox.pos.x, hitbox.pos.y, 0), new Vector3(hitbox.scale.x, hitbox.scale.y, 1) / 2, Quaternion.identity, hitboxLayer);
            }
            else
            {
                hitColliders = Physics.OverlapBox(transform.position - new Vector3(hitbox.pos.x, -hitbox.pos.y, 0), new Vector3(hitbox.scale.x, hitbox.scale.y, 1) / 2, Quaternion.identity, hitboxLayer);
            }
            foreach(Collider col in hitColliders)
            {
                UniversalPlayerScript otherPlayerScriptIHope;
                otherPlayerScriptIHope = col.GetComponentInParent<UniversalPlayerScript>();
                if (otherPlayerScriptIHope != null)
                {
                    if (otherPlayerScriptIHope.P2 != playerScript.P2)
                    {
                        Debug.Log("Hit applied: " + hitbox);
                        otherPlayerScriptIHope.GetHit(hitbox);
                        switch (hitboxNum)
                        {
                            case 1:
                                box1Applied = true;
                                break;
                            case 2:
                                box2Applied = true;
                                break;
                            case 3:
                                box3Applied = true;
                                break;
                        }
                        break;
                    }
                }
                else Debug.Log("NAH");
            }
        }
    }

    void visualizeHitbox(hitboxStruct hitbox)
    {
        if(playerScript.leftOfTarget) Gizmos.DrawCube(transform.position + new Vector3(hitbox.pos.x, hitbox.pos.y, 0), new Vector3(hitbox.scale.x, hitbox.scale.y, 1));
        else Gizmos.DrawCube(transform.position - new Vector3(hitbox.pos.x, -hitbox.pos.y, 0), new Vector3(hitbox.scale.x, hitbox.scale.y, 1));
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = hitboxColour;
        if (hitbox1.active && !box1Applied) visualizeHitbox(hitbox1);
        if (hitbox2.active) visualizeHitbox(hitbox2);
        if (hitbox3.active) visualizeHitbox(hitbox3);


    }
    void FixedUpdate()
    {
        //HITTIN'
        if (hitbox1.active && !box1Applied) makeHitbox(hitbox1, 1);
        if (hitbox2.active) makeHitbox(hitbox2, 2);
        if (hitbox3.active) makeHitbox(hitbox3, 3);
        if (!hitbox1.active) box1Applied = false;
        if (!hitbox2.active) box2Applied = false;
        if (!hitbox3.active) box3Applied = false;
    }
}
