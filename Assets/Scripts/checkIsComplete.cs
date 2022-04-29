using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class checkIsComplete : MonoBehaviour
{
    public string finishScene;
    public Transform puzzleOne;
    public Transform puzzleTwo;
    public Transform puzzleThree;
    // Start is called before the first frame update


    private void Update()
    {
        if (puzzleOne.transform.childCount > 0 && puzzleTwo.transform.childCount > 0 && puzzleThree.transform.childCount > 0)
        {
            SceneManager.LoadScene(finishScene);
        }
    }
}
