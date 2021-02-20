using UnityEngine;

public class GameManagerLekcja : MonoBehaviour {
    public int timeToEnd = 60 * 4;

    private bool gamePaused = false;
    private bool endGame = false;
    private bool win = false;

    //wytłumaczyć dlaczego get; private set;
    public GameManagerLekcja gameManager { get; private set; }

    public void PauseGame() {
        Debug.Log("Pause Game");
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void ResumeGame() {
        Debug.Log("Resume Game");
        Time.timeScale = 1f;
        gamePaused = false;
    }

    public void EndGame() {
        CancelInvoke("Stopper");
        if (win) {
            Debug.Log("You Win!!! Reload?");
        }
        else {
            Debug.Log("You Lose!!! Reload?");
        }
    }

    private void Awake() {
        if (gameManager == null) {
            gameManager = this;
        }
    }

    private void Start() {
        InvokeRepeating("Stopper", 2, 1);
    }

    private void Stopper() {
        timeToEnd--;
        Debug.Log("Time: " + timeToEnd + " s");

        if (timeToEnd <= 0) {
            timeToEnd = 0;
            endGame = true;
        }

        if (endGame) {
            EndGame();
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (gamePaused) {
                ResumeGame();
            }
            else {
                PauseGame();
            }
        }
    }
}