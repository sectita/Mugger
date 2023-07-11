using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefabs;
    //public Transform[] playerrotation;

    private void Start()
    {
        SpawnPrefabs();
    }

    void SpawnPrefabs()
    {
        GameObject objRef = Instantiate(prefabs, this.transform.position, this.transform.rotation) as GameObject;
    }

}
