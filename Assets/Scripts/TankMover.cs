using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2d;

    [SerializeField] private TankMovementData _movementData;

    private Vector2 _movementVector;
    private float _currentSpeed = 0;
    private float _currentForewardDirection = 1;

    public UnityEvent<float> OnSpeedChange = new UnityEvent<float>();

    private void Awake()
    {
        _rb2d = GetComponentInParent<Rigidbody2D>();
    }

    public void Move(Vector2 movementVector)
    {
        this._movementVector = movementVector;
        CalculateSpeed(movementVector);
        OnSpeedChange?.Invoke(this._movementVector.magnitude);
        if (movementVector.y > 0)
        {
            if (_currentForewardDirection == -1)
                _currentSpeed = 0;
            _currentForewardDirection = 1;
        }
        else if (movementVector.y < 0)
        {
            if (_currentForewardDirection == 1)
                _currentSpeed = 0;
            _currentForewardDirection = -1;
        }

    }

    private void CalculateSpeed(Vector2 movementVector)
    {
        if (Mathf.Abs(movementVector.y) > 0)
        {
            _currentSpeed += _movementData.acceleration * Time.deltaTime;
        }
        else
        {
            _currentSpeed -= _movementData.deacceleration * Time.deltaTime;
        }
        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _movementData.maxSpeed);
    }

    private void FixedUpdate()
    {
        _rb2d.velocity = (Vector2)transform.up * _currentSpeed * _currentForewardDirection * Time.fixedDeltaTime;
        _rb2d.MoveRotation(transform.rotation * Quaternion.Euler(0, 0, -_movementVector.x * _movementData.rotationSpeed * Time.fixedDeltaTime));
    }
}
