using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharaDataPresenter : MonoBehaviour
{
    public void OnCollectionsButton()
    {
        SceneManager.LoadScene("Collections");
    }  

}
