using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public void OpenPauseMenu() {
        gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void ClosePauseMenu() {
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
    }

    public void QuitToMainMenu() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void Quicksave() {
        SaveManager.Save();
    }

    public void Quickload() {
        SaveManager.Load();
    }

    public void QuitToDesktop() {
        Application.Quit();
    }

}