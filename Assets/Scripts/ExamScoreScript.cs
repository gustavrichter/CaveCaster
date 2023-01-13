using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExamScoreScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ExamScript examScript;
    float m_timeAverage;
    int m_enemiesSlain;
    float m_finalGrade;
    float m_prevGrade;
    bool m_bcountTime;
    float m_currentTime;
    [SerializeField]
    TextMeshProUGUI m_timeText;
    [SerializeField]
    TextMeshProUGUI m_enemyText;
    [SerializeField]
    TextMeshProUGUI m_gradeText;
    private void Start()
    {
        m_timeText.text = "";
        m_enemyText.text = "";
        m_gradeText.text = "";
        examScript.m_magicBookScript.PageTurned += StartTimeCount;
        examScript.m_pageScript.SpellWasFired += StopTimeCount;
        examScript.m_caveScript.EnemyHasBeenSlain += AddEnemyKill;
        examScript.m_playerScript.PlayerDeath += FinishExam;
        examScript.m_playerScript.RestartTheGame += ResetScore;
       

    }

    private void Update()
    {
        if (m_bcountTime)
        {
            m_currentTime += Time.deltaTime;
        }
    }
    void StartTimeCount()
    {
        m_currentTime = 0.0f;
        m_bcountTime = true;
    }

    void StopTimeCount()
    {
        if(m_timeAverage > 0.0f)
        {
            //after first time
            m_timeAverage += m_currentTime;
            m_timeAverage /= 2;
        }
        else
        {
            //first time 
            m_timeAverage = m_currentTime;
        }
        m_bcountTime = false;
    }

    void AddEnemyKill()
    {
        //Debug.Log("enemy killed. " + m_enemiesSlain.ToString());
        m_enemiesSlain++;
    }

    void calculateFinalGrade()
    {
        int[] pointsList = { 20, 18, 14, 10, 5 };
        float[] gradeList = { 1.0f, 2.0f, 3.0f, 4.0f, 5.0f };
        
        if (m_bcountTime)
        {
            StopTimeCount();
        }
        for (int i = 0; i < 5; i++)
        {
            if(pointsList[i]>= m_enemiesSlain)
            {
                m_finalGrade = gradeList[i];
            }
        }
       // m_finalGrade = m_enemiesSlain / m_timeAverage;

    }

    
    void FinishExam()
    {
        examScript.m_magicBookScript.CloseBook();
        examScript.m_caveScript.StopStage();
        calculateFinalGrade();
        examScript.m_playerScript.PlayDeathMusic();
        //show end result screen
        m_timeText.text = "average cast time: " + m_timeAverage.ToString("F2") + " s";
        m_enemyText.text = "enemies slain: " + m_enemiesSlain.ToString();
        m_gradeText.text = "final grade: " + m_finalGrade.ToString("F1");
        if (m_enemiesSlain < 5)
        {
            m_gradeText.text = "final grade: n.a.";
        }
    }
    void ResetScore()
    {
        m_timeText.text = "";
        m_enemyText.text = "";
        m_gradeText.text = "";
        m_enemiesSlain = 0;
        m_prevGrade = m_finalGrade;
    }
    
}
