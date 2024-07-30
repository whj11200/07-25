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
        //LoadSceneMode.Additive == 기존 씬을 삭제 하지 않고 추가 해서 새로운 씬을 로드한다.
        //single == 기존에 로드된 씬을 모두 삭제 후 새로운 씬을 로드한다.
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
