using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float _speed = 6f;
    private Rigidbody2D _rb;
    private bool _directionIsTop = false;
    private bool _inAir = false;

    [SerializeField] private GameObject _deathParticle;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector3.down * _speed;
    }
    
    void Update()
    {
        if((Input.GetMouseButtonDown(0) || Input.touchCount > 0) && !_inAir)
        {
            if(_directionIsTop)
            {
                _rb.velocity = Vector3.down * _speed;
                _directionIsTop = false;
            }
            else
            {
                _rb.velocity = Vector3.up * _speed;
                _directionIsTop = true;
            }
        }

        if(_directionIsTop)
        {
            _rb.velocity = Vector3.up * _speed;
        }
        else
        {
            _rb.velocity = Vector3.down * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Air")
            _inAir = true;

        if (collision.gameObject.tag == "Hole")
        {
            DestroyBall();
            GameManager.Instance.GameOver();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Air")
            _inAir = false;        
    }

    public void DestroyBall()
    {
        Instantiate(_deathParticle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void SetSpeed(float value)
    {
        _speed = value;

        if (_directionIsTop)
        {
            _rb.velocity = Vector3.up * _speed;
        }
        else
        {
            _rb.velocity = Vector3.down * _speed;
        }

    }

}
