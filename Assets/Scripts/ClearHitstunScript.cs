using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearHitstunScript : MonoBehaviour
{
    [SerializeField] UniversalPlayerScript playerScript;

    public void clearHitstun()
    {
        playerScript.hitstun = false;
    }

    public void startRunHelper()
    {
        playerScript.startRunning();
    }
}
