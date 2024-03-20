using UnityEngine;

public static class RigidbodyExtension
{
    public static Vector3 HorizontalVelocity(this Rigidbody body)
    {
        Vector3 velocity = body.velocity;
        velocity.y = 0;
        return velocity;
    }
}

