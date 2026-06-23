using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour {
    public void LoadSceneByName(string sceneName, bool additive = false) {
        if (additive) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        }
        else {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    public void LoadSceneByIndex(int sceneIndex, bool additive = false) {
        if (additive) {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex, LoadSceneMode.Additive);
        }
        else {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
        }
    }
}
