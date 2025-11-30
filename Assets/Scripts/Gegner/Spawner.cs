using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Gegners;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(Wellen()); //Starte das spawnen von gegnern
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Wellen() //spawnt alle gegner vom spiel in einer gewissen reihenfolge mit gewissen zeitabständen
    {
        // Welle 1          
        yield return new WaitForSecondsRealtime(5f);            //warte 5 sekunden
        Spawn(0);                                               //spawne gegner 0
        yield return new WaitForSecondsRealtime(12f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(12f);
        Spawn(0);

        // Welle 2
        yield return new WaitForSecondsRealtime(12f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(12f);
        Spawn(0); Spawn(0);

        // Welle 3
        yield return new WaitForSecondsRealtime(11f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(11f);
        Spawn(1);
        yield return new WaitForSecondsRealtime(11f);
        Spawn(0);

        // Welle 4
        yield return new WaitForSecondsRealtime(11f);
        Spawn(0); Spawn(0);
        yield return new WaitForSecondsRealtime(11f);
        Spawn(1);
        yield return new WaitForSecondsRealtime(11f);
        Spawn(0);

        // Welle 5
        yield return new WaitForSecondsRealtime(10f);
        Spawn(0); Spawn(1);
        yield return new WaitForSecondsRealtime(10f);
        Spawn(2);
        yield return new WaitForSecondsRealtime(10f);
        Spawn(0); Spawn(0);

        // Welle 6
        yield return new WaitForSecondsRealtime(9f);
        Spawn(1); Spawn(0);
        yield return new WaitForSecondsRealtime(9f);
        Spawn(2); Spawn(0);

        // Welle 7
        yield return new WaitForSecondsRealtime(8f);
        Spawn(0); Spawn(2);
        yield return new WaitForSecondsRealtime(8f);
        Spawn(1);
        yield return new WaitForSecondsRealtime(8f);
        Spawn(2); Spawn(2);

        // Welle 8
        yield return new WaitForSecondsRealtime(7f);
        Spawn(0); Spawn(1);
        yield return new WaitForSecondsRealtime(7f);
        Spawn(2); Spawn(0);
        yield return new WaitForSecondsRealtime(7f);
        Spawn(0); Spawn(2);

        // Welle 9
        yield return new WaitForSecondsRealtime(6f);
        Spawn(1); Spawn(0);
        yield return new WaitForSecondsRealtime(6f);
        Spawn(2); Spawn(2); Spawn(0);
        yield return new WaitForSecondsRealtime(6f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);
        yield return new WaitForSecondsRealtime(0.3f);
        Spawn(0);


        // Welle 10
        yield return new WaitForSecondsRealtime(10f);
        Spawn(2);
        yield return new WaitForSecondsRealtime(0.5f);
        Spawn(1);
        yield return new WaitForSecondsRealtime(0.5f);
        Spawn(2);

        // Welle 11
        yield return new WaitForSecondsRealtime(10f);
        Spawn(2); Spawn(0);
        yield return new WaitForSecondsRealtime(0.5f);
        Spawn(2);
        yield return new WaitForSecondsRealtime(6f);
        Spawn(2);
        yield return new WaitForSecondsRealtime(0.5f);
        Spawn(1);
        yield return new WaitForSecondsRealtime(0.5f);
        Spawn(2);
        yield return new WaitForSecondsRealtime(0.5f);
        Spawn(2);

        // Welle 12 – Boss
        yield return new WaitForSecondsRealtime(20f);
        Spawn(3);
        Destroy(gameObject);  //zerstöre dich weil keine gegner mehr gespawnnt werden müssen
    }

    private void Spawn(int GegnerNummer) //spawnt den gegner mit dem wert an zufälligen x koordinaten
    {
        GameObject Gegner = Gegners[GegnerNummer];
        Instantiate(Gegner, new Vector3(Random.Range(-75, 76) / 10, 9, 0), transform.rotation);  //erstelle den gegner über dem bildschirm und auf der x koordinate zwischen koordinate x -7.5 und 7.5
    }

    private void FixedUpdate()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player"); //finde den spieler

        if (player == null) //wenn kein spieler existiert
        {
            Destroy(gameObject);  //zerstöre dich
        }
    }
}
