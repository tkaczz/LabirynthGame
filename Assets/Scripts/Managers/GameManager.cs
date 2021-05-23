using UnityEngine;

public class GameManager : MonoBehaviour {
	[Header("Gameplay")]
	public int timeToEnd = 240;

	[Header("Keys")]
	public int greenKeys;
	public int redKeys;
	public int goldKeys;

	[Header("Audio")]
	public AudioClip resumeClip;
	public AudioClip pauseClip;
	public AudioClip winClip;
	public AudioClip loseClip;

	public MusicScript MusicScript;

	private bool gamePaused = false;
	private bool endGame = false;
	private bool win = false;
	private int points = 0;

	private AudioSource audioSource;

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
		PlayClipShort(pauseClip);
		MusicScript.OnPauseGame();
		Debug.Log("Game paused");
		gamePaused = true;
		Time.timeScale = 0f;
	}

	public void ResumeGame() {
		PlayClipShort(resumeClip);
		MusicScript.OnResumeGame();
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

	public void PlayClipShort(AudioClip playClip) {
		//jeśli nie ma klipu do odtworzenia, to tego nie robimy
		if (playClip == null) { return; }

		audioSource.PlayOneShot(playClip);
	}

	private void Awake() {
		if (gameManager == null) {
			gameManager = this;
		}

		audioSource = GetComponent<AudioSource>();
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