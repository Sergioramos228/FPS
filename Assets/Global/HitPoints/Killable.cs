using System;

public class Killable : BaseHealth
{
    public event Action Died;

    protected override void Die()
    {
        Died?.Invoke();
    }
}
