using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdsCloudsSpawner : MonoBehaviour
{
    [SerializeField]private List<GameObject> objPrefab = new List<GameObject>();
    [SerializeField]private float waitingTimeMin, waitingTimeMax;
    [SerializeField]private int maxSpawn;
    private int objIndex;
    private float waitSec;
    private RectTransform rectT;

    private  int totalSpawnedObj;


    private void Start()
    {
        MenuObjManager.onObjDestroyed += SubtractTotalSpawned;
        StartCoroutine("Spawn");
    }


    private IEnumerator Spawn()
    {
        if (totalSpawnedObj < maxSpawn)
        {
            rectT = (RectTransform)this.transform;
            GameObject newObj = Instantiate(objPrefab[Random.Range(0, objPrefab.Count)], Vector3.zero, this.transform.rotation, this.transform) as GameObject;
            newObj.transform.localPosition = new Vector3(15f, Random.Range(-400f, 400f), -1f);
            totalSpawnedObj++;

            waitSec = Random.Range(waitingTimeMin, waitingTimeMax);
        }

        yield return new WaitForSeconds(waitSec);
        StartCoroutine("Spawn");
    }


    private void SubtractTotalSpawned(string parent)
    {
        if (parent.Equals(gameObject.name))
        {
            totalSpawnedObj--;
        }
    }
}
