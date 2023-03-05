using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Monkey : MonoBehaviour
{
    private float _moveSpeed = 2.0f;
    private Vector2 _moveDirection;
    private bool _canHitBed = true;
    private const float Pi = (float) Math.PI;
    private GameplayUIManager _ui;
    private int _bedHitCount;
    private Rigidbody2D _rigidbody;

    public float speedModifier;
    public int speedChangeRate;  // Base number for bounces between speed changes, higher is less often
    
    private void Start()
    {
        float moveAngle = Random.Range(Pi / 5.0f, (4 * Pi) / 5.0f);
        _moveDirection = new Vector2(math.cos(moveAngle), math.sin(moveAngle));
        _ui = FindObjectOfType<GameplayUIManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        if (GameManager.Instance.IsPaused) return;
        Vector2 newPosition = _rigidbody.position + (_moveDirection * (_moveSpeed * Time.deltaTime));
        _rigidbody.MovePosition(newPosition);
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
                _ui.EndGame();
                break;
            case "BedCollider":
                BounceOffBed(other);
                _ui.AddScore();
                break;
        }
    }

    private void BounceOffBed(Collider2D bed)
    {
        if (!_canHitBed) return;
        _canHitBed = false;
        
        // Use y value to change possible angle range
        Vector2 newMoveDirection = new Vector2(transform.position.x - bed.transform.position.x, 1);
        newMoveDirection.Normalize();
        _moveDirection = newMoveDirection;
        
        _bedHitCount++;
        if (_bedHitCount >= speedChangeRate) IncreaseSpeed();
    }

    public void IncreaseSpeed()
    {
        _moveSpeed += speedModifier;
        speedChangeRate = speedChangeRate * 3 / 2;

        _bedHitCount = 0;
    }

    public void DecreaseSpeed()
    {
        _moveSpeed -= speedModifier;
        speedChangeRate = ( int ) Math.Round(speedChangeRate / 3.0 * 2.0);

        _bedHitCount = 0;
    }
}
