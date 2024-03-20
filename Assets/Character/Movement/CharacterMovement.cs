using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

[RequireComponent(typeof(ConstantForce), typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    private const float DelayTime = 0.01f;

    [SerializeField] private float _speed;
    [SerializeField] private float _jumpSpeed;
    [SerializeField] private float _gravityFactor;
    [SerializeField] private CheckGround _groundChecker;

    [Inject] private NewControls _control;
    
    private Rigidbody _rigidbody;
    private WaitForSeconds _delay;
    private Coroutine _moving;
    private ConstantForce _move;
    private bool _isMoving;
    private IMoveProcessor _horizontal;
    private IMoveProcessor _vertical;
    private IMoveProcessor _current;

    public event Action<bool> MoveChanged;

    private void Awake()
    {
        _horizontal = new HorizontalProcessor(transform, _control, _speed);
        _vertical = new VerticalProcessor(_gravityFactor);
        _current = _horizontal;
        _rigidbody = GetComponent<Rigidbody>();
        _move = GetComponent<ConstantForce>();
        _delay = new WaitForSeconds(DelayTime);
    }

    private void OnEnable()
    {
        StartMove();
        _control.Character.Jump.performed += Jump;
        _groundChecker.StateChanged += ChangeIMoveProcessor;
    }

    private void OnDisable()
    {
        StopMove();
        _control.Character.Jump.performed -= Jump;
        _groundChecker.StateChanged -= ChangeIMoveProcessor;
    }

    private void StartMove()
    {
        _moving = StartCoroutine(Moving());
        MoveChanged?.Invoke(true);
    }

    private void StopMove()
    {
        if (_moving != null)
        {
            _move.force = Vector3.zero;
            StopCoroutine(_moving);
            _moving = null;
        }

        MoveChanged?.Invoke(false);
    }

    private IEnumerator Moving()
    {
        yield return null;
        
        while (true)
        {
            Vector3 newForce = _current.Calculate(_rigidbody.HorizontalVelocity());

            if (newForce.Equals(Vector3.zero))
            {
                if (_isMoving)
                {
                    MoveChanged?.Invoke(false);
                    _isMoving = false;
                }
            }
            else
            {
                if (_isMoving == false)
                {
                    MoveChanged?.Invoke(true);
                    _isMoving = true;
                }
            }

            _move.force = newForce;
            yield return _delay;
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (_current.IsJumpable)
            _rigidbody.AddForce(Vector3.up * _jumpSpeed);
    }

    private void ChangeIMoveProcessor(bool isGrounded)
    {
        if (isGrounded)
            _current = _horizontal;
        else
            _current = _vertical;
    }
}
