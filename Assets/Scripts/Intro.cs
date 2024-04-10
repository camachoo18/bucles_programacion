using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class Intro : MonoBehaviour
{

    [SerializeField] int Init = 0;

    [SerializeField] float Times = 3;
    [SerializeField] float TimeBetweenPrints = 0.5f;
    [SerializeField] GameObject Prefab;
    List<GameObject> instantiatedObjects = new List<GameObject>();

    Vector3 position;
    int numberInstantiated;
    Coroutine coroutine = null;

    [SerializeField] bool rotate = false;
    [SerializeField] Vector3 rotationSpeed;

    void Start()
    {
        StartCoroutine(MainLoop());
        StartCoroutine(SphereRotation());
    }
    IEnumerator MainLoop()
    {
        while (true)
        {

            coroutine = StartCoroutine(PrintValuesCoroutine());
            while (coroutine != null)
                yield return null;

            coroutine = StartCoroutine(DestroyObjects());
            while (coroutine != null)
                yield return null;
        }
    }


    IEnumerator PrintValuesCoroutine()
    {
        for (int i = Init; i <= Init + Times - 1; i++)
            for (int j = Init; j <= Init + Times - 1; j++)
                for (int k = Init; k <= Init + Times - 1; k++)
                {
                    if (j == Init && i == Init ||
                        i == Init && k == Init ||
                        k == Init && j == Init ||
                        j == Init && i == Init + Times - 1 ||
                        i == Init && k == Init + Times - 1 ||
                        k == Init && j == Init + Times - 1 ||
                        j == Init + Times - 1 && i == Init ||
                        i == Init + Times - 1 && k == Init ||
                        k == Init + Times - 1 && j == Init ||
                        j == Init + Times - 1 && i == Init + Times - 1 ||
                        i == Init + Times - 1 && k == Init + Times - 1 ||
                        k == Init + Times - 1 && j == Init + Times - 1)
                    {
                        position = new Vector3(j, i, k);
                        InstantiateAt(position);
                        yield return new WaitForSeconds(TimeBetweenPrints);
                    }
                }

        coroutine = null;
    }


    void InstantiateAt(Vector3 position)
    {
        GameObject instantiated = Instantiate(
            Prefab,
            transform
            );

        instantiated.transform.localPosition = position;

        instantiated.name = "Sphere " + numberInstantiated++;
        print("Placing " + instantiated.name + " at " + position);

        instantiatedObjects.Add(instantiated);

        float Normalize(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }

        instantiated.GetComponent<MeshRenderer>().material.color = new Color(
        Normalize(position.x, 0, 5),
        Normalize(position.y, 0, 5),
        Normalize(position.z, 0, 5));

    }

    IEnumerator SphereRotation()
    {
        while (true)
        {
            if (rotate == true)
            {
                transform.Rotate(rotationSpeed * Time.deltaTime);
            }
            yield return null;
        }
    }
    IEnumerator DestroyObjects()
    {
        while (instantiatedObjects.Count > 0)
        {
            Destroy(instantiatedObjects[0]);
            instantiatedObjects.RemoveAt(0);
            yield return new WaitForSeconds(TimeBetweenPrints);
        }

        coroutine = null;
    }
}
