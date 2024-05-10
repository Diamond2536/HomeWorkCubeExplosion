using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private int _minCubeNumber = 2;
    [SerializeField] private int _maxCubeNumber = 7;
    [SerializeField] private float _scaleFactor = 0.5f;

    private bool _isStaticSpawn = true;

    private void Start()
    {
        SpawnCubes(_minCubeNumber, _maxCubeNumber, _isStaticSpawn);
    }

    private void SpawnCubes(int minNumber, int maxNumber, bool isStatic)
    {
        Vector3 spawnerPosition = _cubeSpawner.transform.position;
        int numCubes = Random.Range(minNumber, maxNumber);

        for (int i = 0; i < numCubes; i++)
        {
            float offsetX = Random.Range(-4f, 4f);
            float offsetZ = Random.Range(-4f, 4f);

            Vector3 spawnPosition = spawnerPosition + new Vector3(offsetX, 0f, offsetZ); 
            Cube cube = Instantiate(_cubeTemplate, spawnPosition, Quaternion.identity);
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            cube.GetComponent<Rigidbody>().isKinematic = isStatic;
        }
    }

    public void SpawnNewCubes(int minNumber, int maxNumber, Vector3 position)
    {
        int numCubes = Random.Range(minNumber, maxNumber);

        for (int i = 0; i < numCubes; i++)
        {
            Cube cube = Instantiate(_cubeTemplate, position, Quaternion.identity);
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            cube.transform.localScale *= _scaleFactor;
        }
    }
}
