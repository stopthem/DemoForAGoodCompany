using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CanTemplate.Extensions
{
    public static class TransformExtensions
    {
        ///<summary>Copies <see langword ="toTransform"/> position,rotation and scale(<see langword ="useToScale"/>) to target transform</summary>
        ///<param name = "local">If true, copies local of the parameters(position, rotation)</param>
        ///<param name = "useToScale">If true, copies <see langword = "toTransform"/>'s scale to target scale</param>
        public static void CopyTransform(this Transform t, Transform toTransform, bool local = false, bool useToScale = true, bool setParent = true)
        {
            if (setParent) t.SetParent(toTransform.parent);

            if (local)
            {
                t.localPosition = toTransform.localPosition;
                t.localRotation = toTransform.localRotation;
            }
            else
            {
                t.rotation = toTransform.rotation;
                t.position = toTransform.position;
            }

            if (useToScale) t.localScale = toTransform.localScale;
        }

        /// <summary>
        /// Transforms a world space rotation to local space.
        /// </summary>
        /// <param name="worldRotation"></param>
        /// <returns></returns>
        public static Quaternion InverseTransformRotation(this Transform t, Quaternion worldRotation) => Quaternion.Inverse(t.rotation) * worldRotation;

        public static Vector3 AnchoredPositionToScreenPosition(this RectTransform rectTransform, Vector3 anchoredPos)
        {
            var toDestinationInWorldSpace = rectTransform.position - anchoredPos;
            var toDestinationInLocalSpace = rectTransform.InverseTransformVector(toDestinationInWorldSpace);
            return rectTransform.anchoredPosition + (Vector2)toDestinationInLocalSpace;
        }
    }
}