using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private int _minCubeNumber = 2;
    [SerializeField] private int _maxCubeNumber = 7;

    [SerializeField] private float _radius;
    [SerializeField] private float _explosionForce;

    [SerializeField] private float _splitFactor = 0.5f;
    [SerializeField] private float _waitingTime = 0.5f;

    private Camera _mainCamera;

    private int _mouseButton = 0;

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
                    if (Random.value < hitCube.SplitChance)
                    {
                        _cubeSpawner.SpawnCubesAfterExplosion(_minCubeNumber, _maxCubeNumber, hitCube.transform.position);
                        hitCube.SplitChance *= _splitFactor;
                    }

                    Explode(hitCube.transform.position);
                    Destroy(hitCube.gameObject);
                }
            }            
        }
    }

    private void Explode(Vector3 center)
    {
        Collider[] colliders = Physics.OverlapSphere(center, _radius);

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Rigidbody rigidBody))
            {
                rigidBody.AddExplosionForce(_explosionForce, center, _radius);
            }            
        }

        StartCoroutine(SetCubesStaticAfterDelay());
    }

    private IEnumerator SetCubesStaticAfterDelay()
    {
        yield return new WaitForSeconds(_waitingTime);

        _cubeSpawner.SetCubesStatic(true);
    }
}
