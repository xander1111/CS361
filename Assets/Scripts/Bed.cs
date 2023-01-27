using System;
using UnityEngine;

public class Bed : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        Vector3 mouseLocation = _cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 clampedLocation = new Vector3(mouseLocation.x, Math.Clamp(mouseLocation.y, -5.0f, -1.5f), 0);
        
        transform.position = clampedLocation;
    }
}
