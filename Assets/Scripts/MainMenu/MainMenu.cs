using UnityEngine;

public class MainMenu : MonoBehaviour {

    public GameObject goMain;
    public GameObject goOptions;

    public void Play() {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void OpenOptions() {
        goMain.SetActive(false);
        goOptions.SetActive(true);
    }

    public void CloseOptions() {
        goMain.SetActive(true);
        goOptions.SetActive(false);
    }

    public void Quit() {
        Application.Quit();
    }

}
