using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderContainer : MonoBehaviour
{
    public static ColliderContainer Instance;
    Collider2D[] gridColliders;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gridColliders = GetComponentsInChildren<Collider2D>();
    }

    public void EnableColliderOnGrid(int column, int row)
    {
        int colliderIndex = column + 7 * row;
        gridColliders[colliderIndex].enabled = true;
    }
}
