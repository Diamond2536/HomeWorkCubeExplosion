using System.Collections.Generic;
using UnityEngine;

public class CubeDetonator : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private float _radius;
    [SerializeField] private float _explosionForce;

    private Camera _mainCamera;

    private int _mouseButton = 0;

    private List<Cube> _cubeList = new List<Cube>();

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_mouseButton))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);            

            if (Physics.Raycast(ray, out hit))
            {
                Cube hitCube;                

                if (hit.collider.TryGetComponent(out hitCube))
                {
                    Vector3 hitCubePosition = hitCube.transform.position;
                    var newCubes = _cubeSpawner.SpawnCubesAfterExplosion(hitCubePosition);                   

                    foreach (var cube in newCubes)
                    {
                        _cubeList.Add(cube);
                    }

                    Destroy(hitCube.gameObject);
                    Explode(hitCubePosition, newCubes);
                }
            }
        }
    }

    private void Explode(Vector3 center, List<Cube> cubesToExplode)
    {
        foreach (Cube cube in cubesToExplode)
        {
            Rigidbody cubeRigidbody;

            if (cube.TryGetComponent(out cubeRigidbody))
            {
                cubeRigidbody.AddExplosionForce(_explosionForce, center, _radius);
            }
        }
    }
}
