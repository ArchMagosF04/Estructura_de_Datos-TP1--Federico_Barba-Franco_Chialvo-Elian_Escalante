using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovent : MonoBehaviour
{
    private Transform target;
    private Transform exit;
    private ShopperQueue shopperQueue;
    private MeshRenderer _renderer;
    private bool isMoving = false;
    private float enterTime;
    private int queueCount;
    private int queuePosition;
    private int currentQueuePosition;
    

    [SerializeField] private float TickDuration = 2f;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float destinationReachedTreshold = 1.5f;
    [SerializeField] private Material[] colors;

    private void Awake()
    {
        shopperQueue = FindObjectOfType<ShopperQueue>();
        _renderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        queueCount = shopperQueue._Queue.Count - 1;
        queuePosition = queueCount;
        currentQueuePosition = queuePosition;
        _renderer.material = colors[Random.Range(0, colors.Length)];
    }

    private void Update()
    {
        if(exit == null)
        {
            CheckPositionInLine();
        }

        if (target != null && isMoving)
        {
            Movement();
        }
    }

    private void CheckPositionInLine()
    {
        queueCount = shopperQueue._Queue.Count - 1;

        if (queueCount < queuePosition)
        {
            currentQueuePosition--;
            SetTargetTransform(shopperQueue.LinePositions[currentQueuePosition]);
        }
        queuePosition = queueCount;
    }

    private void Movement()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget >= destinationReachedTreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            isMoving = false;
        }
    }

    public void SetTargetTransform(Transform transform)
    {
        target = transform;
        isMoving = true;
    }

    public void SetExit(Transform transform)
    {
        exit = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Picker")
        {
            enterTime = Time.time;
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Picker" && Time.time - enterTime > this.TickDuration)
        {
            enterTime = Time.time;
            StandRefiill stand = other.GetComponentInParent<StandRefiill>();
            if(stand.items.Count == 0)
            {
                SetTargetTransform(shopperQueue.PickShelf());
            }
            else
            {
                stand.items.Peek().transform.parent = this.transform;
                stand.items.Pop().transform.localPosition = new Vector3(0,0,1f);
                SetTargetTransform(exit);
            }
        }
    }
}
