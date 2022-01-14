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
        AudioManager.instance.Play("FinishedLevel");
        GetComponent<SpriteRenderer>().sprite = UnlockedDoor;
        CanUseDoor = true;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(CanUseDoor && col.transform.CompareTag("Player"))
        {
            AbilityManager.instance.transform.position = new Vector3(0, 0, 0);
            SceneManager.LoadScene("Level" + NextLevel.ToString());
        }
    }
}
