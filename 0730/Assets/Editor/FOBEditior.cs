using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// EnemyFOV ����
[CustomEditor(typeof(EnemyFOV))]
public class FOBEditior : Editor
{
    // �ź信���� �ð����� ǥ���� �ϴ� �Լ�
    // ������ �������� �˱�����
    private void OnSceneGUI()
    {
        EnemyFOV fov = (EnemyFOV)target;
        //���� ���� ��������ǥ�� ��� (�־��� ���� 1/2)

        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);
        //���� ������ ������� ����
        Handles.color = Color.white;
                              // �� ��ġ��           ��� ���� , ��ä�� ���� ��ǥ,��ä�ð���, ��ä���ǹ�����
        Handles.DrawWireDisc(fov.transform.position, Vector3.up,fov.viewRange);

        Handles.color = new Color(1, 1, 1, 0.2f);
        // ä���� ��ä���� �׸�
        Handles.DrawSolidArc(fov.transform.position,//���� ��ǥ 
            Vector3.up, // ��ֺ���
            fromAnglePos,  // ��ä���� ���� ��ǥ
            fov.viewRange, // ��ä���� ����
            fov.viewAngle); // ��ä���� ������
        // �þ߰��� �ؽ�Ʈ�� ǥ��
                     //
        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f), fov.viewAngle.ToString());       
    }
}
