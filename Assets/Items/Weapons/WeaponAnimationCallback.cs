using UnityEngine;

public class WeaponAnimationCallback : MonoBehaviour
{
    [SerializeField] private Rigidbody _shellPrefab;
    [SerializeField] private Transform _shellPoint;
    [SerializeField] private float _shellSpeed = 2f;
    [SerializeField] private float _shellAngular = 15f;
    [SerializeField] private AudioSource _reloadSound;

    public void ReloadSound()
    {
        _reloadSound.Play();
    }
}
