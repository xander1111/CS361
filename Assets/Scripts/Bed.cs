using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Bed : MonoBehaviour
{
    private static Rigidbody2D _rigidbody;

    private static float _maxY;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _maxY = GameManager.minVisibleY / 3.0f;
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused) return;

        Vector3 mouseLocation = GameManager.mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        float clampedX = Math.Clamp(mouseLocation.x, GameManager.minVisibleX, GameManager.maxVisibleX);
        float clampedY = Math.Clamp(mouseLocation.y, GameManager.minVisibleY, _maxY);
        Vector2 newLocation = new Vector2(clampedX, clampedY);

        _rigidbody.MovePosition(newLocation);
    }
}
