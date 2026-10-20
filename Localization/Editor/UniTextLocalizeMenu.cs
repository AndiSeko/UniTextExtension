using LightSide;
using UnityEditor;

namespace UniTextExtension.Localization
{
    public class UniTextLocalizeMenu
    {
        [MenuItem("CONTEXT/UniText/Localize")]
        private static void Localize(MenuCommand command)
        {
            UniText text = command.context as UniText;

            if (text != null)
            {
                UniTextLocalizer.Localize(text.gameObject, text);
            }
        }
    }
}