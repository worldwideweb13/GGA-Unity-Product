using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitlePresenter : MonoBehaviour
{
    public void OnNewGameButton()
    {
        SceneManager.LoadScene("Opening");
    }
    public void OnContinueButton()
    {
        SceneManager.LoadScene("Map");
    }

}
