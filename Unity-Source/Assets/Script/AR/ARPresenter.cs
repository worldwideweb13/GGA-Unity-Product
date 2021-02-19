using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARPresenter : MonoBehaviour
{
    public void OnMapButton()
    {
        SceneManager.LoadScene("Map");
    }      

}
