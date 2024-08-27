using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfScript : MonoBehaviour
{
    [SerializeField] private List<StandRefiill> stands = new List<StandRefiill>();
    public List<StandRefiill> Stands => stands;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            stands.Add(child.GetComponent<StandRefiill>());
        }
    }
}
