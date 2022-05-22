using UnityEngine;
using UnityEngine.SceneManagement;

public class Dificulties : MonoBehaviour
{
    public string mainScene;
    public void Easy()
    {
        LoadingScene.Instance.LoadScene(mainScene);
        PlayerPrefs.SetFloat("bearSpeed", 1f);
        PlayerPrefs.SetInt("islandsCount", 2);
    }
    public void Medium()
    {
        LoadingScene.Instance.LoadScene(mainScene);
        PlayerPrefs.SetFloat("bearSpeed", 1.5f);
        PlayerPrefs.SetInt("islandsCount", 3);
    }
    public void Hard()
    {
        LoadingScene.Instance.LoadScene(mainScene);
        PlayerPrefs.SetFloat("bearSpeed", 2.5f);
        PlayerPrefs.SetInt("islandsCount", 4);
    }
}
