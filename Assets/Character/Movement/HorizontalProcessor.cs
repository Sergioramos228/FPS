using UnityEngine;

public class HorizontalProcessor : IMoveProcessor
{
    private const float VelocityDumpCoefficient = 9f;
    private const float Stabilizer = 100;
    private const float MinMagnitude = 0.01f;

    private Transform _transform;
    private NewControls _control;
    private float _speed;

    public HorizontalProcessor(Transform transform, NewControls control, float speed)
    {
        _transform = transform;
        _control = control;
        _speed = speed;
        IsJumpable = true;
    }

    public bool IsJumpable { get; private set; }

    public Vector3 Calculate(Vector3 horizontalVelocity)
    {
        Vector2 input = _control.Character.Move.ReadValue<Vector2>().normalized;
        Vector3 velocity = -horizontalVelocity * VelocityDumpCoefficient;

        if (input.sqrMagnitude > MinMagnitude)
        {
            Vector3 forward = Vector3.ProjectOnPlane(_transform.forward, Vector3.up).normalized;
            Vector3 strafe = Vector3.ProjectOnPlane(_transform.right, Vector3.up).normalized;
            velocity += (forward * input.y + strafe * input.x) * _speed;
        }

        return velocity *= Time.deltaTime * Stabilizer;
    }
}

