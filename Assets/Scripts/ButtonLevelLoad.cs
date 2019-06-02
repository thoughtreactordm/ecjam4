using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonLevelLoad : MonoBehaviour
{
    public Image Black;         // Image that will fade
    public Animator FadeScreen; // Animator that controlls the fade

    public void LoadLevel( int sceneIndex)
    {
        StartCoroutine(Fading(sceneIndex));
    }



    IEnumerator Fading(int sceneIndex)      // Waits untill the screen fully fades before loading next screen
    {
        FadeScreen.SetBool("Fadeout", true);
        yield return new WaitUntil(() => Black.color.a == 1);
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)  // waits until loading is done untill moving to next scene. Scene is still playing for thigns like a loading bar/ etc
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
    }

}
