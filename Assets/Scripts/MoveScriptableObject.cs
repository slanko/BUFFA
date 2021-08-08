using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMove", menuName = "Game Specific/Move")]
public class MoveScriptableObject : ScriptableObject
{
    public string moveName;
    public bool grounded, airborne;
    public int specialCancelTime = 0;
    public AnimationClip moveAnim;
    public InputBuffer.inputType[] inputRequired;
}
