using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UniTextExtension
{
    public static class CanvasUtility
    {
        public static GameObject CreateCanvasGameObject()
        {
            GameObject canvasGO = new GameObject("Canvas");
            Undo.RegisterCreatedObjectUndo(canvasGO, "Create Canvas");

            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            CanvasScaler cScaler =  canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            
            cScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cScaler.referenceResolution = new Vector2(1920, 1080);
            cScaler.matchWidthOrHeight = 0.5f;

            if (Object.FindObjectsByType<EventSystem>(FindObjectsSortMode.None) == null)
            {
                GameObject es = new GameObject("EventSystem");
                Undo.RegisterCreatedObjectUndo(es, "Create EventSystem");
                es.AddComponent<EventSystem>();
                es.AddComponent<StandaloneInputModule>();
            }
            return canvasGO;
        }
        
        public static GameObject GetOrCreateCanvasGameObject()
        {
            GameObject selected = Selection.activeGameObject;
            if (selected != null)
            {
                Canvas canvas = selected.GetComponentInParent<Canvas>();
                if (canvas != null && canvas.gameObject.activeInHierarchy)
                    return canvas.gameObject;
            }
            
            Canvas existing = Object.FindFirstObjectByType<Canvas>();
            if (existing != null && existing.gameObject.activeInHierarchy)
                return existing.gameObject;
            
            return CreateCanvasGameObject();
        }
        
        public static void PlaceUIElement(GameObject element, MenuCommand menuCommand)
        {
            GameObject context = menuCommand.context as GameObject;
            GameObject parent = (context != null && context.GetComponentInParent<Canvas>() != null)
                ? context
                : GetOrCreateCanvasGameObject();

            GameObjectUtility.SetParentAndAlign(element, parent);
            Selection.activeGameObject = element;
        }
    }
}