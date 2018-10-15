using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour {

	public void Play(AudioSource sound)
    {
        sound.Play();
    }

    public void Shuffle(AudioSource[] sound)
    {
        int selectedSound = Random.Range(0, sound.Length + 1);
        sound[selectedSound].Play();

    }
}
