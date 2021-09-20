using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null; //�ؽ�Ʈ����

    [SerializeField] int increaseScore = 10; // ����Ʈ ����
    int currentScore = 0; // ���� ����

    [SerializeField] float[] weight = null; // ���������� ����
    [SerializeField] int comboBonusScore = 10;


    Animator myAnim;
    string animScoreUp = "ScoreUp";

    ComboManager theComboManager;

    void Start()
    {
        theComboManager = FindObjectOfType<ComboManager>();
        myAnim = GetComponent<Animator>();

        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState)
    {
        //�޺�
        theComboManager.IncreaseCombo();

        //�޺� �߰�����
        int t_currentCombo = theComboManager.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        //����ġ
        int t_increaseScore = increaseScore + t_bonusComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);

        //����
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        //�ִϸ��̼�
        myAnim.SetTrigger(animScoreUp);
    }
}
