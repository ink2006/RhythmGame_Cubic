using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null; //텍스트변수

    [SerializeField] int increaseScore = 10; // 디폴트 점수
    int currentScore = 0; // 현재 점수

    [SerializeField] float[] weight = null; // 판정에따른 점수
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
        //콤보
        theComboManager.IncreaseCombo();

        //콤보 추가점수
        int t_currentCombo = theComboManager.GetCurrentCombo();
        int t_bonusComboScore = (t_currentCombo / 10) * comboBonusScore;

        //가중치
        int t_increaseScore = increaseScore + t_bonusComboScore;
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);

        //점수
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        //애니메이션
        myAnim.SetTrigger(animScoreUp);
    }
}
