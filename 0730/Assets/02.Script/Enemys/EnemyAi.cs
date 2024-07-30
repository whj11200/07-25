
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Net.WebRequestMethods;
[RequireComponent(typeof(Animator))]
public class EnemyAi : MonoBehaviour
{
    public enum State// 열거형 상수
    {
        PTROL = 0, TRACE, ATTACK, DIE,Dence,FlyingDead
    }
    public State state = State.PTROL;
    [SerializeField] private Transform Playertr; // 거리를 재기위해 선언
    [SerializeField] private Transform Enemytr;  // 거리를 재기위해 선언
    [SerializeField] private Animator animator; // 애니메이터
    [SerializeField] private EnemyFOV enemyFOV;
    // 공격 거리 추적 거리
    public float attackDist = 10.0f; // 공격사거리
    public float traceDist = 10f;  // 추적 사거리
    public bool isDie = false; // 사망여부
    private WaitForSeconds ws; // 
    private Enemy enemy;
    // 애니메이터 컨트롤러에 정의 한 파라미터의 해시값을 정수로 미리 추출
    private readonly int hashMove = Animator.StringToHash("Is_Move");
    private readonly int hashSpeed = Animator.StringToHash("MoveSpeed");
    private readonly int hashReload = Animator.StringToHash("Reload");
    private readonly int hashDie = Animator.StringToHash("Die");
    private readonly int hashIdx = Animator.StringToHash("DieIdx");
    private readonly int hashoffset = Animator.StringToHash("Offset");
    private readonly int hashWalkSpeed = Animator.StringToHash("WalkSpeed");
    private readonly int hashDance = Animator.StringToHash("Dance");
    public EnemyDeamage enemyDeamage;
 
  

    private EnemyFire enemyFire;
    void Awake()
    {
        
        enemyDeamage = GetComponent<EnemyDeamage>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
        enemyFire = GetComponent<EnemyFire>();
        var Player = GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            Playertr = Player.GetComponent<Transform>();
        }
        Enemytr = GetComponent<Transform>();
        ws = new WaitForSeconds(0.3f);
        enemyFOV = GetComponent<EnemyFOV>();
    }
    private void OnEnable() //  오브젝트가 활성화 될때 호출
    {
        Deamge.OnPlayerDie += PlayerDie;
        // 이벤트 연결
        animator.SetFloat(hashoffset, Random.Range(0.3f,1.0f));
        animator.SetFloat(hashWalkSpeed, Random.Range(0.0f,5.0f));
        StartCoroutine(CacheState());
        StartCoroutine(Action());
    }
    IEnumerator CacheState()
    {
        yield return new WaitForSeconds(1.0f);
        // 오브젝트 풀에 생성시  다른 스크립트의 초기화를 위해 대기
        while (!isDie)
        {
            if (state == State.DIE || state == State.FlyingDead) yield break;
            // 사망 상태이면 코루틴 함수를 종료 시킴
            float dist = (Playertr.position - Enemytr.position).magnitude;
            // 만약 공격거리에 들어온다면
            if (dist <= attackDist)
            {
                if (enemyFOV.isViewPlayer())
                {
                    state = State.ATTACK;// ATTACK활성화
                }
                else 
                    state = State.TRACE;
               
            }
            // 추격거리에 들어오면
            else if (enemyFOV.isTracePlayer())
            {
                state = State.TRACE;
            }
            else
            {
                state = State.PTROL;
            }
            yield return ws;
        }

    }
    IEnumerator Action()
    {
        while (!isDie)
        {
            yield return ws;
            switch (state)
            {
                case State.PTROL:
                    enemyFire.isFire = false;
                    enemy.patroiing = true;
                    animator.SetBool(hashMove, true);
                    break;
                case State.ATTACK:
                    enemyFire.isFire = true;
                    enemy.Stop();
                    animator.SetBool(hashMove, false);
                    break;
                case State.TRACE:
                    enemyFire.isFire = false;
                    enemy.traceTaget = Playertr.position;
                    animator.SetBool(hashMove, true);
                    break;
                case State.DIE:
                    EnemyDie();
                    break;
                case State.Dence:
                    PlayerDie();
                    break;
                case State.FlyingDead:
                    FlyingDead();
                    break;


            }
        }
        
    }

    private void EnemyDie()
    {
        
        enemy.Stop();
        enemyFire.isFire = false;
        animator.SetTrigger(hashDie);
        animator.SetInteger(hashIdx, Random.Range(0, 3));
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
        gameObject.tag = "Untagged";
        
        
        StartCoroutine(PoolPush());
        isDie = true;

    }
    IEnumerator PoolPush()
    {
        
        
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<CapsuleCollider>().enabled = true;
        gameObject.tag = "Enemy"; //  오브젝트가 활성화 되기전 태그이름을 원래대로
        
        isDie = false;
        state = State.PTROL;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);



    }
    void PlayerDie()
    {
        StopAllCoroutines();
        animator.SetTrigger(hashDance);
        GameManger.Ginstance.isGameOver = true;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;

    }
    void FlyingDead()
    {
        if (isDie) return;
        enemy.Stop();
        enemyFire.isFire = false;
        animator.SetTrigger(hashDie);
        animator.SetInteger(hashIdx, 0);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
        gameObject.tag = "Untagged";

        StartCoroutine(PoolPush());
        isDie = true;
    }

    void Update()
    {
        animator.SetFloat(hashSpeed,enemy.speed);
    }
    private void OnDisable()
    {
        Deamge.OnPlayerDie -= PlayerDie;
    }
}
