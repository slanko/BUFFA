using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLegacizer : MonoBehaviour
{
    [SerializeField] AnimationClip[] animationsToLegacize;
    public void legacizeAnims()
    {
        string debugString = "LEGACIZATION OUTPUT: \n";
        foreach (AnimationClip anim in animationsToLegacize)
        {
            anim.legacy = true;
            if (anim.legacy == true) debugString += "Legacized " + anim.name + " successfully \n";
            if (anim.legacy == false) debugString += "HEY!!! " + anim.name + " FAILED TO LEGACIZE!!! \n";
        }
        debugString += "End of legacization.";
        Debug.Log(debugString);
    }

    public void deLegacizeAnims()
    {
        string debugString = "DELEGACIZATION OUTPUT: \n";
        foreach (AnimationClip anim in animationsToLegacize)
        {
            anim.legacy = false;
            if (anim.legacy == false) debugString += "Delegacized " + anim.name + " successfully \n";
            if (anim.legacy == true) debugString += "<color = #FF0000>HEY!!!</color> " + anim.name + " FAILED TO DELEGACIZE!!! \n";
        }
        debugString += "End of legacization.";
        Debug.Log(debugString);
    }
}
