using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour {
	public List<AudioClip> clips;

	private AudioSource audioSource;
	private double pauseClipTime;
	private int actualClip; //nie muszę pisać 0, bo domyślnie int = 0

	public void OnPauseGame() {
		pauseClipTime = audioSource.time;
		audioSource.Pause();
	}

	public void OnResumeGame() {
		audioSource.PlayScheduled(pauseClipTime);
		pauseClipTime = 0;
	}

	private void Awake() {
		audioSource = GetComponent<AudioSource>();
	}

	private void Start() {
		PlayActualMusicClip();
	}

	private void PlayActualMusicClip() {
		audioSource.clip = clips[actualClip];
		audioSource.Play();
	}

	private void Update() {
		if (audioSource.time >= clips[actualClip].length) {
			actualClip++;
			if (actualClip > clips.Count - 1) {
				actualClip = 0;
			}

			PlayActualMusicClip();
		}
	}
}