using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperMovement : MonoBehaviour
{
    public float SpeedMultiplier = 2f;
    
    void Update()
    {
        var position = transform.position;
        transform.position = new Vector3(position.x, position.y, position.z + Time.deltaTime * SpeedMultiplier);
    }
}
