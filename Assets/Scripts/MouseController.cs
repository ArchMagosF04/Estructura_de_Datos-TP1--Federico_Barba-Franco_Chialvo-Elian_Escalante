using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void Awake()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 mousePosition = Input.mousePosition;

        Ray myRay = _camera.ScreenPointToRay(mousePosition);

        RaycastHit hit;

        bool hitFound = Physics.Raycast(myRay, out hit);

        if (Input.GetMouseButtonDown(0))
        {
            if(hitFound && hit.transform.tag == "Stand")
            {
                Debug.Log(hit.transform.name + "Item Added");
                hit.transform.GetComponent<StandRefiill>().AddItem();
            }
        }

        if(Input.GetMouseButtonDown(1))
        {
            if(hitFound && hit.transform.tag == "Stand")
            {
                Debug.Log(hit.transform.name + "Item Removed");
                hit.transform.GetComponent<StandRefiill>().RemoveItem();
            }
        }
    }
}
