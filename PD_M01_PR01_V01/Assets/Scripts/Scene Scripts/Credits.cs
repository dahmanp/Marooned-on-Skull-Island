using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour
{
    public void PlayAgain()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);//
        SceneManager.LoadScene("HeroBorn", LoadSceneMode.Additive);
    }
}
