using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
// EnemyFOV 참전
[CustomEditor(typeof(EnemyFOV))]
public class FOBEditior : Editor
{
    // 신뷰에서만 시각적인 표지를 하는 함수
    // 원주의 시작점을 알기위해
    private void OnSceneGUI()
    {
        EnemyFOV fov = (EnemyFOV)target;
        //원주 위의 시작점좌표를 계산 (주어진 각도 1/2)

        Vector3 fromAnglePos = fov.CirclePoint(-fov.viewAngle * 0.5f);
        //원의 색상은 흰색으로 고정
        Handles.color = Color.white;
                              // 원 위치값           노멀 벡터 , 부채꼴 시작 좌표,부채꼴각도, 부채꼴의반지름
        Handles.DrawWireDisc(fov.transform.position, Vector3.up,fov.viewRange);

        Handles.color = new Color(1, 1, 1, 0.2f);
        // 채워진 부채꼴을 그림
        Handles.DrawSolidArc(fov.transform.position,//원점 좌표 
            Vector3.up, // 노멀벡터
            fromAnglePos,  // 부채꼴의 시작 좌표
            fov.viewRange, // 부채꼴의 각도
            fov.viewAngle); // 부채꼴의 반지름
        // 시야각의 텍스트를 표시
                     //
        Handles.Label(fov.transform.position + (fov.transform.forward * 2.0f), fov.viewAngle.ToString());       
    }
}
