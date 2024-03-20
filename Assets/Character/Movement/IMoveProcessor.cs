using UnityEngine;

public interface IMoveProcessor
{
    public bool IsJumpable { get; }
    public Vector3 Calculate(Vector3 velocity);
}
