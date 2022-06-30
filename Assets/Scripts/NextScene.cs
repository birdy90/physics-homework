using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene: MonoBehaviour
{
    public float TimeToNextScene;

    public string NextSceneName;

    void Update()
    {
        if (Time.timeSinceLevelLoad > TimeToNextScene)
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}