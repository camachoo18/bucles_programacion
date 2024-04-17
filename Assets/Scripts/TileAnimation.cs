using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileAnimation : MonoBehaviour
{
    [SerializeField] float targetScale = 1.5f;
    [SerializeField] float animationDuration = 0.5f;
    [SerializeField] AnimationCurve ease;

    Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        StartCoroutine(TileAnimations());
    }

    public IEnumerator TileAnimations()
    {
        
        {
            float timeElapsed = 0f;
            while (timeElapsed < animationDuration)
            {
                
                float scale = Mathf.Lerp(originalScale.x, targetScale, ease.Evaluate(timeElapsed / animationDuration));
                transform.localScale = new Vector3(scale, scale, scale);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            transform.localScale = originalScale; 
            
        }
    }
}
