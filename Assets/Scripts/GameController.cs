using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Sprite bgImage;
    public Sprite[] puzzles;

    public List<Sprite> gamePuzzles = new List<Sprite>();
    public List<Button> btns = new List<Button>();

    //Guess control
    private bool firstGuess, secondGuess;
    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessPuzzle, secondGuessPuzzle;
    
    //Buttons control
    public GameObject exitPanel;
    public GameObject gameOverPanel;

    //Game objects interface control
    public Text levelTitle;
    public GameObject infinityClockImage;
    public Text timerCountText;
    public GameObject infinityFailureImage;
    public Text failureCountText;

    //Game obstacles control
    private bool withTimer = false;
    private float timer;
    private bool withfailures = false;
    private int failuresRemains = 0;


    private void Awake()
    {

        //Take the parameters of the level
        string typeImage = LevelParameters.typeImage;
        int level = LevelParameters.levelNumber;
        int timerValue = LevelParameters.timer;
        int failuresValue = LevelParameters.failures;

        //Construct the level
        levelTitle.text = "Level "+level;

        if (timerValue>0) {
            withTimer = true;
            timer = timerValue;
            timerCountText.text = timerValue.ToString();
            infinityClockImage.gameObject.SetActive(false);
            timerCountText.gameObject.SetActive(true);
        }

        if (failuresValue>0) {
            withfailures = true;
            failuresRemains= failuresValue;
            failureCountText.text = failuresValue.ToString();;
            infinityFailureImage.gameObject.SetActive(false);
            failureCountText.gameObject.SetActive(true);
        }

        switch (typeImage)
        {
            case "animals":
                puzzles = Resources.LoadAll<Sprite>("puzzlesImages/animals");
                break;
            case "transport":
                puzzles = Resources.LoadAll<Sprite>("puzzlesImages/transport");
                break;
        }
        
    }
   
   void Start() {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        gameGuesses = gamePuzzles.Count / 2;
        Time.timeScale = 1;
    }

    private void Update() {

        if(Time.timeScale == 0)return;

        //Control the timer of the game, when have timer
        if(withTimer) {
             if(timer>0) {
                timer -= Time.deltaTime;
                timerCountText.text = timer.ToString("0");
            } else {
                gameOver();
            }
        }

        if(withfailures) {
            if(failuresRemains<0) {
                gameOver();
            }
        }
    }

    void GetButtons() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for(int i=0; i < objects.Length; i ++){
            btns.Add(objects[i].GetComponent<Button>());
            btns[i].image.sprite = bgImage;
        }
    }

    void AddGamePuzzles(){
        int lopper = btns.Count;
        int index = 0;

        for(int i = 0; i < lopper; i++){
            if(index == lopper/2) {
                index=0;
            }
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners(){
        foreach (Button btn in btns)
        {
           btn.onClick.AddListener(() => PickAPuzzle());
        }
    }

    public void PickAPuzzle() {

        if (!firstGuess) {

            firstGuess = true;
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];

        } else if (!secondGuess) {

            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;

            StartCoroutine(CheckIfThePuzzlesMatch());
        }
    }

    IEnumerator CheckIfThePuzzlesMatch(){
        yield return new WaitForSeconds (1f);

        if (firstGuessPuzzle == secondGuessPuzzle) {
            yield return new WaitForSeconds (.5f);

            btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);

            CheckIfTheGameIsFinished();

        } else {

            yield return new WaitForSeconds (.5f);

            btns[firstGuessIndex].image.sprite = bgImage;
            btns[secondGuessIndex].image.sprite = bgImage;

            failuresRemains--;

            if(failuresRemains>=0) {
                failureCountText.text = failuresRemains.ToString();
            }
        }

        yield return new WaitForSeconds (.3f);

        firstGuess = secondGuess = false;

    }

    void CheckIfTheGameIsFinished(){
        countCorrectGuesses++;

        if (countCorrectGuesses == gameGuesses) {
            Debug.Log("Game Finished!!");
            Debug.Log("It took you "+ countGuesses + " may gauess(es) to finished the game");
        }
    }

    void Shuffle(List<Sprite> list) {

        for(int i = 0; i < list.Count; i++) {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void showExitPanel() {
        exitPanel.gameObject.SetActive(true);
    }

    public void hiddenExitPanel() {
        exitPanel.gameObject.SetActive(false);
    }

    public void exitLevel() {
        SceneManager.LoadScene("MainMenu");
    }

    public void gameOver(){
        Time.timeScale = 0;
        gameOverPanel.gameObject.SetActive(true);
    }
}
