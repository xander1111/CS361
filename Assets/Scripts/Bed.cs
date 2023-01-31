using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bed : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (GameManager.Instance.IsPaused) return;
        
        float screenMinY = _cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        
        Vector3 mouseLocation = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 clampedLocation = new Vector3(mouseLocation.x, Math.Clamp(mouseLocation.y, screenMinY, screenMinY / 3.0f), 0);
        
        transform.position = clampedLocation;
    }
}
