using Script.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Script.Util.Facade;

public class MainMenu : MonoSingleton<MainMenu>
{
    public string NewGameScene;
    #region Button

    [SerializeField] private Button m_NewGame;
    [SerializeField] private Button m_LoadGame;
    [SerializeField] private Button m_Exit;

    #endregion

    public void Exit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void NewGame() => _LoadSceneManager.LoadScene(NewGameScene);
}