using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
   public static GameManger Ginstance;
   public bool isGameOver = false; 
   public Text Kill;
    public int killCount = 0;
    void Awake()
    {
        if (Ginstance == null)
            Ginstance = this;
        else if (Ginstance != null)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        Kill = GameObject.Find("PlayerUi").transform.GetChild(7).GetComponent<Text>();
        LoadGameDate();
    }

   
    void Update()
    {
        
    }
    public bool isPaused;
    public void Onpauseclear()
    {
        isPaused = !isPaused;
        Time.timeScale = (isPaused) ? 0 : 1;
        var playerobj = GameObject.FindGameObjectWithTag("Player");
        var scirpts = playerobj.GetComponents<MonoBehaviour>();
        foreach (var mono in scirpts)
        {
            mono.enabled = !isPaused;
        }
        var canvasGroup = GameObject.Find("Panel_Weapen").GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = !isPaused;
        

    }
    public void KillScroe()
    {
        ++killCount;
        Kill.text = $"<color=#0000000>Kill</color> :" + killCount.ToString("000");
        PlayerPrefs.SetInt("KILLCOUNT", killCount);
        //킬수를 저장한다.
    }
    void LoadGameDate()
    {               // 플레이어의 프리퍼런스
        killCount = PlayerPrefs.GetInt("KILLCOUNT",0);
                   // 키값을 예약
        Kill.text = $"<color=#ff0fff>Kill:</color>" +killCount.ToString("000");

     // 실무에서 쓴다고 하면 암호화 해서 저장 해야한다.
    }
    
}
