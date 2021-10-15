using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControllerRepScript : MonoBehaviour
{
    Transform controllerZone, lefty, righty;
    MultiverseGodScript MVGS;
    PlayerInput input;
    bool amLeft, amRight;
    bool performed = false;
    [SerializeField] Text nameText;
    // Start is called before the first frame update
    void Awake()
    {
        //gameobject.find my beloved??
        MVGS = GameObject.Find("Godlet").GetComponent<MultiverseGodScript>();
        input = GetComponent<PlayerInput>();
        controllerZone = GameObject.Find("Canvas/Controller Zone").transform;
        lefty = GameObject.Find("Canvas/LeftPoint").transform;
        righty = GameObject.Find("Canvas/RightPoint").transform;
        transform.localScale = GameObject.Find("Canvas").transform.localScale;
        transform.SetParent(controllerZone);
        nameText.text = "P" + GameObject.FindGameObjectsWithTag("Rep").Length;
    }

    public void lockMeIn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (amLeft && !MVGS.sideOneLockedIn) MVGS.sideOneLockedIn = true;
            if (amRight && !MVGS.sideTwoLockedIn) MVGS.sideTwoLockedIn = true;
        }
    }

    public void moveDirections(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        if (!performed)
        {
            if (dir.x < -0.75f) goLeft();
            if (dir.x > 0.75f) goRight();
            performed = true;
        }

        if (dir.x > -0.1f && dir.x < 0.1f) performed = false;
    }

    void goLeft()
    {
        if (amRight && !MVGS.sideTwoLockedIn )
        {
            if (transform.parent == righty) MVGS.p2Device = null;
            transform.SetParent(controllerZone);
            amRight = false;
        }
        else if (lefty.childCount == 0 && !amRight)
        {
            transform.SetParent(lefty);
            transform.position = lefty.position;
            MVGS.p1Device = input.devices[0];
            amLeft = true;
        }
    }    
    void goRight()
    {
         if (amLeft && !MVGS.sideOneLockedIn)
         {
             MVGS.p1Device = null;
             transform.SetParent(controllerZone);
             amLeft = false;
         }
         else if (righty.childCount == 0 && !amLeft)
         {
             transform.SetParent(righty);
             transform.position = righty.position;
             MVGS.p2Device = input.devices[0];
             amRight = true;
         }
    }


}
