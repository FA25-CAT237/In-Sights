using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    // CHANGESCENE
    // - Change the scene to the target.
    public void ChangeScene(string targetScene)
    {
        if(targetScene != null)
            SceneManager.LoadScene(targetScene);
        else
            Debug.Log("Scene to load is missing!");
    }

    // QUIT:
    // - Exits the game.
    // - This is, after all, one of the buttons.
    public void Quit()
    {
        Application.Quit();
    }
}
