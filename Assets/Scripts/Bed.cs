using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bed : MonoBehaviour
{
    private static Camera _cam;
    private static float _screenMinY;
    private static Rigidbody2D _rigidbody;

    [NonSerialized] public static float maxY;

    private void Awake()
    {
        _cam = Camera.main;
        _screenMinY = _cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        _rigidbody = GetComponent<Rigidbody2D>();
        maxY = _screenMinY / 3.0f;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused) return;

        Vector3 mouseLocation = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector3 clampedLocation = new Vector3(mouseLocation.x, Math.Clamp(mouseLocation.y, _screenMinY, maxY), 0);

        _rigidbody.MovePosition(clampedLocation);
    }
}
