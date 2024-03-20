using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalProcessor : IMoveProcessor
{
    private Vector3 _customGravity;

    public VerticalProcessor(float gravityFactor)
    {
        _customGravity = Physics.gravity * gravityFactor;
        IsJumpable = false;
    }

    public bool IsJumpable { get; private set; }

    public Vector3 Calculate(Vector3 velocity)
    {
        return (velocity + _customGravity) * Time.deltaTime;
    }
}
