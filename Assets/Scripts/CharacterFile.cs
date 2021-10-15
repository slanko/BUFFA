using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Game Specific/Character Data")]
public class CharacterFile : ScriptableObject
{
    public string characterName;
    public GameObject character;
    public Sprite characterArt;
}
