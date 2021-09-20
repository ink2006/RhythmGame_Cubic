using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Text txtScore = null; //�ؽ�Ʈ����

    [SerializeField] int increaseScore = 10; // ����Ʈ ����
    int currentScore = 0; // ���� ����

    [SerializeField] float[] weight = null; // ���������� ����

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

        //����ġ
        t_increaseScore = (int)(t_increaseScore * weight[p_JudgementState]);
        currentScore += t_increaseScore;
        txtScore.text = string.Format("{0:#,##0}", currentScore);

        myAnim.SetTrigger(animScoreUp);
    }
}
