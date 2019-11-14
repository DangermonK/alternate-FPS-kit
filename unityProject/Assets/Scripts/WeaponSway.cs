using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{

    [SerializeField] private CameraMovement cam;
    [SerializeField] private PlayerMovement player;

    [SerializeField] private float smoothing;
    [SerializeField] private float strength;

    private float xSway = 0, ySway = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        xSway = Mathf.Lerp(xSway, (cam.getInputX() + player.getInputX()) * strength, smoothing);
        ySway = Mathf.Lerp(ySway, (cam.getInputY() + player.getInputY()) * strength, smoothing);
        
        transform.rotation = Quaternion.Euler(cam.getRotationY() + ySway, cam.getRotationX() - xSway, 0);

    }
}
