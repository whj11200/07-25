using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;


public class UiManger : MonoBehaviour
{
    public string LevelScene = "Level_1";
    public string BettalFiledScene = "BattleFiled";
   public void OnClikPlayBtn()
    {
        SceneManager.LoadScene(LevelScene);
        //LoadSceneMode.Additive == ���� ���� ���� ���� �ʰ� �߰� �ؼ� ���ο� ���� �ε��Ѵ�.
        //single == ������ �ε�� ���� ��� ���� �� ���ο� ���� �ε��Ѵ�.
        SceneManager.LoadScene(BettalFiledScene,LoadSceneMode.Additive);


    }
    public void OnClickQuitBtn()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(); 
#endif
    }
}
