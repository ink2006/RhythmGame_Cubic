using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null; //텍스트변수

    [SerializeField] int increaseScore = 10; // 디폴트 점수
    int currentScore = 0; // 현재 점수

    [SerializeField] float[] weight = null; // 판정에따른 점수

    Animator myAnim;
    string animScoreUp = "ScoreUp";

    void Start()
    {
        myAnim = GetComponent<Animator>();

        currentScore = 0;
        txtScore.text = "0";
    }

    public void IncreaseScore(int p_JudgementState)
    {
        int t_increaseScore = increaseScore;

        //가중치
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        myAnim.SetTrigger(animScoreUp);
    }
}
