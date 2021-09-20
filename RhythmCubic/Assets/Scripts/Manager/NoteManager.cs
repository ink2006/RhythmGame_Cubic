using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d; // 오차 최소화

    [SerializeField] Transform tfNoteAppear = null; // 노트 생성위치
    [SerializeField] GameObject goNote = null; // prefab 변수

    TimingManager theTimingManager;
    EffectManager theEffectManager;

    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
    }
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            theTimingManager.boxNoteList.Add(t_note);
            currentTime -= 60d / bpm; // 0으로 초기화 하면 안되는 이유 : 17때문에 약간의 오차가 생김
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            // hit시 image만 false로 바꾸기때문에 collider가 남아 miss effect를 연출하는 문제 해결
            if(collision.GetComponent<Note>().GetNoteFlag())
                theEffectManager.JudgementEffect(4);

            theTimingManager.boxNoteList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }    
    }
}
