using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public int bpm = 0;
    double currentTime = 0d; // ���� �ּ�ȭ

    [SerializeField] Transform tfNoteAppear = null; // ��Ʈ ������ġ
    [SerializeField] GameObject goNote = null; // prefab ����
    

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= 60d / bpm)
        {
            GameObject t_note = Instantiate(goNote, tfNoteAppear.position, Quaternion.identity);
            t_note.transform.SetParent(this.transform);
            currentTime -= 60d / bpm; // 0���� �ʱ�ȭ �ϸ� �ȵǴ� ���� : 17������ �ణ�� ������ ����
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

}
