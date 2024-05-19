using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int currentscene, counter;
    private static GameManager managerInstance;
    private MusicManager musicManager;
    public List<MoveScript> moveScripts;
    public Breath breath;
    public float bpm, lastBeatTime;
    [Range(0f, 1f)]
    public float breathPoints;
    // Adicionando uma variável para a tolerância de erro no movimento do jogador
    public float playerMoveTolerance, beatInterval;
    public Text text;

    private void Awake()
    {
        if (managerInstance == null)
        {
            managerInstance = this;
        }
        else
        {
            Destroy(gameObject); // Ajustado para destruir o objeto antigo corretamente.
        }
    }

    public static GameManager InstanceManager { get { return GameManager.managerInstance; } }

    void Start()
    {
        musicManager = GetComponent<MusicManager>();
        breath = FindFirstObjectByType<Breath>();
        moveScripts = FindObjectsByType<MoveScript>(FindObjectsSortMode.None).ToList();
        currentscene = SceneManager.GetActiveScene().buildIndex;
        //SetTimerMovement();
        PauseGame();
        ResumeGame();
        beatInterval = 60f / bpm;

        counter = 0;
    }

    void StartDanceRoutine()
    {
        musicManager.ResumeMusic();
        InvokeRepeating(nameof(PerformMove), beatInterval, beatInterval);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        StartCoroutine(ResumeCO());
    }

    IEnumerator ResumeCO()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
        StartDanceRoutine();
    }

    void PerformMove()
    {
        counter++;
        text.text = "" + counter;
        if (counter > 3)
        {
            counter = 0;
        }

        breath.DecreaseBreath(breathPoints);

        foreach (MoveScript move in moveScripts)
        {
            move.Move();
            if (move.gameObject.GetComponent<EnemyScript>() != null)
            {
                move.gameObject.GetComponent<EnemyScript>().EnemyMove();
            }
        }
        lastBeatTime = musicManager.GetMusicTime();
    }
    private void SetTimerMovement()
    {
        switch (currentscene)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
        }
    }
}