using LightSide;
using UnityEditor;
using UnityEngine;


namespace UniTextExtension
{
    public static class UniTextCreator
    {
        [MenuItem("GameObject/UI (Canvas)/UniText")]
        public static void CreateUniText(MenuCommand menuCommand)
        {
            GameObject go = new GameObject("Text");
            
            Undo.RegisterCreatedObjectUndo(go, "Create UniText");
            
            RectTransform rectTransform = Undo.AddComponent<RectTransform>(go);
            rectTransform.sizeDelta = new Vector2(200, 50);
            
            UniText uniText = Undo.AddComponent<UniText>(go);

            Undo.RecordObject(uniText, "Setup UniText");
            
            uniText.Text = "New Text";
            uniText.AutoSize = true;
            
            
            Undo.RegisterFullObjectHierarchyUndo(go, "Create UniText");
            GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
            Selection.activeObject = go;
        }
    }
}
