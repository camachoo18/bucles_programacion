using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] GameObject tile;
    [SerializeField] int boardWidth = 9;
    [SerializeField] int boardHeight = 5;
    [SerializeField] float timeBetweenSpawns = 0.1f;

    [SerializeField] float xRotationVelocity = 5;
    [SerializeField] float yRotationVelocity = 7;
    [SerializeField] float timeBetweenAnimations = 0.1f;
    [SerializeField] float xColorVelocity = 2.3f;
    [SerializeField] float yColorVelocity = 2.9f;
     [SerializeField] float boomDuration = 1;



    List<TileAnimation> tiles;

    void Start()
    {
        tiles = new List<TileAnimation>();

        Camera.main.transform.position = new Vector3(
            (boardWidth - 1) * 0.35f,
            (boardHeight - 1) * 0.55f,
            -(boardHeight - 1) * 0.5f
        );
        Camera.main.transform.LookAt(new Vector3(
            (boardWidth - 1) * 0.5f,
            0,
            (boardHeight - 1) * 0.5f
        ));

        StartCoroutine(InstantiateTiles());
        StartCoroutine(TileAnimations());
        StartCoroutine(boomAnimation());
    }




    IEnumerator InstantiateTiles()
    {
        TileAnimation tileanimation;

        for (int i = 0; i < boardWidth; i++)
            for (int j = 0; j < boardHeight; j++)
            {
                tileanimation = Instantiate(
                    tile,
                    new Vector3(i, 0, j),
                    Quaternion.identity
                ).GetComponent<TileAnimation>();

                tileanimation.rotationSpeed = new Vector3(
                    xRotationVelocity,
                    yRotationVelocity,
                        0
                        );

                tileanimation.X = i;
                tileanimation.Y = j;
                tileanimation.width = boardWidth;
                tileanimation.height = boardHeight;
                tileanimation.aSpeed = xColorVelocity;
                tileanimation.bSpeed = yColorVelocity;


                tiles.Add(tileanimation);

                yield return new WaitForSeconds(timeBetweenSpawns);
            }
    }

    IEnumerator TileAnimations()
    {
        int current = 0;

        while (tiles.Count == 0)
            yield return null;

        while (true)
        {
            tiles[current].MovementAnimation();
            current++;
            if (current >= tiles.Count)
                current = 0;

            yield return new WaitForSeconds(timeBetweenAnimations);
        }
    }
    public IEnumerator boomAnimation()
   {

       while (true)
       {
           for(int i = 0; i < tiles.Count; i++)
           {
               tiles[i].MovementAnimation();
               yield return null;
           }

           yield return new WaitForSeconds(boomDuration);
       }
   }
}
