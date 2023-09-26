using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsCloudsEndpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.Equals("Obj"))
        {
            Destroy(collider.gameObject);
        }
    }
}
