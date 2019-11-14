using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndDie : MonoBehaviour
{

    [SerializeField] private float time;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
