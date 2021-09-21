using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 이동
    [SerializeField] float moveSpeed = 3;
    Vector3 dir = new Vector3();
    Vector3 destPos = new Vector3();

    //회전
    [SerializeField] float spinSpeed = 270; // 각도
    Vector3 rotDir = new Vector3(); // 방향
    Quaternion destRot = new Quaternion(); // 목표 회전값

    //반동
    [SerializeField] float recoilPosY = 0.25f;
    [SerializeField] float recoilSpeed = 1.5f;

    //회전시 hit
    bool canMove = true;

    //가짜큐브를 돌려놓고 돌아간 값만큼을 목표회전값으로
    [SerializeField] Transform fakeCube = null;
    [SerializeField] Transform realCube = null;

    TimingManager theTimingManager;

    void Start()
    {
        theTimingManager = FindObjectOfType<TimingManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.W))
        {
            if (canMove)
            {
                if (theTimingManager.CheckTiming()) // return값이 반환됨
                {
                    StartAction();
                }
            }
        }    
    }

    void StartAction()
    {
        //방향계산
        dir.Set(Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        //이동 목표값 계산
        destPos = transform.position + new Vector3(-dir.x, 0, dir.z); // 카메라 방향때문에 - 설정

        //회전 목표값 계산
        rotDir = new Vector3(-dir.z, 0f, -dir.x);
        fakeCube.RotateAround(transform.position, rotDir, spinSpeed);
        destRot = fakeCube.rotation;

        StartCoroutine(MoveCo());
        StartCoroutine(SpinCo());
        StartCoroutine(RecoilCo());

    }

    IEnumerator MoveCo()
    {
        canMove = false;

        while (Vector3.SqrMagnitude(transform.position - destPos) >= 0.001f ) // distance보다 가벼움  제곱근 값 리턴
        {
            transform.position = Vector3.MoveTowards(transform.position, destPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        // while 을 빠져나가면 근소한 차이가 발생할수 있음
        transform.position = destPos;

        canMove = true;
    }

    IEnumerator SpinCo()
    {
        while (Quaternion.Angle(realCube.rotation, destRot) > 0.5f)
        {
            realCube.rotation = Quaternion.RotateTowards(realCube.rotation, destRot, spinSpeed * Time.deltaTime);
            yield return null;
        }
        realCube.rotation = destRot;
    }

    IEnumerator RecoilCo()
    {
        while(realCube.position.y < recoilPosY)
        {
            realCube.position += new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }

        while(realCube.position.y > 0)
        {
            realCube.position -= new Vector3(0, recoilSpeed * Time.deltaTime, 0);
            yield return null;
        }
        realCube.localPosition = new Vector3(0, 0, 0);
    }
}
