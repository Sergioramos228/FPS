using System;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    private const float DecayTime = 4;
    private const float CameraMoveDuration = 0.8f;

    [SerializeField] private AnimationRouter _animator;
    [SerializeField] private CharacterRotation _rotation;
    [SerializeField] private Transform _cameraGamePosition;
    [SerializeField] private Hand _hand;
    [SerializeField] private CharacterMovement _movement;
    [SerializeField] private Killable _hitPoints;

    private Transform _transform;

    public Vector3 Position => _transform.position;

    public event Action Died;

    private void Awake()
    {
        ChangePartActivities(false);
        _transform = transform;
    }

    private void OnEnable()
    {
        _movement.MoveChanged += _animator.Run;
        _hitPoints.HealthChanged += _animator.Damage;
        _hitPoints.Died += OnDied;
    }

    private void OnDisable()
    {
        _movement.MoveChanged -= _animator.Run;
        _hitPoints.HealthChanged -= _animator.Damage;
        _hitPoints.Died -= OnDied;
    }

    public void EnterGame()
    {
        Transform camera = Camera.main.transform;
        camera.parent = _cameraGamePosition;
        camera.DOMove(_cameraGamePosition.position, CameraMoveDuration).onComplete = () => ChangePartActivities(true);
        camera.DORotateQuaternion(_cameraGamePosition.rotation, CameraMoveDuration);
    }

    public void TakeDamage(float damage)
    {
        _hitPoints.TakeDamage(damage);
    }

    private void OnDied()
    {
        ChangePartActivities(false);
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return new WaitForEndOfFrame();
        _animator.Death();
        _transform.DOMove(_transform.position + Vector3.up * DecayTime, DecayTime).OnComplete(DiedInvoking);
    }

    private void DiedInvoking() => Died?.Invoke();

    private void ChangePartActivities(bool newValue)
    {
        _movement.enabled = newValue;
        _hand.enabled = newValue;
        _rotation.enabled = newValue;
    }
}
