using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Animation Obj")]
public class AnimationScriptableObject : ScriptableObject
{
    public ActionAnimations actionAnimation;
    public Sprite firstImage;
    public RuntimeAnimatorController animController;
}
