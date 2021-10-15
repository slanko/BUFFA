using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CharSelectorScript : MonoBehaviour
{
    public bool p2, movePerformed;
    [SerializeField] Transform iconContainer;
    MultiverseGodScript MVGS;
    [SerializeField] Image myImage;
    [SerializeField] Text charName;
    int index = 0;

    private void Start()
    {
        //say ur prayers. find god(let)
        MVGS = GameObject.Find("Godlet").GetComponent<MultiverseGodScript>();
    }

    public void navigate(InputAction.CallbackContext context)
    {
        var dir = context.ReadValue<Vector2>();
        if (!movePerformed)
        {
            if (dir.y < 0.25f)
            {
                index++;
                movePerformed = true;
            }
            if (dir.y > -0.25f)
            {
                index--;
                movePerformed = true;
            }
        }
        if (dir.y < 0.25f && dir.y > -0.25f) movePerformed = false;

        if (index > iconContainer.childCount - 1) index = 0;
        if (index < 0) index = iconContainer.childCount - 1;
        transform.position = iconContainer.GetChild(index).position;

        //visual stuff
        CharacterFile selected;
        selected = iconContainer.GetChild(index).GetComponent<CharacterIconScript>().charFile;
        myImage.sprite = selected.characterArt;
        charName.text = selected.characterName;

    }

    public void select(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!p2)
            {
                CharacterFile selected;
                selected = iconContainer.GetChild(index).GetComponent<CharacterIconScript>().charFile;
                MVGS.characterOne = selected;
                MVGS.oneReady = true;
            }
            else
            {
                CharacterFile selected;
                selected = iconContainer.GetChild(index).GetComponent<CharacterIconScript>().charFile;
                MVGS.characterTwo = selected;
                MVGS.twoReady = true;
            }
        }
    }
}
