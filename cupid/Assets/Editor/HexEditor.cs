// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
//
// [CustomEditor(typeof(Map))]
// public class TetrisControllerEditor : Editor
// {
//     Map gm;
//     
//     void OnEnable()
//     {
//         gm = target as Map;
//         gm.EditorRepaint += Repaint;
//     }
//
//     void OnDisable()
//     {
//         gm.EditorRepaint -= Repaint;
//     }
//
//     public override void OnInspectorGUI()
//     {
//         base.OnInspectorGUI();
//         GUILayout.Space(20);
//
//         EditorGUILayout.LabelField("Blocks");
//         for (int i = 19; i >= 0; i--)
//         {
//             GUILayout.BeginHorizontal();
//             for (int j = 0; j < 10; j++)
//                 gm.matrix[j, i] = EditorGUILayout.IntField(gm.matrix[j,i].value, GUILayout.Width(30));
//             GUILayout.EndHorizontal();
//         }
//     }
// 	
// }