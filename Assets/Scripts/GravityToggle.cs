using System.Collections.Generic;
using UnityEngine;

public class GravityToggle : MonoBehaviour
{
    private readonly Dictionary<int, Rigidbody> _cache = new Dictionary<int, Rigidbody>();

    public bool IsMoving = true;
    
    public float DragValueInside = 3f;

    void Update()
    {
        if (IsMoving)
        {
            transform.position = new Vector3(Mathf.Cos(Time.time / 2f) * 6, 0, 0);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var id = other.GetInstanceID();
        if (!_cache.TryGetValue(id, out var rigidBody))
        {
            rigidBody = other.attachedRigidbody;
            _cache[id] = rigidBody;
        }
        
        rigidBody.useGravity = false;
        rigidBody.drag = DragValueInside;

    }
    
    private void OnTriggerExit(Collider other)
    {
        var id = other.GetInstanceID();
        if (!_cache.TryGetValue(id, out var rigidBody))
        {
            rigidBody = other.attachedRigidbody;
        }
        
        rigidBody.useGravity = true;
        rigidBody.drag = 0.01f;
    }
}
