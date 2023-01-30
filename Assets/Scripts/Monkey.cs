using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monkey : MonoBehaviour
{
    private float _moveSpeed = 2.0f;
    private Vector3 _moveDirection;
    private bool _canHitBed = true;
    private const float Pi = (float) Math.PI;
    private GameplayUIManager _ui;

    private void Start()
    {
        float moveAngle = Random.Range(0.0f, Pi);
        _moveDirection = new Vector2(math.cos(moveAngle), math.sin(moveAngle)) * _moveSpeed;
        _ui = FindObjectOfType<GameplayUIManager>();
    }
    
    private void Update()
    {
        if (GameManager.Instance.IsPaused) return;
        transform.position += _moveDirection * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "EdgeCollider_Sides":
                _moveDirection.x *= -1;
                break;
            case "EdgeCollider_Top":
                _canHitBed = true;
                _moveDirection.y *= -1;
                break;
            case "EdgeCollider_Bottom":
                GameManager.Instance.EndGame();
                break;
            case "BedCollider":
                if (!_canHitBed) break;
                _canHitBed = false;
                
                // Use y value to change possible angle range
                Vector3 newMoveDirection = new Vector3(transform.position.x - other.transform.position.x, 1, 0);
                newMoveDirection.Normalize();
                _moveDirection = newMoveDirection * _moveSpeed;

                _ui.AddScore();
                
                break;
        }
    }
}
