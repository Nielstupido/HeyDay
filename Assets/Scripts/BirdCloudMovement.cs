using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdCloudMovement : MonoBehaviour
{
    [SerializeField]private int speedMin, speedMax;
    private float speed = 0;


    private void Start()
    {
        speed = Random.Range(speedMin, speedMax);
    }


    private void Update()
    {
        this.transform.Translate(new Vector3(-speed * Time.deltaTime, 0f, 0f));
    }


    private void OnDestroy()
    {
        MenuObjManager.onObjDestroyed(transform.parent.name);
    }
}
