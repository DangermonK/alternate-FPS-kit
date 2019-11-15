using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private Transform hitEmitter;
    
    private Vector3 oldPos, fwd;
    private float yVelocity = 0;
    
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

        fwd = transform.forward;
        
    }

    // Update is called once per frame
    void Update()
    {
        yVelocity += gravity * Time.deltaTime;
        
        oldPos = transform.position;
        transform.position += fwd * speed * Time.deltaTime + Vector3.down * yVelocity;
        Debug.DrawLine(oldPos, transform.position, Color.red);

        if(Physics.Linecast(oldPos, transform.position, out hit))
        {
            if(hit.collider.tag == "enemy")
            {
                Destroy(hit.collider.gameObject);
            }

            Instantiate(hitEmitter, hit.point, Quaternion.FromToRotation(hitEmitter.up, -hit.normal));
            Destroy(gameObject);
        }

    }
}
