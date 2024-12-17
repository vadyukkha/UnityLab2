using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuButtonLogic : MonoBehaviour
{
    public void OnMenu(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            SceneManager.LoadScene(1);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
