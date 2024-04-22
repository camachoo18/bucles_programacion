using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnKeyPress : MonoBehaviour
{


    public string sceneToLoad;



    void Update()
    {

        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(sceneToLoad);
        }


    }
}
