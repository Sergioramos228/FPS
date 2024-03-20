public class Breakable : BaseHealth
{
    protected override void Die()
    {
        Destroy(gameObject);
    }
}
