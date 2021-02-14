using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ClickableUI))]
public class Touchable_Editor : Editor
     { public override void OnInspectorGUI(){} }
#endif
public class ClickableUI:Text
     { protected override void Awake() { base.Awake();} }
