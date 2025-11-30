using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public int InDifficultyWert;  //wert um von außen die difficulty zu verändern
    private int CurrentWertIn;

    //variables für den spieler:
    private GameObject player;
    private Player playerScript;

    //werte die von außen benutzt werden:
    public int PlayerHP;
    public float PlayerAttackSpeed;
    public float EnemyAttackSpeedMultiplayer;
    public int Gegner1HP;
    public int Gegner2HP;
    public int Gegner3HP;
    public int BossHP;

    //alle möglichen werte für die oberen variables von fode bis hard:
    public int[] AllPlayerHP;
    public float[] AllPlayerAttackSpeed;
    public float[] AllEnemyAttackSpeedMultiplayer;
    public int[] AllGegner1HP;
    public int[] AllGegner2HP;
    public int[] AllGegner3HP;
    public int[] AllBossHP;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DifficultyWertBestimmer()   //setzt alle werte mit der difficulty (diese funktion ist public und wird aufgerufen vom Difficulty script immer wenn die difficulty sich verändert
    {
        //setze die werte von allem zu den werten von der difficulty
        PlayerHP = AllPlayerHP[InDifficultyWert];
        PlayerAttackSpeed = AllPlayerAttackSpeed[InDifficultyWert];

        //setze gleich die werte in den spieler rein falls er existiert:
        player = GameObject.FindGameObjectWithTag("Player");  //finde den spieler mit hilfe von einem tag und packe ihn in eine variable
        if (player != null) //wenn der spieler existiert
        {
            playerScript = player.GetComponent<Player>(); //finde das script von player
            playerScript.hp = PlayerHP;  //setze das hp
            playerScript.Attackspeed = PlayerAttackSpeed; //setze die angriffsgeschwindigkeit
        }

        EnemyAttackSpeedMultiplayer = AllEnemyAttackSpeedMultiplayer[InDifficultyWert];
        Gegner1HP = AllGegner1HP[InDifficultyWert];
        Gegner2HP = AllGegner2HP[InDifficultyWert];
        Gegner3HP = AllGegner3HP[InDifficultyWert];
        BossHP = AllBossHP[InDifficultyWert];
    }
}