using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public List<string> levels;

    public void LoadLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count || levels[levelIndex] == "")
            return;
        SceneManager.LoadScene(levels[levelIndex]);
    }
}
