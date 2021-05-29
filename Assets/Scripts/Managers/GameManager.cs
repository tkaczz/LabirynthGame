using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

	[Header("UI")]
	public Text timeText;
	public Text goldKeyText;
	public Text redKeyText;
	public Text greenKeyText;
	public Text crystalText;
	public Image snowFlake;

	public GameObject infoPanel;
	public Text pauseEnd;
	public Text reloadInfo;
	public Text useInfo;

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
				greenKeyText.text = greenKeys.ToString();
				break;
			case Keys.Red:
				redKeys++;
				redKeyText.text = redKeys.ToString();
				break;
			case Keys.Gold:
				goldKeys++;
				goldKeyText.text = goldKeys.ToString();
				break;
		}
	}

	public void AddPoints(int pointsToAdd) {
		points += pointsToAdd;
		crystalText.text = points.ToString();
	}

	public void FreezeTime(int freezeAmount) {
		CancelInvoke("Stopper");
		snowFlake.enabled = true;
		InvokeRepeating("Stopper", freezeAmount, 1);
	}

	public void AddTime(int timeToAdd) {
		timeToEnd += timeToAdd;
		timeText.text = timeToEnd.ToString();
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

	public void WinGame() {
		win = true;
		endGame = true;
	}

	private void Awake() {
		if (gameManager == null) {
			gameManager = this;
		}

		audioSource = GetComponent<AudioSource>();
	}

	private void SetUseInfo(string info) {
		useInfo.text = info;
	}

	private void Start() {
		snowFlake.enabled = false;
		timeText.text = timeToEnd.ToString();
		pauseEnd.text = "Pause";
		reloadInfo.text = string.Empty;

		SetUseInfo(string.Empty);
		audioSource = GetComponent<AudioSource>();
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

		if (endGame) {
			if (Input.GetKeyDown(KeyCode.Y)) {
				SceneManager.LoadScene(0);
			}
			if (Input.GetKeyDown(KeyCode.N)) {
				Application.Quit();
			}
		}
	}
}