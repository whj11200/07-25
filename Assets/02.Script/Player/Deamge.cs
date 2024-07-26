using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Codice.Client.Common.WebApi.WebApiEndpoints;
using UnityEngine.UI;

public class Deamge : MonoBehaviour
{
    private readonly string E_BulletTag = "E_Bullet";
    public GameObject Blood;
  
    private int hp = 0;
    private int maxhp = 100;
    private bool isDie = false;
    private Rigidbody rb;
    private CapsuleCollider cp;
    
    public Image Damege;
    [SerializeField]
    private Image HpBar;
    [SerializeField]
    private Text HpText;

    public delegate void PlayerDie();
    public static event PlayerDie OnPlayerDie;

    void Start()
    {
        HpBar = GameObject.Find("PlayerUi").transform.GetChild(2).GetChild(2).GetComponent<Image>();
        HpText = GameObject.Find("PlayerUi").transform.GetChild(2).GetChild(0).GetComponent<Text>();
        Damege = GameObject.Find("PlayerUi").transform.GetChild(0).GetComponent<Image>();
        hp = maxhp;
        Blood = Resources.Load("Effects/BulletImpactFleshSmallEffect") as GameObject;
        rb = GetComponent<Rigidbody>();
        cp = rb.GetComponent<CapsuleCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(E_BulletTag))
        {
            collision.gameObject.SetActive(false);

            // 맞은 위치 Collision 구조체안에 Contacts라는 배열이 있다.
            GameObject blood = ShowBloodEffect(collision);

            hp -= 10;
            HpBarText();

            if (hp <= 0)
            {
                hp = Mathf.Clamp(hp, 0, maxhp);
                Debug.Log("으앙죽음");
                PlayersDie();
            }
            StartCoroutine(showBloodScreen());
        }

    }

    private void HpBarText()
    {
        HpBar.fillAmount = (float)hp / (float)maxhp;
        if (hp ==50)
            HpBar.color = Color.yellow;
        else if (hp == 20)
        {
             HpBar.color = Color.red;
        }
        HpText.text = $"<color=#ff0000>{hp}</color>/{maxhp}";
        if (hp == 50)
            HpText.color = Color.yellow;
        else if (hp == 20)
            HpText.color = Color.black;
    }

    IEnumerator showBloodScreen()
    {
        Damege.color = new Color(1, 0, 0, Random.Range(0.25f, 0.35f));
        yield return new WaitForSeconds(0.1f);
        Damege.color = Color.clear;
    }

    private GameObject ShowBloodEffect(Collision collision)
    {
        Vector3 pos = collision.contacts[0].point; //  위치
        Vector3 _nomal = collision.contacts[0].normal; // 방향
        Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, _nomal);
        GameObject blood = Instantiate(Blood, pos, rot);
        Destroy(blood, 1.0f);
        return blood;
    }

    public void PlayersDie()
    {
        #region 위의 로직은 대형 MMORPG에 맞지가 않다.
        //isDie = true;

        //GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //for (int i = 0; i< enemies.Length; i++)
        //{
        //    enemies[i].gameObject.SendMessage("PlayerDie",SendMessageOptions.DontRequireReceiver);
        //}
        #endregion
        OnPlayerDie();


    }
}
