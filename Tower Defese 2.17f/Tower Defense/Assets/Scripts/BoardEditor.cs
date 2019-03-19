using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BoardEditor : EditorWindow {

    string nodes = "nodes";
    
    [MenuItem("Window/Board Editor")]
	private static void ShowWindow()
    {
        GetWindow(typeof(BoardEditor));
    }

    private void OnGUI()
    {
        //Window code here
        GUILayout.Label(nodes, EditorStyles.boldLabel);
    }
}
