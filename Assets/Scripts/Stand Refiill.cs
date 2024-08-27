using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandRefiill : MonoBehaviour
{
    [SerializeField] private GameObject itemTemplate;
    [SerializeField] private int itemLimit=9;
    [SerializeField] private int initialItemCount=2;
    
    [SerializeField] private float initialXOffset = -0.4f;
    [SerializeField] private float itemSpacing = 0.1f;
    [SerializeField] private float itemHeight = 0.5f;

    public Stack<GameObject> items = new Stack<GameObject>();

    private void Start()
    {
        for (int i = 0; i < initialItemCount; i++) 
        {
            AddItem();
        }  
    }

    public void AddItem()
    {
        if (items.Count < itemLimit)
        {
            GameObject newItem = Instantiate(itemTemplate,this.transform);
            newItem.transform.localPosition = new Vector3(initialXOffset + itemSpacing * items.Count, itemHeight, 0);
            items.Push(newItem);
        }
    }

    public void RemoveItem()
    {
        if (items.Count > 0)
        {
            Destroy(items.Pop());
        }
    }
}
