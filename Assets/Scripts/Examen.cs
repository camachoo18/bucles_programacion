using System.Collections;
using UnityEngine;

public class Examen : MonoBehaviour
{
    [SerializeField] float maxXRotationSpeed;
    [SerializeField] float maxYRotationSpeed;
    [SerializeField] float maxZRotationSpeed;

    float minXRotationSpeed;
    float minYRotationSpeed;
    float minZRotationSpeed;

    [SerializeField] float XAnimationDuration;
    [SerializeField] float YAnimationDuration;
    [SerializeField] float ZAnimationDuration;

    [SerializeField] AnimationCurve ease;

    void Start()
    {
        StartCoroutine(RotateX());
        StartCoroutine(RotateY());
        StartCoroutine(RotateZ());
    }

    IEnumerator RotateX()
    {
        float timer = 0f;
        while (timer < XAnimationDuration)
        {
            float speedX = Mathf.Lerp(minXRotationSpeed, maxXRotationSpeed, ease.Evaluate(timer / XAnimationDuration));
            transform.Rotate(Vector3.right * speedX * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RotateY()
    {
        float timer = 0f;
        while (timer < YAnimationDuration)
        {
            float speedY = Mathf.Lerp(minYRotationSpeed, maxYRotationSpeed, ease.Evaluate(timer / YAnimationDuration));
            transform.Rotate(Vector3.up * speedY * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RotateZ()
    {
        float timer = 0f;
        while (timer < ZAnimationDuration)
        {
            float speedZ = Mathf.Lerp(minZRotationSpeed, maxZRotationSpeed, ease.Evaluate(timer / ZAnimationDuration));
            transform.Rotate(Vector3.forward * speedZ * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
