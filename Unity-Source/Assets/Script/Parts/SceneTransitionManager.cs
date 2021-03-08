using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenePresenter : MonoBehaviour
{
    public void OnCollectionsButton()
    {
        SceneManager.LoadScene("Collections");
    }

    public void OnTopButton()
    {
        SceneManager.LoadScene("Title");
    }

    public void OnARButton()
    {
        SceneManager.LoadScene("AR");
    }

}
