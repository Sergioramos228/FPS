using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [Header("View Settings")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;
    private const float DropForce = 100f;
    private Rigidbody _rigidbody;

    public string Name => _name;
    public Sprite Icon => _icon;
    public bool IsTaken { get; private set; }
    public abstract void Interact(Vector3 forward, Vector3 startPosition);

    public event UnityAction ChangedState;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Take(Transform newParent)
    {
        OnTake();
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        IsTaken = true;
        ChangedState?.Invoke();
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void Drop()
    {
        OnDrop();
        _rigidbody.useGravity = true;
        _rigidbody.isKinematic = false;
        IsTaken = false;
        transform.SetParent(null);
        _rigidbody.AddForce(transform.forward * DropForce);
    }

    protected abstract void OnDrop();
    protected abstract void OnTake();
}
