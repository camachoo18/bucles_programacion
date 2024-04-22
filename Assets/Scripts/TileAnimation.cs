using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileAnimation : MonoBehaviour
{
    [SerializeField] float targetScale = 1.1f;
    [SerializeField] float animationDuration = 0.2f;
    [SerializeField] AnimationCurve ease;

    Vector3 originalScale;
    Vector3 scaleFrom;
    Vector3 scaleTo;

    [HideInInspector] public int X;
    [HideInInspector] public int Y;
    [HideInInspector] public Vector3 rotationSpeed = Vector3.zero;
    [HideInInspector] public int width;
    [HideInInspector] public int height;
    [HideInInspector] public float aSpeed;
    [HideInInspector] public float bSpeed;

    Material material;

    //Hago un awake para inicializar cosas basicas, y luego en el start inicializo cosas que dependan de otros componentes.
     void Awake()
    {
        originalScale = transform.localScale;

        material = GetComponentInChildren<MeshRenderer>().material;

    }

     void Start()
    {

        if (rotationSpeed != Vector3.zero || aSpeed != 0 || bSpeed != 0)
        {
            
                StartCoroutine(RotationCoroutine());         
                StartCoroutine(ColorCoroutine());
        }

    }

    public void MovementAnimation()
    {
        StartCoroutine(TileAnimations());
    }



    IEnumerator TileAnimations()
    {
        float seconds = 0;

        scaleFrom = originalScale;
        scaleTo = originalScale * targetScale;

        while (seconds < animationDuration)
        {
            seconds += Time.deltaTime;
            transform.localScale = Vector3.Lerp(
                scaleFrom,
                scaleTo,
                ease.Evaluate(seconds / animationDuration)
            );

            yield return null;
        }

        transform.localScale = originalScale;
    }

    IEnumerator RotationCoroutine()
    {
        transform.Rotate(X * 8, Y * 8, 0);

        while (true)
        {
            transform.Rotate(rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
    IEnumerator ColorCoroutine()
    {
        float a = X;
        float b = Y;
        float c = 0;
        float aDirection = 1;
        float bDirection = 1;
        float cDirection = 1;

        while (true)
        {
            a += Time.deltaTime * aSpeed * aDirection;
            if (a > width && aDirection > 0)
                aDirection = -1;
            else if (a < 0 && aDirection < 0)
                aDirection = 1;

            b += Time.deltaTime * bSpeed * bDirection;
            if (b > height && bDirection > 0)
                bDirection = -1;
            else if (b < 0 && bDirection < 0)
                bDirection = 1;

            c += Time.deltaTime * cDirection;
            if (c > height)
                cDirection = -1;
            else if (c < 0)
                cDirection = 1;

            material.color = new Color(
                a / width,
                b / height,
                c / height 
            );

            yield return null;
        }
    }

}
