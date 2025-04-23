using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;

    [HideInInspector]

    public bool isGameOver = false;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void IncreaseCoin()
    {
        coin += 1;
        text.SetText(coin.ToString());


        if (coin % 30 == 0)
        {
            Player player = Object.FindObjectOfType<Player>();
            //What's wrong over here!
            if (player != null)
            {
                player.Upgrade();
            }
        }  
               
        }


    public void SetGameOver()
    {

        isGameOver = true;
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if (enemySpawner != null) { }
        {
            enemySpawner.StopEnemyRoutine();
        }
        Invoke("ShowGameOverPanel", 0.125f);
    }
    void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
    }

   