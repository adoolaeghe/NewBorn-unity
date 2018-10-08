using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ProcShape))]
public class NewBehaviourScript : Editor {

   // ProcShape procShape;
   // Editor shapeEditor;
   // Editor colourEditor;

   // public override void OnInspectorGUI(){
   //     using (var check = new EditorGUI.ChangeCheckScope())
   //     {
   //         base.OnInspectorGUI();
   //         if(check.changed)
   //         {
   //             procShape.GenerateProcShape();    
   //         }
   //     }

   //     if (GUILayout.Button("Generate ProcShape")) 
   //     {
   //         procShape.GenerateProcShape();
   //     }

   //     DrawSettingsShapeEditor(procShape.shapeSettings, procShape.OnShapeSettingsUpdated, ref procShape.shapeSettingsFoldout, ref shapeEditor);
   //    //DrawSettingsEditor(procShape.colourSettings, procShape.OnColourSettingsUpdated, ref procShape.colourSettingsFoldout, ref colourEditor);

   // }

   // void DrawSettingsShapeEditor(ShapeSettings settings, System.Action onSettingUpdated, ref bool foldout, ref Editor editor) 
   // {
   //     if(settings != null) {
			//foldout = EditorGUILayout.InspectorTitlebar(foldout, settings);
			//using (var check = new EditorGUI.ChangeCheckScope())
			//{
			//	if (foldout)
			//	{
			//		CreateCachedEditor(settings, null, ref editor);
			//		editor.OnInspectorGUI();
			//		if (check.changed)
			//		{
			//			if (onSettingUpdated != null)
			//			{
			//				onSettingUpdated();
			//			}
			//		}
			//	}
			//}   
    //    }
    //}

    //private void OnEnable()
    //{
    //    procShape = (ProcShape)target;
    //}
}
