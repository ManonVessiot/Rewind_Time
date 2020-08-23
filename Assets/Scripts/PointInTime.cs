using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime
{
    public Vector3 _position;
    public Quaternion _rotation;

    public PointInTime(Vector3 position, Quaternion rotation)
    {
        _position = position;
        _rotation = rotation;
    }
}
