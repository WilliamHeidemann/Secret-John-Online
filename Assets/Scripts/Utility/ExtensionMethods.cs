using UnityEngine;
using UnityEngine.UI;

namespace Utility
{
    public static class ExtensionMethods
    {
        public static void SetAlpha(this Image image, float alphaValue)
        {
            var color = image.color;
            color.a = alphaValue;
            image.color = color;
        }
    }
}