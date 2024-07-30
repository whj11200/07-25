using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{

    public float viewRange = 15.0f; // �� ĳ���� ���� ��Ÿ�
    [Range(0, 360)]
    public float viewAngle = 120; //���� �þ߰�
    [SerializeField]
    private Transform enemyTr;
    [SerializeField]
    private Transform playerTr;
    [SerializeField]
    private int playerLayer;
    [SerializeField]
    private int boxLayer;
    [SerializeField] 
    private int barrelLayer;
    [SerializeField]
    private int layerMask;

    void Start()
    {
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.Find("Player").transform.GetComponent<Transform>();

        playerLayer = LayerMask.NameToLayer("Player");
        boxLayer = LayerMask.NameToLayer("Boxes");
        barrelLayer = LayerMask.NameToLayer("Barrel");
        layerMask = 1<<playerLayer|1<< boxLayer | 1<< barrelLayer;
    }
    public Vector3 CirclePoint(float angle)
    {
        // ���� ��ǥ ��������   �����ϱ� ���ؼ� ��ĳ���� Yȸ������ ����
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0f, Mathf.Cos(angle * Mathf.Deg2Rad));
        // �������� 1�� ���������� �����ϸ� x�� sin =x/1 �̶� ���� �����ǰ� 
        // ���� x= sin �̵ǰ� ���������� z�� cos =z/1 �̱⿡ z=cos ����
        // Mathf.Deg2Rad �� ��ȯ ����μ� (PI*2)/360 �� ����.
        // �Ϲݰ��� �������� ��ȯ
        // �׹ݴ뵵 �ִ�. Mathf.Rad2Deg ������ �Ϲݰ����� ��ȭ


    }
    // �÷��̾ �����ؾ� �ϴ� �� �Ǵ� �Լ�
    public bool isTracePlayer()
    {
        bool isTrace = false;
        // 15�ݰ� �ȿ��� �� �����ǿ��� �÷��̾ �ִ����� ����
        Collider[] colls = Physics.OverlapSphere(enemyTr.position
            ,viewRange,1<< playerLayer);
        if(colls.Length == 1)// �÷��̾ ���� �ȿ� �ִٰ� �Ǵ�
        {
            // ��ĳ���Ϳ� ���ΰ� ������ ���� ���͸� ��� ��
            Vector3 dir = (playerTr.position - enemyTr.position).normalized;
            //             �� ��ġ�� ������ �÷��̾� ���� ��ġ�� ���þ߿��� 5���� ������ ��������
            if(Vector3.Angle(enemyTr.forward,dir) < viewAngle * 0.5f)
            {
                // �þ� ����
                isTrace = true;
            }
        }
        return isTrace;

    }
    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit;
        // ���� ����
        Vector3 dir = (playerTr.position - enemyTr.position).normalized;

        // �����ɽ�Ʈ��  ��Ƽ� ��ֹ��� �ִ��� �Ǵ�
        if(Physics.Raycast(enemyTr.position, dir, out hit,viewRange,layerMask))
        {
            isView = hit.collider.CompareTag("Player");
        }
        return isView;

    }
    
}
