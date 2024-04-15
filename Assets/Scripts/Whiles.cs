using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiles : MonoBehaviour
{
    [SerializeField] Transform ObjectToMove;
    [SerializeField] List<Transform> Points;
    [SerializeField] List<Color> Colors;
    [SerializeField] AnimationCurve ease;
    [SerializeField] float AnimationDuration;

    Material material;

   
    int startPointIndex = 0;
    int endPointIndex = 1;

    void Start()
    {
        material = ObjectToMove.GetComponent<MeshRenderer>().material;
        StartCoroutine(CountDuration());
    }
   /* IEnumerator Letter()
    {
        int frameCount = 0;

        while (frameCount < AnimationDuration)
        {
            frameCount++;
            //print("a: " + frameCount);
            yield return new WaitForEndOfFrame();
        }
    }*/
    IEnumerator CountDuration()
    {
        float elapsedTime;

        while (true)
        {
            elapsedTime = 0;

            while (elapsedTime < AnimationDuration)
            {
                elapsedTime += Time.deltaTime;

                //.LerpUnclamped es un tipo de interpolacion que permite exceder los valores iniciales y finales, es decir que los supera. es algo mas suave y continuo, pero puede pasar los 
                 // limites que pongamos, en este caso avanza un poco mas de los limites que ponemos.

                ObjectToMove.position = Vector3.LerpUnclamped(
                    Points[startPointIndex].position,
                    Points[endPointIndex].position,
                    ease.Evaluate(elapsedTime / AnimationDuration)
                );
                ObjectToMove.rotation = Quaternion.LerpUnclamped(
                    Points[startPointIndex].rotation,
                    Points[endPointIndex].rotation,
                    ease.Evaluate(elapsedTime / AnimationDuration)
                );
                material.color = Color.LerpUnclamped(
                    Colors[startPointIndex],
                    Colors[endPointIndex],
                    ease.Evaluate(elapsedTime / AnimationDuration)
                );

                yield return null;
            }

            UpdatePointIndices();

            yield return null;
        }
    }

    void UpdatePointIndices()
    {
        startPointIndex = endPointIndex;
        endPointIndex = (endPointIndex + 1) % Points.Count;
    }
}
