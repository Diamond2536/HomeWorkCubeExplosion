using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private int _minCubeNum = 2;
    [SerializeField] private int _maxCubeNum = 7;
    [SerializeField] private float _scaleFactor = 0.5f;

    private void Start()
    {
        SpawnCubes(_minCubeNum, _maxCubeNum);
    }

    private void SpawnCubes(int minNum, int maxNum)
    {
        int numCubes = Random.Range(minNum, maxNum);
        Vector3 spawnerPosition = _cubeSpawner.transform.position;

        for (int i = 0; i < numCubes; i++)
        {
            Cube cube = Instantiate(_cubeTemplate, spawnerPosition, Quaternion.identity);
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }

    public void SpawnNewCubes(int minNum, int maxNum, Vector3 position)
    {
        int numCubes = Random.Range(minNum, maxNum);

        for (int i = 0; i < numCubes; i++)
        {
            Cube cube = Instantiate(_cubeTemplate, position, Quaternion.identity);
            cube.GetComponent<Renderer>().material.color = Random.ColorHSV();
            cube.transform.localScale *= _scaleFactor;
        }
    }
}
