using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [SerializeField] private Rigidbody rigid;
    [SerializeField] private Transform target;

    [SerializeField] private float speed = 20;

    private string[] follow = { "Player", "path"};
    private int index;

    private Vector3 look, movement;
    // Start is called before the first frame update
    void Start()
    {
        index = (int)(Random.value * 2);
        target = GameObject.FindGameObjectWithTag(follow[index]).transform;

    }

    // Update is called once per frame
    void Update()
    {

        look = (target.position - transform.position);
        look.y = 0;

        if(index != 0 && Vector3.Magnitude(look) < 5)
        {
            target = target.GetComponent<Linker>().getObj();
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(look), 0.1f);
        movement = Vector3.up * rigid.velocity.y + transform.forward * speed * Time.deltaTime;

        rigid.velocity = movement;
    }
}
