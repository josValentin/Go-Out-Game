using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppExit : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitApp() => Application.Quit();
}
