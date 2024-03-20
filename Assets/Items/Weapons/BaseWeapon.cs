using System.Collections;
using UnityEngine;

public class BaseWeapon : Item
{
    [Header("Weapon Settings")]
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _maxDistance = 0.5f;
    [SerializeField] private float _impactForce = 20f;
    [SerializeField] private ShootEffect _shootEffect;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _attackDelay = 1.5f;
    [SerializeField] private LayerMask _castFilter;

    private bool _isReadyToAttack = true;

    public override void Interact(Vector3 forward, Vector3 startPosition)
    {
        if (_isReadyToAttack == false)
            return;

        StartCoroutine(Reload());
        RaycastShoot(forward, startPosition);
        _shootEffect.Perform();
        _animator.SetTrigger("Shoot");
        _isReadyToAttack = false;
    }

    protected override void OnDrop()
    {
        _animator.SetBool("InHand", false);
    }

    protected override void OnTake()
    {
        _animator.SetBool("InHand", true);
    }

    private void RaycastShoot(Vector3 forward, Vector3 startPosition)
    {
        if (Physics.Raycast(startPosition, forward, out RaycastHit hitInfo, _maxDistance, _castFilter))
        {
            var health = hitInfo.collider.GetComponentInParent<BaseHealth>();

            if (health != null)
                health.TakeDamage(_damage);

            var victimBody = hitInfo.rigidbody;

            if (victimBody != null)
            {
                victimBody.AddForceAtPosition(forward * _impactForce, hitInfo.point, ForceMode.Force);
            }
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_attackDelay);
        _isReadyToAttack = true;
    }
}
