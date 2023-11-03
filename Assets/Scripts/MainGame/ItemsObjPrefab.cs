using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemsObjPrefab : MonoBehaviour
{
    [SerializeField] private Image itemImageObj;
    [SerializeField] private Button itemEatBtn;

    public Sprite ItemImageSprite { set{itemImageObj.sprite = value;}}
    public Button ItemEatBtn { get{return itemEatBtn;}}


    public void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
