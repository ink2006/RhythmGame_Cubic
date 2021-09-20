using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingManager : MonoBehaviour
{
    public List<GameObject> boxNoteList = new List<GameObject>();

    [SerializeField] Transform Center = null; // 판정범위 중심
    [SerializeField] RectTransform[] timingRect = null; // 판정 범위
    Vector2[] timingBoxs = null; // 실제 판정 최소 최대

    EffectManager theEffect;

    void Start()
    {
        theEffect = FindObjectOfType<EffectManager>();

        // 타이밍 박스 설정
        timingBoxs = new Vector2[timingRect.Length];
        for (int i = 0; i < timingRect.Length; i++)
        {
            timingBoxs[i].Set(Center.localPosition.x - timingRect[i].rect.width / 2,
                              Center.localPosition.x + timingRect[i].rect.width / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTiming()
    {
        for (int i = 0; i < boxNoteList.Count; i++)
        {
            float t_notePosX = boxNoteList[i].transform.localPosition.x;

            for(int x = 0; x < timingBoxs.Length; x++)
            {
                if(timingBoxs[x].x <= t_notePosX && t_notePosX <= timingBoxs[x].y)
                {
                    //노트 제거
                    boxNoteList[i].GetComponent<Note>().HideNote();
                    boxNoteList.RemoveAt(i);

                    //이펙트 연출
                    if (x<timingBoxs.Length -1)  // good이상 판정시에만 이펙트
                        theEffect.NoteHitEffect();

                    theEffect.JudgementEffect(x);

                    Debug.Log("Hit" + x);
                    return;
                }
            }
        }
        theEffect.JudgementEffect(timingBoxs.Length); // 4
        Debug.Log("Miss");
    }
}
