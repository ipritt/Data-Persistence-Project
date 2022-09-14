using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public TMP_InputField playerName;

    // Start is called before the first frame update
    void Start()
    {
        playerName = GameObject.Find("InputField (TMP)").GetComponent<TMP_InputField>();
    }

    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        PlayerManager.instance.SavePlayerData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void NameChanged()
    {
        PlayerManager.instance.playerName = playerName.text;
    }
}
