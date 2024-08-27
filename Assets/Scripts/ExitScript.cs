using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    [SerializeField] private ShopperQueue line;

    private void Awake()
    {
        line = FindObjectOfType<ShopperQueue>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Shopper")
        {
            Destroy(collision.gameObject);
            line.CallNextCustomer();
        }
    }
}
