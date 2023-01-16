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
    [SerializeField]
    TextMeshProUGUI m_gameOverText;
    private void Start()
    {
        m_gameOverText.text = "";
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
        int[] pointsList = { 30, 25, 20, 15, 12 };
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
        m_gameOverText.text = "GAME OVER";
        m_timeText.text = "Average cast time: " + m_timeAverage.ToString("F2") + " s";
        m_enemyText.text = "Enemies slain: " + m_enemiesSlain.ToString();
        m_gradeText.text = "Final grade: " + m_finalGrade.ToString("F1");
        if(m_finalGrade <= 4.0f)
        {
            m_gameOverText.text = "YOU PASSED";
        }
    }
    void ResetScore()
    {
        m_gameOverText.text = "";
        m_timeText.text = "";
        m_enemyText.text = "";
        m_gradeText.text = "";
        m_enemiesSlain = 0;
        m_prevGrade = m_finalGrade;
    }
    
}
