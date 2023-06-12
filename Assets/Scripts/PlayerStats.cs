using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    private static int Money, Health, Score;

    [SerializeField]
    private int startMoney = 100, startHealth = 20, moneyPetSecond = 1;

    [SerializeField]
    private TMP_Text healthText, moneyText, scoreText;

    public static int PlusScore{set => Score += value;}
    public static int ChangeMoney{get => Money; set => Money += value;}

    public static int TakeDmg{set => Health -= value;}

    private float moneyCountdown = 0;

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
            ChangeMoney = moneyPetSecond;
            moneyCountdown = 1;
        }
        moneyCountdown -= Time.deltaTime;
    }

}
