using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemsObjPrefab : MonoBehaviour
{
    [SerializeField] private Image itemImageObj;

    public Sprite ItemImageSprite { set{itemImageObj.sprite = value;}}
}
