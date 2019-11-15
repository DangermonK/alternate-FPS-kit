using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PathSystem : MonoBehaviour
{

    [SerializeField] private float spawnTime = 20;
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject[] path;

    private float nextSpawn;

    void Start()
    {
        
        path[path.Length - 1].GetComponent<Linker>().setObj(path[0].transform);
        
        for (int i = 0; i < path.Length - 1; i++)
        {
            path[i].GetComponent<Linker>().setObj(path[i+1].transform);
        }

        nextSpawn = Time.time + spawnTime;

    }

    private void OnDrawGizmos()
    {
        path = GameObject.FindGameObjectsWithTag("path");
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(path[path.Length -1].transform.position, path[0].transform.position);
        for (int i = 1; i < path.Length; i++) {
            Gizmos.DrawLine(path[i - 1].transform.position, path[i].transform.position);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnTime;
            Instantiate(enemy, path[(int)(Random.value * path.Length - 1)].transform.position, Quaternion.Euler(0,0,0));
        }
    }
}
