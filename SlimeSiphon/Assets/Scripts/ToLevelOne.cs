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
            AbilityManager.instance.transform.position = new Vector3(0, 0, 0);
            SceneManager.LoadScene("Level1");
        }
    }
}
