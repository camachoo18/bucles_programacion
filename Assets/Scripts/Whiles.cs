using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiles : MonoBehaviour
{
    [SerializeField] Transform ObjectToMove;
    [SerializeField] List<Transform> Points;
    [SerializeField] List<Color> Colors;
    [SerializeField] AnimationCurve ease;

    Material  material;

    [SerializeField] float AnimationDuration;
    float seconds = 0f;
    //int currentPointIndex = 0;
    int startPointIndex = 0;
    int endPointIndex = 1;
    void Start()
    {
        StartCoroutine(Letter());
        StartCoroutine(CountDuration());
    }

    IEnumerator Letter()
    {
        int frameCount = 0;

        while (frameCount < AnimationDuration)
        {
            frameCount++;
            //print("a: " + frameCount);
            yield return new WaitForEndOfFrame();
        }
    }


    IEnumerator CountDuration()
    {
        while (true)
        {
            seconds = 0f;

            while (seconds < AnimationDuration)
            {
                seconds += Time.deltaTime;
               
                ObjectToMove.position = Vector3.Lerp(Points[startPointIndex].position, Points[endPointIndex].position, ease.Evaluate(seconds / AnimationDuration));
                ObjectToMove.GetComponent<Renderer>().material.color = Color.Lerp(Colors[startPointIndex], Colors[endPointIndex], ease.Evaluate(seconds / AnimationDuration));

                
                Quaternion startRotation = Points[startPointIndex].rotation;
                Quaternion endRotation = Points[endPointIndex].rotation;
                ObjectToMove.rotation = Quaternion.Lerp(startRotation, endRotation, ease.Evaluate(seconds / AnimationDuration));

                yield return null;
            }

            UpdatePointIndices();
        }
    }

    void UpdatePointIndices()
    {
        startPointIndex = endPointIndex;
        endPointIndex = (startPointIndex + 1) % Points.Count;
    }
}