using UnityEngine;

public class GameManager : MonoBehaviour {
    public int timeToEnd = 240;

    private bool gamePaused = false;
    private bool endGame = false;
    private bool win = false;

    private int greenKeys;
    private int redKeys;
    private int goldKeys;

    private int points = 0;

    public static GameManager gameManager { get; private set; }

    public void AddKey(Keys keyType) {
        switch (keyType) {
            case Keys.Green:
                greenKeys++;
                break;
            case Keys.Red:
                redKeys++;
                break;
            case Keys.Gold:
                goldKeys++;
                break;
        }
    }

    public void AddPoints(int pointsToAdd) {
        points += pointsToAdd;
    }

    public void FreezeTime(int freezeAmount) {
        CancelInvoke("Stopper");
        InvokeRepeating("Stopper", freezeAmount, 1);
    }

    public void AddTime(int timeToAdd) {
        timeToEnd += timeToAdd;
    }

    public void EndGame() {
        CancelInvoke("Stopper");
        if (win) {
            Debug.Log("You win!! Reload?");
        }
        else {
            Debug.Log("You loose!! Reload?");
        }
    }

    public void PauseGame() {
        Debug.Log("Game paused");
        gamePaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Debug.Log("Game resumed");
        gamePaused = false;
        Time.timeScale = 1.0f;
    }

    public void Stopper() {
        timeToEnd--;
        Debug.Log("Time: " + timeToEnd + "s");

        if (timeToEnd <= 0) {
            timeToEnd = 0;
            endGame = true;
        }

        if (endGame) {
            EndGame();
        }
    }

    private void Awake() {
        if (gameManager == null) {
            gameManager = this;
        }
    }

    private void Start() {
        InvokeRepeating("Stopper", 1, 1);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (gamePaused == true) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }
}