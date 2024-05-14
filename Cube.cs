using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]

public class Cube : MonoBehaviour
{
    private float _splitChance = 1f;

    public float SplitChance
    {
        get { return _splitChance; }
        set { _splitChance = value; }
    }
}

