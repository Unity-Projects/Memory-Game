using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject challengerModePanel;
    public GameObject freeModePanel;
    public GameObject configPanel;
    public GameObject goalsPanel;
    public Text goalTitleText;
    public Text goalDescriptionText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mainMenuController(int typePanel) {
        switch(typePanel) {
            case 1:
                mainMenuPanel.gameObject.SetActive(false);
                challengerModePanel.gameObject.SetActive(true);
            break;
            case 2:
                mainMenuPanel.gameObject.SetActive(false);
                freeModePanel.gameObject.SetActive(true);
            break;
            case 3:
                mainMenuPanel.gameObject.SetActive(false);
                configPanel.gameObject.SetActive(true);
            break;
            default:
                challengerModePanel.gameObject.SetActive(false);
                freeModePanel.gameObject.SetActive(false);
                configPanel.gameObject.SetActive(false);
                mainMenuPanel.gameObject.SetActive(true);
            break;
        }
    }

    //Seta os parametros de dificuldade do level selecionado
    public void selectLevel(int level) {

        LevelParameters.levelNumber = level;

        //Melhorar esse switch
        switch (level)
        {
            case 1:
                
                LevelParameters.typeImage = "animals";
                LevelParameters.cardsNumber = 8;
                LevelParameters.timer = 0;
                LevelParameters.failures = 0;
                break;
            case 2:
                LevelParameters.typeImage = "transport";
                LevelParameters.cardsNumber = 8;
                LevelParameters.timer = 0;
                LevelParameters.failures = 0;
                break;
            case 3:
                LevelParameters.typeImage = "animals";
                LevelParameters.cardsNumber = 12;
                LevelParameters.timer = 0;
                LevelParameters.failures = 0;
                break;
            case 4:
                LevelParameters.typeImage = "animals";
                LevelParameters.cardsNumber = 12;
                LevelParameters.timer = 10;
                LevelParameters.failures = 0;
                break;
            case 5:
                LevelParameters.typeImage = "animals";
                LevelParameters.cardsNumber = 12;
                LevelParameters.timer = 0;
                LevelParameters.failures = 3;
                break;
        }

        showGoalsPanel(level);

    }

    //Constroi o painel de objetivos
    private void showGoalsPanel(int level) {

        challengerModePanel.gameObject.SetActive(false);
        goalsPanel.gameObject.SetActive(true);

        goalTitleText.text = "Goals - Level "+level;
        goalDescriptionText.text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.";
    }

    public void closeGoalsPanel(){
        goalsPanel.gameObject.SetActive(false);
        challengerModePanel.gameObject.SetActive(true);
    }

    public void startLevel() {

        SceneManager.LoadScene("ChallengeMode");
    }

}
