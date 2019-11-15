using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class Interaction : MonoBehaviour
{

    [SerializeField] private float rayLength;
    [SerializeField] private TextMeshProUGUI textField;

    private Dictionary<string, string> info;
    private string value;

    private RaycastHit hit;
    private bool onHit;

    private int layerMask;

    // Start is called before the first frame update
    void Start()
    {

        info = new Dictionary<string, string>();
        info.Add("mag", "Press E to use");

        layerMask = LayerMask.NameToLayer("interaction");

        onHit = false;
    }

    private void FixedUpdate()
    {
        onHit = Physics.Raycast(transform.position, transform.forward, out hit, rayLength);
    }

    private void Update()
    {
        if(hasHit())
        {
            info.TryGetValue(getHitObject().tag, out value);
            textField.SetText(value);
        } else
        {
            textField.SetText("");
        }
    }

    public bool hasHit()
    {
        return onHit;
    }

    public GameObject getHitObject()
    {
        return (hasHit() ? hit.collider.gameObject : null);
    }

}
