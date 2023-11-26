using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIEcranTitre : MonoBehaviour
{
    [SerializeField] private Configs configurations;
    [SerializeField] private SampleMessageListener controller;
    [SerializeField] private int joueur;
    [Tooltip("Delay between switching focus to the new button in seconds")]
    [SerializeField] private float navigationDelay = 1.0f;
    private GameObject boutonStart;
    private GameObject ecranChoix;
    private bool started = false;
    private bool cursorMoved = false;
    private bool selecting = false;
    private bool ready = false;

    // Start is called before the first frame update
    void Start()
    {

        boutonStart = this.transform.Find("Start").gameObject;
        ecranChoix = this.transform.Find("ChoixVehicule").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (started)
        {
            Navigate();
        }else{
            StartGame();
        }

        
    }

    // Naviguer dans le menu
    void Navigate(){
        float joystickG = controller.GetJoystickL();
        float joystickD = controller.GetJoystickR();
        int actionButton = controller.GetActionButton();

        if (joystickG > 0.5f && !cursorMoved)
        {
            var next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnRight();
            if (next != null)
            {
                EventSystem.current.SetSelectedGameObject(next.gameObject);
            }

            cursorMoved = true;
            this.Invoke("resetSelection", navigationDelay );
        }
        else if (joystickG < -0.5f && !cursorMoved)
        {
            var next = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnLeft();
            if (next != null)
            {
                EventSystem.current.SetSelectedGameObject(next.gameObject);
            }

            cursorMoved = true;
            this.Invoke("resetSelection", navigationDelay );
        }

        if (actionButton == 1 && !selecting)
    {
            var currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            if (currentButton != null)
            {
                currentButton.onClick.Invoke();
            }
            selecting = true;
        }else if(actionButton == 0 && selecting){
            selecting = false;
        }
    }

    // Commencer la partie ! Ça va confirmer qu'il y a un joueur
    void StartGame(){
        int actionButton = controller.GetActionButton();

        if (actionButton > 0)
        {
            boutonStart.SetActive(false);
            ecranChoix.SetActive(true);

            configurations.playerStarted[joueur] = true;

            started = true;
        }
    }

    void resetSelection(){
        cursorMoved = false;
    }



    
}
