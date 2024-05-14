using UnityEngine;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CubeColorChanger))]

public class Cube : MonoBehaviour
{
    private CubeColorChanger _colorChanger;

    private void Awake()
    {
        _colorChanger = GetComponent<CubeColorChanger>();
    }

    private void Start()
    {
        ChangeRandomColor();
    }

    private void ChangeRandomColor()
    {
        Color randomColor = Random.ColorHSV();
        _colorChanger.ChangeColor(randomColor);
    }
}
