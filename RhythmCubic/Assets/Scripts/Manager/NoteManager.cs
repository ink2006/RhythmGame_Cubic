using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d; // 오차 최소화

    bool noteActive = true;

    [SerializeField] Transform tfNoteAppear = null; // 노트 생성위치

    TimingManager theTimingManager;
    EffectManager theEffectManager;
    ComboManager theComboManager;

    void Start()
    {
        theEffectManager = FindObjectOfType<EffectManager>();
        theTimingManager = GetComponent<TimingManager>();
        theComboManager = GetComponent<ComboManager>();
    }
    void Update()
    {
        if (noteActive)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= 60d / bpm)
            {
                GameObject t_note = ObjectPool.instance.noteQueue.Dequeue();
                t_note.transform.position = tfNoteAppear.position;
                t_note.SetActive(true);

                theTimingManager.boxNoteList.Add(t_note);
                currentTime -= 60d / bpm; // 0으로 초기화 하면 안되는 이유 : 23때문에 약간의 오차가 생김
            }
        }  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            // hit시 image만 false로 바꾸기때문에 collider가 남아 miss effect를 연출하는 문제 해결
            if (collision.GetComponent<Note>().GetNoteFlag())
            {
                theEffectManager.JudgementEffect(4);
                theComboManager.ResetCombo();
            }


            theTimingManager.boxNoteList.Remove(collision.gameObject);

            ObjectPool.instance.noteQueue.Enqueue(collision.gameObject);
            collision.gameObject.SetActive(false);
            
            //Destroy(collision.gameObject);
        }    
    }

    public void RemoveNote()
    {
        noteActive = false;

        for (int i = 0; i < theTimingManager.boxNoteList.Count; i++)
        {
            theTimingManager.boxNoteList[i].SetActive(false);
            ObjectPool.instance.noteQueue.Enqueue(theTimingManager.boxNoteList[i]); // 노트반납
        }
    }

}
