using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;

    public MonoBehaviour AbilityOne, AbilityTwo;

    public float StartTimerOne, StartTimerTwo;
    private float TimerOne, TimerTwo;

    private bool CanUseOne = false, CanUseTwo = false, TouchingBody = false, HasOnlyOneAbility = true;

    private GameObject InteractText, DeadBody, Canvas;

    [SerializeField] private bool FirstIsTop = true;

    private Transform AbilityParent;

    private Health DeadBodyHealth;

    private Image CooldownOne, CooldownTwo;


    private void Awake()
    {
        //If I'm the only one of myself in scene
        if (instance == null)
        {
            instance = this;
        }
        //If I'm not the original
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        NewStart();
    }

    private void NewStart()
    {
        TimerOne = StartTimerOne;

        TimerTwo = StartTimerTwo;

        InteractText = transform.GetChild(0).gameObject;

        Canvas = GameObject.FindGameObjectWithTag("Canvas");
        AbilityParent = Canvas.transform.GetChild(0).GetChild(0);

        CooldownOne = transform.GetChild(1).GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
        CooldownTwo = transform.GetChild(1).GetChild(0).GetChild(1).gameObject.GetComponent<Image>();


        if (AbilityOne != null)
        {
            Sprite img = Resources.Load("Ability" + AbilityOne.GetType().Name, typeof(Sprite)) as Sprite;

            GameObject TopAbility = AbilityParent.GetChild(0).gameObject;
            Image TopImg = TopAbility.GetComponent<Image>();

            TopImg.sprite = img;
            CooldownOne.sprite = img;
        }

        if (AbilityTwo != null)
        {
            Sprite img = Resources.Load("Ability" + AbilityTwo.GetType().Name, typeof(Sprite)) as Sprite;

            GameObject BottomAbility = AbilityParent.GetChild(2).gameObject;
            Image BottomImg = BottomAbility.GetComponent<Image>();

            BottomImg.sprite = img;
            CooldownTwo.sprite = img;

            HasOnlyOneAbility = false;
        }


        AudioManager.instance.Play("Start");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && CanUseOne)
        {
            AbilityOne.Invoke("Ability", 0f);
            TimerOne = StartTimerOne;

            CameraShake.cam.Trauma += 0.08f;

            CanUseOne = false;
        }



        else if (Input.GetMouseButtonDown(1) && CanUseTwo && !HasOnlyOneAbility)
        {
            AbilityTwo.Invoke("Ability", 0f);
            TimerTwo = StartTimerTwo;
            CameraShake.cam.Trauma += 0.08f;

            CanUseTwo = false;
        }



        else if (Input.GetKeyDown(KeyCode.F) && TouchingBody)
        {
            GetComponent<Health>().Heal(DeadBodyHealth.MaxHealth / 2);


            Component NewScript = gameObject.AddComponent(DeadBodyHealth.AbilityScript.GetType());

            if (FirstIsTop && !HasOnlyOneAbility)
            {
                Destroy(AbilityOne);
                AbilityOne = (MonoBehaviour)NewScript;

                StartTimerOne = DeadBodyHealth.Delay;
                TimerOne = StartTimerOne;
            }
            else
            {
                Destroy(AbilityTwo);
                AbilityTwo = (MonoBehaviour)NewScript;

                StartTimerTwo = DeadBodyHealth.Delay;
                TimerTwo = StartTimerTwo;
            }

            SetAbilitySprite();

            AudioManager.instance.Play("PickupAbility");

            HasOnlyOneAbility = false;

            GetComponent<PlayerMovement>().CanMove = true;
            DeadBody.tag = "Untagged";              //--------------- Need to change this, so you can swap to your old ability, but not heal again
            DisableInteractive();
        }



        else if (Input.GetKeyDown(KeyCode.Q) && !HasOnlyOneAbility)
        {
            AudioManager.instance.Play("SwappedAbilityUI");
            FlipAbility();

            if (InteractText.activeSelf)
            {
                ChangeInteractText();
            }
        }

        else if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
        }



        if (TimerOne > 0 && CanUseOne == false)
        {
            TimerOne -= Time.deltaTime;
            CooldownOne.fillAmount = 1 - (TimerOne / StartTimerOne);
        }
        else
        {
            CanUseOne = true;
        }



        if(TimerTwo > 0 && CanUseTwo == false)
        {
            TimerTwo -= Time.deltaTime;
            CooldownTwo.fillAmount = 1 - (TimerTwo / StartTimerTwo);
        }
        else
        {
            CanUseTwo = true;
        }
    }






    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Dead"))
        {
            DeadBody = col.gameObject;
            DeadBodyHealth = DeadBody.GetComponent<Health>();



            InteractText.SetActive(true);

            if (HasOnlyOneAbility)
            {
                InteractText.GetComponent<TextMeshPro>().text = "Pickup " + DeadBodyHealth.AbilityScript.GetType().Name;
            }
            else
            {
                ChangeInteractText();
            }


            TouchingBody = true;
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.transform.CompareTag("Dead"))
        {
            DisableInteractive();
        }
    }


    private void DisableInteractive()
    {

        InteractText.SetActive(false);
        DeadBody = null;
        TouchingBody = false;
    }


    private void FlipAbility()
    {
        GameObject TopAbility = AbilityParent.GetChild(0).gameObject;
        Image TopImg = TopAbility.GetComponent<Image>();

        GameObject LeftClick = AbilityParent.GetChild(1).gameObject;
        Image LClickImg = LeftClick.GetComponent<Image>();

        GameObject BottomAbility = AbilityParent.GetChild(2).gameObject;
        Image BottomImg = BottomAbility.GetComponent<Image>();

        GameObject RightClick = AbilityParent.GetChild(3).gameObject;
        Image RClickImg = RightClick.GetComponent<Image>();


        Sprite TempAbility = TopImg.sprite;

        TopImg.sprite = BottomImg.sprite;
        BottomImg.sprite = TempAbility;


        Sprite TempClick = LClickImg.sprite;

        LClickImg.sprite = RClickImg.sprite;
        RClickImg.sprite = TempClick;


        if (FirstIsTop)
        {
            FirstIsTop = false;
        }
        else
        {
            FirstIsTop = true;
        }
    }


    private void SetAbilitySprite() 
    {
        Sprite img = Resources.Load("Ability" + DeadBodyHealth.AbilityScript.GetType().Name, typeof(Sprite)) as Sprite;
        GameObject TopAbility = AbilityParent.GetChild(0).gameObject;
        Image TopImg = TopAbility.GetComponent<Image>();

        GameObject BottomAbility = AbilityParent.GetChild(2).gameObject;
        Image BottomImg = BottomAbility.GetComponent<Image>();
        
        if (!HasOnlyOneAbility)
        {
            TopImg.sprite = img;
        }
        else
        {
            BottomImg.sprite = img;
        }


        if (FirstIsTop && !HasOnlyOneAbility)
        {
            CooldownOne.sprite = img;
        }
        else
        {
            CooldownTwo.sprite = img;
        }
    }




    private void ChangeInteractText()
    {
        string currentAbility = "";

        if (FirstIsTop)
        {
            currentAbility = AbilityOne.GetType().Name;
        }
        else
        {
            currentAbility = AbilityTwo.GetType().Name;
        }

        string DeadBodyAbility = DeadBodyHealth.AbilityScript.GetType().Name;
        InteractText.GetComponent<TextMeshPro>().text = "Replace " + currentAbility + " with " + DeadBodyAbility;
    }



    public void CallNewScene()
    {
        Invoke("NewScene", 0.5f);       //------------------------ needs to change, but will work for now
    }

    private void NewScene()
    {
        NewStart();
    }
}
