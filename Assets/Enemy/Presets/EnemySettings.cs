using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Settings", menuName = "Enemy/New Enemy Settings", order = 51)]
public class EnemySettings : ScriptableObject
{
    [SerializeField] private Material _material;
    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;
    [SerializeField, Range(0.4f, 1.6f)] private float _size;

    public Material Material => _material;
    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public float Size => _size;
}
