using LightSide;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;

namespace UniTextExtension.Localization
{
    public static class UniTextLocalizer
    {
        private static void SetupLocalizeStringEvent(GameObject go, UniText uniText)
        {
            var existing = go.GetComponent<LocalizeStringEvent>();
            if (existing != null)
                return;

            var localize = Undo.AddComponent<LocalizeStringEvent>(go);

            var setMethod = typeof(UniText).GetProperty("Text")?.GetSetMethod();
            if (setMethod == null)
            {
                Debug.LogError("[UniTextLocalizer] Property 'Text' not found on UniText");
            }

            var action = System.Delegate.CreateDelegate(
                typeof(UnityAction<string>),
                uniText,
                setMethod
            ) as UnityAction<string>;

            UnityEventTools.AddPersistentListener(localize.OnUpdateString, action);
            localize.OnUpdateString.SetPersistentListenerState(0, UnityEventCallState.EditorAndRuntime);
        }
        
        public static void Localize(GameObject go, UniText uniText)
        {
            SetupLocalizeStringEvent(go, uniText);
        }
    }
}