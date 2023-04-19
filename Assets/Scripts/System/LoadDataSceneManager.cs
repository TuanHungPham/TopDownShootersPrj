using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadDataSceneManager : MonoBehaviour
{
    private void Start()
    {
        Invoke("LoadScene", 2f);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
