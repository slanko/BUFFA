using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MoveLegacizer))]


public class MoveLegacizerBuddy : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MoveLegacizer myScript = (MoveLegacizer)target;

        if (GUILayout.Button("Legacize Anims")) myScript.legacizeAnims();
        if (GUILayout.Button("Delegacize Anims")) myScript.deLegacizeAnims();
    }

}
//WHY DOES THIS SCRIPT EXIST??

/*
 * Good question.
 * 
 * Okay so right. The animation system I'm using for this game needs two seperate animators, the current one and the old deprecated one.
 * 
 * WHY??? THAT"S FUCKING STUPID!!
 * 
 * yeah yeah i know calm down.
 * So, a big focus of this game is modularity. Making characters is a lot easier if I can just say "THIS MOVE IS ON THEIR MOVELIST",
 * and the way I've achieved that is by running all the moves through the old stinky animation system, rather than the new one.
 * This means characters that steal moves (AMOEBA) don't need to have access to every move right from the get-go, but can rather
 * just have a move added to their movelist at runtime.
 * 
 * BUT, WHY DOES THIS SCRIPT EXIST?? YOU HAVE DONE A LOT OF JUSTIFYING AND NO EXPLAINING
 * 
 * yeah okay alright
 * So, the old stinky animation system needs legacy animations to work. The nice odourless new animation system doesn't like them at all.
 * That means when I need a move played, it needs to be legacized, then delegacized so I can continue working with it in-editor.
 * When I build, I only need the legacy animations, since I only need them delegacized to work on them. So I can just run this editor script
 * and legacize/delegacize all my scripts when I needs to.
 * 
 * SHUT UP!!!
 */
