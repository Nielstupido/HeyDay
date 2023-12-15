using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player3dController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent playerNavMesh;
    [SerializeField] private Animator animator;
    private float clickTimeThres = 0.3f;
    private float firstClickTime = 0f;


    private void Start()
    {
        playerNavMesh = gameObject.GetComponent<NavMeshAgent>();
        animator.StopPlayback();
    }


    private void Update()
    {
        if (Input.anyKeyDown)
        // if(Input.touchCount > 0) //for mobile
        {
            if (Time.time - firstClickTime < clickTimeThres)
            {
                Ray movePos = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(movePos, out var targetInfo))
                {
                    playerNavMesh.SetDestination(targetInfo.point);
                    animator.Play("Walking");
                }
                firstClickTime = 0;
            }
            else
            {
                firstClickTime = Time.time;
            }
        }
    }
}
