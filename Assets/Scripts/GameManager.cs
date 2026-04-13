using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    // buttons
    InputAction pauseAction;
    InputAction resetAction;
    InputAction debugAction;
    InputAction undoAction;

    // AWAKE:
    // - set up buttons
    void Awake()
    {
        pauseAction = InputSystem.actions.FindAction("Pause");
        resetAction = InputSystem.actions.FindAction("Reset");
        debugAction = InputSystem.actions.FindAction("Debug");
        undoAction = InputSystem.actions.FindAction("Undo");
    }

    // Update is called once per frame
    void Update()
    {
        // Reset scene when the Shift + R keys are pressed.
        if (debugAction.IsPressed())
        {
            if (resetAction.IsPressed())
            {
                print("Functional.");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            if (pauseAction.IsPressed())
            {
                print("Pausing.");
                Paused();
            }
            if (undoAction.IsPressed())
            {
                print("Unpausing.");
                Unpaused();
            }
        }

        
    }

    // Paused
    // -
    private void Paused()
    {
        Time.timeScale = 0;
    }

    private void Unpaused()
    {
        Time.timeScale = 1;
    }
}
