using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d; // 오차 최소화

    [SerializeField] Transform tfNoteAppear = null; // 노트 생성위치
    [SerializeField] GameObject goNote = null; // prefab 변수
    

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            currentTime -= 60d / bpm; // 0으로 초기화 하면 안되는 이유 : 17때문에 약간의 오차가 생김
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
