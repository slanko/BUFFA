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
