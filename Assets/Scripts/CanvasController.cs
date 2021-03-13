using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public GameObject RestartLevel;
    public GameObject RestartCheckpoint;
    public GameObject RestartGame;
    public GameObject ExitLevel;
    public GameObject NextLevel;

    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI TimeText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI MachineUsesText;

    public int MachineUses;

    private PlayerController player;
    private int startingLives;
    private Coroutine oneSecCo;
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        LivesText.text = "Total Lives: " + GameManager.Instance.PlayerLives;
        startingLives = GameManager.Instance.PlayerLives;
        MachineUsesText.text = "Machine Uses: 0";
        oneSecCo = StartCoroutine(OneSecond());
    }

    private IEnumerator OneSecond()
    {
        for (;;)
        {
            int min = Mathf.FloorToInt(Time.timeSinceLevelLoad / 60);
            int sec = Mathf.FloorToInt(Time.timeSinceLevelLoad % 60);
            TimeText.text = "Time: " + min.ToString("00") + ":" + sec.ToString("00");
            yield return new WaitForSeconds(1);
        }
    }

    public void LostLife()
    {
        GameManager.Instance.PlayerLives -= 1;
        LivesText.text = "Total Lives: " + GameManager.Instance.PlayerLives;
        if (GameManager.Instance.PlayerLives == 0)
        {
            RestartGame.SetActive(true);
            GameManager.Instance.LevelUnlocked[1] = false;
            GameManager.Instance.LevelUnlocked[1] = false;
        }
        else
        {
            RestartCheckpoint.SetActive(true);
            RestartLevel.SetActive(true);
            ExitLevel.SetActive(true);
        }
    }

    public void CompletedLevel()
    {
        StopCoroutine(oneSecCo);
        int i = SceneManager.GetActiveScene().buildIndex;
        if (i == 1) GameManager.Instance.LevelUnlocked[1] = true;
        else if (i == 2) GameManager.Instance.LevelUnlocked[2] = true;
        int livesLost = Mathf.Max(1, startingLives - GameManager.Instance.PlayerLives);
        int score = (int)(100 / livesLost + 1000 / MachineUses + 50000 / Time.timeSinceLevelLoad);
        ScoreText.gameObject.SetActive(true);
        ScoreText.text = "Score: " + score;
        ExitLevel.SetActive(true);
        NextLevel.SetActive(true);
    }
    
    public void LoadSceneOnClick(int i)
    {
        if (GameManager.Instance.PlayerLives == 0)
            GameManager.Instance.PlayerLives = GameManager.Instance.MaxLives;
        if (i == -1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else if (i == -2)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene(i);
    }
    
    public void CheckpointRestart()
    {
        RestartCheckpoint.SetActive(false);
        RestartLevel.SetActive(false);
        ExitLevel.SetActive(false);
        player.Restart();
    }

    public void UsedMachine()
    {
        ++MachineUses;
        MachineUsesText.text = "Machine Uses: " + MachineUses;
    }
}
