using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SearchItems : MonoBehaviour
{
    private const float SearchingDistance = 0.7f;
    private const float SearchingDelay = 0.2f;
    private const float SphereRadius = 1f;

    [SerializeField] private LayerMask _mask;

    private bool _isWork = false;
    private WaitForSeconds _delay;
    private Coroutine _searching;
    private Transform _transform;

    public Item CurrentItem { get; private set; }
    public event UnityAction<Item> FoundItem;

    private void Awake()
    {
        _delay = new WaitForSeconds(SearchingDelay);
        _transform = transform;
    }

    public void StartSearching()
    {
        _isWork = true;
        _searching = StartCoroutine(Search());
    }

    public void StopSearching()
    {
        if (_isWork == false)
            return;

        _isWork = false;
        StopCoroutine(_searching);
        _searching = null;
        ChangeFoundItem(null);
    }

    private IEnumerator Search()
    {
        yield return null;
        Item foundItem;
        Collider[] foundItems = new Collider[100];

        while (_isWork)
        {

            if (Physics.OverlapSphereNonAlloc(_transform.position + _transform.forward * SearchingDistance, SphereRadius, foundItems, _mask) > 0 && TryGetItemFromCollision(foundItems, out foundItem))
            {
                if (foundItem != CurrentItem)
                    ChangeFoundItem(foundItem);
            }
            else if (CurrentItem != null)
            {
                ChangeFoundItem(null);
            }

            yield return _delay;
        }

        _searching = null;
        ChangeFoundItem(null);
    }

    private bool TryGetItemFromCollision(Collider[] foundItems, out Item item)
    {
        item = null;

        foreach (Collider collider in foundItems)
        {
            if (collider == null)
                return false;

            item = collider.GetComponentInParent<Item>();

            if (item != null && item.IsTaken == false)
                return true;
        }

        return false;
    }

    private void ChangeFoundItem(Item item)
    {
        CurrentItem = item;
        FoundItem?.Invoke(item);
    }
}
