using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovingEntity : MonoBehaviour
{
    private Rigidbody2D _rb;

    private void OnValidate()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetSpeed(GameManager.Instance.Speed);
    }

    public void SetSpeed(float speed)
    {
        _rb.velocity = Vector2.left * speed;
        
    }
}
