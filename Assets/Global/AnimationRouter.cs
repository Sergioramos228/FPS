using UnityEngine;

public class AnimationRouter : MonoBehaviour
{
    private readonly int _run = Animator.StringToHash("Run");
    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _damage = Animator.StringToHash("Damage");
    private readonly int _death = Animator.StringToHash("Death");

    private Animator _animation;

    private void Start()
    {
        _animation = GetComponent<Animator>();
    }

    public void Run(bool isRun)
    {
        _animation.SetBool(_run, isRun);
    }

    public void Attack()
    {
        _animation.SetTrigger(_attack);
    }

    public void Damage()
    {
        _animation.SetTrigger(_damage);
    }

    public void Death()
    {
        _animation.SetTrigger(_death);
    }

}
