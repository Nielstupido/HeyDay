using System;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static void ScaleObj(GameObject obj, Vector3 targetScale, float duration, bool hide, LeanTweenType easeFunc)
    {
        LeanTween.scale(obj, targetScale, duration).setEase(easeFunc).setOnComplete(() => HideObj(obj, hide));
    }

    private static void HideObj(GameObject obj, bool hide)
    {
        if (hide)
        {
            obj.SetActive(false);
        }
    }

}
