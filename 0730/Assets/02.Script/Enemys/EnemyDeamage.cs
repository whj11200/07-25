using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using UnityEngine.UI;

public class EnemyDeamage : MonoBehaviour
{
    [SerializeField]
    private readonly string BulletTag = "Bullet";
    [SerializeField]
    public GameObject BloodEffect;
    [SerializeField]
    public float hithp = 0;
    public float Maxhp = 100;
    public Text hptext;
    public Image hpimage;
    void Start()
    {
        
        BloodEffect = Resources.Load("Effects/BulletImpactFleshSmallEffect") as GameObject;
        hithp = Maxhp;
        hpimage = GameObject.Find("EnemyUi").transform.GetChild(1).GetComponent<Image>();
        hptext = GameObject.Find("EnemyUi").transform.GetChild(2).GetComponent<Text>();
        UpdateHpUI();

    }
    void OnEnable()
    {
        hithp = Maxhp;
        UpdateHpUI();
    }
    #region projectile ������� ����
    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.collider.CompareTag(BulletTag))
    //    {
    //        other.gameObject.SetActive(false);
    //        ShowBloodEffect(other);
    //        Maxhp -= other.gameObject.GetComponent<Bullet>().Damage;
    //        Maxhp = Mathf.Clamp(Maxhp, 0f, 100f);
    //        if (Maxhp <= 0f)
    //        {
    //            Die();
    //        }
    //    }
    //}
    #endregion
    // �������� �޾����� �Լ��� ȣ�� 
    // object�迭�� ��������
    void OnDamage(object[] _parms)
    {
        // �Լ�ȣ���Ͽ� ��ġ���� 0��° ����
        ShowBloodEffect((Vector3)_parms[0]);
        hithp -= (float)_parms[1];
        hithp = Mathf.Clamp(hithp, 0f, 100f);
        UpdateHpUI();
        if (hithp <= 0f)
        {
            Die();
           
        }
    }
    void UpdateHpUI()
    {
        hpimage.fillAmount = (float)hithp / (float)Maxhp;
        hptext.text = $"HP : <color=#FFAAAA>{hithp}</color>";
        if (hpimage.fillAmount < 0.2f)
            hpimage.color = Color.red;
        else if (hpimage.fillAmount < 0.5f)
            hpimage.color = Color.yellow;
        else
            hpimage.color = Color.green;
    }
    private void ShowBloodEffect(Vector3 col)
    {

        // ���� ��ġ Collision ����ü�ȿ� Contacts��� �迭�� �ִ�.
        //Vector3 pos = other.contacts[0].point; //  ��ġ
        Vector3 pos = col; //  ��ġ
        Vector3 _nomal = col.normalized; // ����
        Quaternion rot = Quaternion.FromToRotation(Vector3.forward, _nomal);
        GameObject blood = Instantiate(BloodEffect, pos, rot);
        Destroy(blood, 1.0f);
    }
    void Die()
    {

        GetComponent<EnemyAi>().state = EnemyAi.State.DIE;
        GameManger.Ginstance.KillScroe();
        
    }
    void FlyingDie()
    {
        GetComponent<EnemyAi>().state = EnemyAi.State.FlyingDead;
        GameManger.Ginstance.KillScroe();
    }
}
