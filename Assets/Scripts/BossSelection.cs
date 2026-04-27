using UnityEngine;
using UnityEngine.SceneManagement;

public class BossSelection : MonoBehaviour
{
    [SerializeField] private string targetScene;

    // ONMOUSEOVER
    // - Size up
    void OnMouseEnter()
    {
        print("Heh");
        Invoke(nameof(SizeUp), 0.2f);
    }

    // ONMOUSEEXIT
    // - Size down
    void OnMouseExit()
    {
        print("Hah");
        Invoke(nameof(SizeDown), 0.2f);
    }

    void SizeUp()
    {
        transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
    }

    void SizeDown()
    {
        transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    // ONMOUSEDOWN
    // - Change the scene :)
    void OnMouseDown()
    {
        ChangeScene();
    }

    // CHANGESCENE
    // - Change the scene to the target.
    public void ChangeScene()
    {
        if(targetScene != null)
            SceneManager.LoadScene(targetScene);
        else
            Debug.Log("Scene to load is missing!");
    }
}
