using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShopperQueue : MonoBehaviour
{
    private Queue<CustomerMovent> queue = new Queue<CustomerMovent>();
    private List<Transform> linePositions = new List<Transform>();
    private float enterTime;

    [SerializeField] private float TickDuration = 5f;
    [SerializeField] private GameObject customer;
    [SerializeField] private int lineLimit;
    [SerializeField] private Transform exit;

    public Queue<CustomerMovent> _Queue => queue;
    public List<Transform> LinePositions => linePositions;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            linePositions.Add(child);
        }
    }

    private void Start()
    {
        enterTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - enterTime > this.TickDuration)
        {
            AddCustomerToLine();
            enterTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            AddCustomerToLine();
            Debug.Log(queue.Count);
        } 

        if (Input.GetKeyDown(KeyCode.R))
        {
            CallNextCustomer();
        }
    }

    public void AddCustomerToLine()
    {
        if(queue.Count<lineLimit)
        {
            GameObject newShopper = Instantiate(customer);
            newShopper.transform.position = linePositions[queue.Count].position;
            queue.Enqueue(newShopper.GetComponent<CustomerMovent>());
        } 
    }

    public void CallNextCustomer()
    {
        if(queue.Count>0)
        {
            queue.Peek().SetExit(exit);
            queue.Dequeue().SetTargetTransform(PickShelf());
        }
    }

    public Transform PickShelf()
    {
        GameObject[] shelf = GameObject.FindGameObjectsWithTag("Shelf");
        ShelfScript shelfScript = shelf[Random.Range(0,shelf.Length)].GetComponent<ShelfScript>();

        Transform[] pos = shelfScript.Stands[Random.Range(0, shelfScript.Stands.Count)].GetComponentsInChildren<Transform>();

        return pos[1];
    }
}
