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
    [SerializeField] private GameObject firstButton;
    [SerializeField] private GameObject[] Vehicules;
    private GameObject boutonStart;
    private GameObject ecranChoix;
    [SerializeField] private EventSystem eventSystem;

    [SerializeField] private AudioSource audioSource;
    private GameObject ecranPret;
    [SerializeField] private bool started = false;
    [SerializeField] private bool cursorMoved = false;
    [SerializeField] private bool selecting = false;
    public bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        
        boutonStart = this.transform.Find("Start").gameObject;
        ecranChoix = this.transform.Find("ChoixVehicule").gameObject;
        ecranPret = this.transform.Find("Waiting").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            ReadyScreen();
        }else if(started){
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

        // Convertir en valeur numérique entre -1 et 1
        float conversionG = (Mathf.InverseLerp(0, 1024, joystickG) * 2 - 1) * -1;
        float conversionD = (Mathf.InverseLerp(0, 1024, joystickD) * 2 - 1) * -1;

        if (conversionG > 0.5f && !cursorMoved)
        {
            var next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (next != null)
            {
                eventSystem.SetSelectedGameObject(next.gameObject);
            }

            cursorMoved = true;
            this.Invoke("resetSelection", navigationDelay );
        }
        else if (conversionG < -0.5f && !cursorMoved)
        {
            var next = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                eventSystem.SetSelectedGameObject(next.gameObject);
            }

            cursorMoved = true;
            this.Invoke("resetSelection", navigationDelay );
        }

        if (actionButton == 1 && !selecting)
    {
            var currentButton = eventSystem.currentSelectedGameObject.GetComponent<Button>();
            if (currentButton != null)
            {
                currentButton.onClick.Invoke();
                audioSource.Play();
            }
            selecting = true;
        }else if(actionButton == 0 && selecting){
            selecting = false;
        }
    }

    // Commencer la partie ! Ça va confirmer qu'il y a un joueur
    void StartGame(){
        int actionButton = controller.GetActionButton();

        if (actionButton > 0 && !selecting)
        {
            boutonStart.SetActive(false);
            ecranChoix.SetActive(true);

            // Assuming the first button is a child of ecranChoix
            eventSystem.SetSelectedGameObject(firstButton);

            configurations.playerStarted[joueur] = true;

            selecting = true;
            started = true;
            this.Invoke("resetSelection", navigationDelay);
        }
    }

    void resetSelection(){
        cursorMoved = false;
        selecting = false;
    }

    public void BackToStart(){
        boutonStart.SetActive(true);
        ecranChoix.SetActive(false);
        configurations.playerStarted[joueur] = false;
        started = false;
        selecting = true;
        this.Invoke("resetSelection", navigationDelay);
    }

    public void IAmReadyBabe(){
        ready = true;
        ecranChoix.SetActive(false);
        ecranPret.SetActive(true);
        this.Invoke("resetSelection", navigationDelay);
    }

    void ReadyScreen(){
        int actionButton = controller.GetActionButton();

        if (actionButton > 0 && !selecting)
        {
            ecranPret.SetActive(false);
            ecranChoix.SetActive(true);

            // Assuming the first button is a child of ecranChoix
            eventSystem.SetSelectedGameObject(firstButton);

            ready = false;
            selecting = true;
            this.Invoke("resetSelection", navigationDelay);
        }
    }

    public void ChosenCar(GameObject choice){
        Debug.Log("tu as choisi : " + choice.name);

        if(joueur == 0){
            configurations.J1VehiculeChoisi = choice;
        }else{
            configurations.J2VehiculeChoisi = choice;
        }
    }
}
