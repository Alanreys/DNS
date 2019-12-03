using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class EscapeMenu : MonoBehaviour
{
    [HideInInspector]
    public bool paused = false;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (paused)
            {
                paused = false;
                gameObject.GetComponent<FirstPersonController>().enabled = true;
                SceneManager.UnloadSceneAsync("Menu");
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                paused = true;
                gameObject.GetComponent<FirstPersonController>().enabled = false;
                SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
            }
        }
    }
}