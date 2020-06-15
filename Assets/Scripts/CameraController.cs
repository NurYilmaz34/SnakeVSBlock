using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Vector3 offset;

    public MoveControl MoveControl;
    
    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, MoveControl.bodyParts[0].position.y + offset.y, transform.position.z);
    }
}
