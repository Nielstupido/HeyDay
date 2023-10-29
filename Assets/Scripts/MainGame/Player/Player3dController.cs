using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player3dController : MonoBehaviour
{
    public NavMeshAgent playerNavMesh;


    private void Start()
    {
        playerNavMesh = gameObject.GetComponent<NavMeshAgent>();
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        {
            Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(movePos, out var targetInfo))
            {
                playerNavMesh.SetDestination(targetInfo.point);
            }
        }
    }
}
