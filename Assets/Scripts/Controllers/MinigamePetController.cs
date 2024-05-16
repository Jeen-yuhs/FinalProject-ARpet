using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]

public class MinigamePetController : MonoBehaviour
{

    [SerializeField] private Camera _camera;
    [SerializeField] private float _dampingSpeed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private float _moveSpeed;

    private void FixedUpdate()
    {
        if (_joystick.Direction.y != 0)
        {
            _rb.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }

    }
}




    