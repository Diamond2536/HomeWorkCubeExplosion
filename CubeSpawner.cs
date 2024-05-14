using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private int _minCubeNumber = 2;
    [SerializeField] private int _maxCubeNumber = 7;
    [SerializeField] private float _scaleFactor = 0.5f;

    private bool _isStatic = true;

    private void Start()
    {
        SpawnStartCubes(_minCubeNumber, _maxCubeNumber, _isStatic);
    }

    private void SpawnCube(Vector3 position, bool isStatic)
    {
        Cube cube = Instantiate(_cubeTemplate, position, Quaternion.identity);
        cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
        cube.GetComponent<Rigidbody>().isKinematic = isStatic;
        if (!isStatic)
        {
            cube.transform.localScale *= _scaleFactor;
        }
    }

    private void SpawnCubes(int minNumber, int maxNumber, Vector3 position, bool isStatic)
    {
        int numCubes = Random.Range(minNumber, maxNumber);

        for (int i = 0; i < numCubes; i++)
        {
            float offsetX = Random.Range(-4f, 4f);
            float offsetZ = Random.Range(-4f, 4f);

            Vector3 spawnPosition = position + new Vector3(offsetX, 0f, offsetZ);
            SpawnCube(spawnPosition, isStatic);
        }
    }

    private void SpawnStartCubes(int minNumber, int maxNumber, bool isStatic)
    {
        Vector3 spawnerPosition = _cubeSpawner.transform.position;
        SpawnCubes(minNumber, maxNumber, spawnerPosition, isStatic);
    }

    public void SpawnCubesAfterExplosion(int minNumber, int maxNumber, Vector3 position)
    {
        SpawnCubes(minNumber, maxNumber, position, false);
    }

    public void SetCubesStatic(bool isStatic)
    {
        Cube[] cubes = FindObjectsOfType<Cube>();

        foreach (Cube cube in cubes)
        {
            cube.GetComponent<Rigidbody>().isKinematic = isStatic;
        }
    }
}
