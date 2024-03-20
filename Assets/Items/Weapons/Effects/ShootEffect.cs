using UnityEngine;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private LightSource _lightSource;
    [SerializeField] private AudioSource _shootSound;

    public void Perform()
    {
        _shootSound.Play();
        _lightSource.Play();
        _particleSystem.Play();
    }
}
