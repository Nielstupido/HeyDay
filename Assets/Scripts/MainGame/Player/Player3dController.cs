using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Player3dController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent playerNavMesh;
    [SerializeField] private Animator animator;
    private Vector3 targetPos = Vector3.zero;
    public static Player3dController Instance { get; private set; }


    private void Awake() 
    { 
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        animator.enabled = false;
    }


    private void Update()
    {
        if (targetPos != Vector3.zero)
        {
            playerNavMesh.isStopped = false;
            playerNavMesh.SetDestination(targetPos);
        }

        if (!playerNavMesh.pathPending && targetPos != Vector3.zero)
        {
            if (playerNavMesh.remainingDistance < 1f)
            {
                targetPos = Vector3.zero;
                playerNavMesh.isStopped = true;
                animator.enabled = false;
            }
        }
    }


    public void WalkToPoint(Vector3 pos)
    {
        targetPos = pos;
        animator.enabled = true;
        animator.Play("Walking");
    }


    public void StopMovement()
    {
        targetPos = Vector3.zero;
        playerNavMesh.isStopped = true;
        animator.enabled = false;
    }
}
