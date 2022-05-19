using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class checkIsComplete : MonoBehaviour
{
    public string finishScene;

    public GameObject jawa;
    public GameObject sumatra;
    public GameObject kalimantan;
    public GameObject papua;
    public GameObject jawaPlace;
    public GameObject sumatraPlace;
    public GameObject kalimantanPlace;
    public GameObject papuaPlace;
    public GameObject jawaPuzzle;
    public GameObject sumatraPuzzle;
    public GameObject kalimantanPuzzle;
    public GameObject papuaPuzzle;

    // Start is called before the first frame update

    private void Start()
    {
        switch (PlayerPrefs.GetInt("islandsCount"))
        {
            case 2:
                {
                    kalimantanPlace.SetActive(false);
                    papuaPlace.SetActive(false);
                    kalimantan.SetActive(false);
                    papua.SetActive(false);
                    break;
                }
            case 3:
                {
                    papuaPlace.SetActive(false);
                    papua.SetActive(false);
                    break;
                }
            case 4:
                {
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    private void Update()
    {
        switch (PlayerPrefs.GetInt("islandsCount"))
        {
            case 2:
                {
                    if (jawaPuzzle.transform.childCount > 0 && sumatraPuzzle.transform.childCount > 0)
                    {
                        SceneManager.LoadScene(finishScene);
                    }
                    break;
                }
            case 3:
                {
                    if (jawaPuzzle.transform.childCount > 0 && sumatraPuzzle.transform.childCount > 0 && kalimantanPuzzle.transform.childCount > 0)
                    {
                        SceneManager.LoadScene(finishScene);
                    }
                    break;
                }
            case 4:
                {
                    if (jawaPuzzle.transform.childCount > 0 && sumatraPuzzle.transform.childCount > 0 && kalimantanPuzzle.transform.childCount > 0 && papuaPuzzle.transform.childCount > 0)
                    {
                        SceneManager.LoadScene(finishScene);
                    }
                    break;
                }
            default:
                {
                    break;
                }
        }
    }
}
