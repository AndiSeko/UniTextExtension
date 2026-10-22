using UnityEditor;

namespace UniTextExtension
{
    public static class UICreatorMenu
    {
        [MenuItem("GameObject/UI (Canvas)/UniText/Text", false, 2001)]
        public static void CreateUniText(MenuCommand menuCommand)
        {
            UniTextCreator.CreateUniText(menuCommand);
        }
    }
}