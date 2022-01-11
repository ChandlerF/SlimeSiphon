using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevelOne : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
