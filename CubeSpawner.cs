using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubeTemplate;
    [SerializeField] private CubeSpawner _cubeSpawner;

    [SerializeField] private int _minCubeAmount = 2;
    [SerializeField] private int _maxCubeAmount = 7;

    [SerializeField] private float _scaleFactor = 0.5f;

    private void Start()
    {
        SpawnCubes(_minCubeAmount, _maxCubeAmount, _cubeSpawner.transform.position);
    }

    private Cube SpawnCube(Vector3 position)
    {
        Cube cube = Instantiate(_cubeTemplate, position, Quaternion.identity);
        return cube;
    }

    private List<Cube> SpawnCubes(int minAmount, int maxAmount, Vector3 position)
    {
        List<Cube> cubes = new List<Cube>();

        int numCubes = Random.Range(minAmount, maxAmount);

        for (int i = 0; i < numCubes; i++)
        {
            float offsetX = Random.Range(-4f, 4f);
            float offsetZ = Random.Range(-4f, 4f);

            Vector3 spawnPosition = position + new Vector3(offsetX, 0f, offsetZ);
            Cube cube = SpawnCube(spawnPosition);
            cubes.Add(cube);
        }

        return cubes;
    }

    public List<Cube> SpawnCubesAfterExplosion(Vector3 position)
    {
        List<Cube> spawnedCubes = SpawnCubes(_minCubeAmount, _maxCubeAmount, position);

        foreach (Cube cube in spawnedCubes)
        {
            cube.transform.localScale *= _scaleFactor;
        }

        return spawnedCubes;
    }
}
