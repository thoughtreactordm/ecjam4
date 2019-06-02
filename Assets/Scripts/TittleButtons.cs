using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TittleButtons : MonoBehaviour
{
    public GameObject ParentCanvas; // Canvas that contains current button
    public GameObject OtherMenu;    // Canvas of the canvas that's not this one
    public Image ScreenfadeImage;   // Image that will fade
    public Animator FadeScreen;     // Animator that controlls the fade
    public Animator OtherFadeScreen; // Animator that controlls the fade

    public void Switch(int sceneIndex)  // called by the buttons to switch canvas
    {
        StartCoroutine(SwitchMenu(sceneIndex));
    }

    IEnumerator SwitchMenu(int sceneIndex)      // Waits untill the screen fully fades before activateing the next canvas and disactivating the current one
    {
        FadeScreen.SetBool("Fadeout", true);
        yield return new WaitUntil(() => ScreenfadeImage.color.a == 1);

        ParentCanvas.SetActive(false);
        OtherMenu.SetActive(true);
        FadeScreen.SetBool("Fadeout", false);
        //    StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void QuitThegame(int sceneIndex) // Called by the Quit button to exit the game
    {
        StartCoroutine(EndGame());
    }

    IEnumerator EndGame()      // Waits untill the screen fully fades before quitting
    {
        FadeScreen.SetBool("Fadeout", true);
        yield return new WaitUntil(() => ScreenfadeImage.color.a == 1);
        Debug.Log("Quit");
        Application.Quit();
        //    StartCoroutine(LoadAsynchronously(sceneIndex));
    }

}
