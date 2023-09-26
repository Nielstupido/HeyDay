using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCloudMovement : MonoBehaviour
{
    [SerializeField]private float speedMin, speedMax;
    private float speed = 0;


    private void Start()
    {
        MenuObjManager.onGameStart += DestroyObj;
        speed = Random.Range(speedMin, speedMax);
    }


    private void Update()
    {
        this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));
    }


    private void OnDestroy()
    {
        if (MenuObjManager.onGameStart != null)
        {
            MenuObjManager.onGameStart -= DestroyObj;
        }
        
        if (MenuObjManager.onObjDestroyed != null)
        {
            MenuObjManager.onObjDestroyed(transform.parent.name);
        }
    }


    private void DestroyObj()
    {
        Destroy(this.gameObject);
    }
}
