using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private int _minCubeNum = 2;
    [SerializeField] private int _maxCubeNum = 7;

    [SerializeField] private float _radius;
    [SerializeField] private float _explosionForce;

    [SerializeField] private float _splitFactor = 0.5f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Cube hitCube = hit.collider.GetComponent<Cube>();

                if (hitCube != null)
                {
                    if (Random.value < hitCube.SplitChance)
                    {
                        _cubeSpawner.SpawnNewCubes(_minCubeNum, _maxCubeNum, hitCube.transform.position);
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
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radius);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody cubeRigidBody = colliders[i].GetComponent<Rigidbody>();

            if (cubeRigidBody != null)
            {
                cubeRigidBody.AddExplosionForce(_explosionForce, center, _radius);
            }
        }
    }
}