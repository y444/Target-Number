using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefsManager : MonoBehaviour {

    public void ResetCurrentLevel()
    {
        PlayerPrefs.SetInt("currentLevel", 0);
        PlayerPrefs.Save();
    }
}
