using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Linker : MonoBehaviour
{

    [SerializeField] private Transform obj;

    public void setObj(Transform obj) {
        this.obj = obj;
    }

    public Transform getObj()
    {
        return obj;
    }

}
