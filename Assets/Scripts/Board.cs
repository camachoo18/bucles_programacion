using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] int boardWidth;
    [SerializeField] int boardHeight;
    [SerializeField] float timeBetweenSpawns;
    [SerializeField] float timeBetweenAnimations = 0.1f;

    Vector3 cordinates;

    List<TileAnimation> tiles;

    void Start()
    {
        tiles = new List<TileAnimation>();

         Camera.main.transform.position = new Vector3(
            (boardWidth - 1) * 2.5f,
            (boardHeight - 1) * 2.5f,
            -(boardHeight - 1) * 2.5f
            );
        Camera.main.transform.LookAt(new Vector3(
            (boardWidth - 1) * 0.5f,
            0,
            (boardHeight - 1) * 0.5f
            ));


        StartCoroutine(SpawnObj());
        StartCoroutine(TileAnimations());
    }

    void Update()
    {

    }

    IEnumerator SpawnObj()
    {
        for (int i = 0; i < boardWidth; i++)
        {
            for (int j = 0; j < boardHeight; j++)
            {
                Vector3 coordinates = new Vector3(i, 0, j);
                InstantiatePrefab(tile, coordinates);
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }

    void InstantiatePrefab(GameObject prefab, Vector3 position)
    {
        GameObject instantiatedObject = Instantiate(prefab, position, Quaternion.identity);
    }


    IEnumerator TileAnimations()
    {
        yield return new WaitForSeconds(timeBetweenSpawns); 

        for (int i = 0; i < tiles.Count; i++)
        {
            StartCoroutine(tiles[i].TileAnimations());
            yield return new WaitForSeconds(timeBetweenAnimations);
        }
    }
}