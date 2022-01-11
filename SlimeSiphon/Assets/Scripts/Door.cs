using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public static Door instance;

    private int EnemyCount;
    private bool CanUseDoor = false;
    [SerializeField] private int NextLevel;
    [SerializeField] private Sprite UnlockedDoor;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateCount();
    }

    public void UpdateCount()
    {
        EnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;



        if (EnemyCount <= 0)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        GetComponent<SpriteRenderer>().sprite = UnlockedDoor;
        CanUseDoor = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(CanUseDoor && col.transform.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level" + NextLevel.ToString());
        }
    }
}
