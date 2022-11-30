using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameData _gameData;

    public void NextLevel()
    {

        _gameData.LevelIndex++;

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
        SceneManager.LoadScene((_gameData.LevelIndex-1) % _gameData.TotalLevelCount);
        UIManager.instance.SetLevelText();
    }

    public void ReplayLevel()
    {

#if !UNITY_EDITOR
        SaveManager.SaveData(_gameData);
#endif
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
}
