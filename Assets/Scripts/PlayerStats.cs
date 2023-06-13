using System.Globalization;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private static int Money, Health, Score;

    [SerializeField]
    private int startMoney = 200, startHealth = 20, moneyPerSecond = 1;

    [SerializeField]
    private TMP_Text healthText, moneyText, scoreText;

    

    public static int PlusScore{set => Score += value;}
    public static int ChangeMoney{get => Money; set => Money += value;}
    public static int GetScore{get => Score;}

    private float moneyCountdown = 0;

    public static PlayerStats instance;

    private void Awake() { 
        if(instance != null){
            Debug.Log("More than one PlayerStats!");
            return;
        }
        instance = this;
    }

    private void Start() {
        Money = startMoney;
        Health = startHealth;
        Score = 0;
    }

    private void Update() {
        moneyText.text = "Coins: " + Money.ToString();
        healthText.text = "Base health: " + Health.ToString();
        scoreText.text = "Score :" + Score.ToString();
        if(moneyCountdown <=0){
            ChangeMoney = moneyPerSecond;
            moneyCountdown = 0.5f;
        }
        moneyCountdown -= Time.deltaTime;
    }

    public static void TakeDmg(int dmg){
        Health -= dmg;
        if(Health <= 0){
            instance.SaveScore();
        }
    }
    
    public void SaveScore(){
        ScoreManager.AddScore();
        ScoreManager.SaveScore();
        GameSceneManager.LoadMainMenu();
    }
}
