using UnityEditor;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public GameObject explosion;
    public GameObject nextDiff;   //nächste Difficulty die gewählt werden soll
    private GameObject DifficultyManager;    //um Difficulty Wert von außen benutzten zu können
    public int DifficultyWert; //welcher wert die difficulty an den manager weitergibt
    private int DifficultyManagerIn;
    private DifficultyManager targetScript;
    private bool Sicherung = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.name = "DifficultyText"; //um von dem start des spiels erkennbar zu sein um auch gelöscht zu werden
        DifficultyManager = GameObject.Find("DifficultyManager");  //finde den difficulty manager
        targetScript = DifficultyManager.GetComponent<DifficultyManager>(); //finde das script vom DifficultyManager
        DifficultyManager.GetComponent<DifficultyManager>().InDifficultyWert = DifficultyWert;  //suche den wert In und setze den wert In zu den gewünschten
        targetScript.DifficultyWertBestimmer(); //starte die Funktion im Difficulty Manager um die werte zu bekommen
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)          //wenn eine collision mit dem start passiert
    {
        if (collision.gameObject.tag == "Schussgut" && Sicherung)         // wenn das objekt was die Difficulty berührt hat eine kugel ist und es zum ersten mal getroffen wurde
        {
            Sicherung = false; //damit es nicht nochmal getroffen werden kann (macht alles kaputt wenn man ohne Sicherung sehr schnell drauf schießt)
            Destroy(collision.gameObject);        //zerstört die kugel
            Instantiate(explosion, transform.position, transform.rotation);   //spawnt eine explosion an der Difficulty
            Instantiate(nextDiff, transform.position + new Vector3(7,0,0), transform.rotation); //spawnt die nächste difficulty außerhalb vom bildschirm
            Destroy(gameObject);  //zerstört den knopf
        }
    }
    private void FixedUpdate()
    {
        if (transform.position.x > 4.5)         //wenn es zu weit rechts ist bewegt es sich links
        {
            transform.position = new Vector3(transform.position.x - 0.1f, 0.1f, 0f); //bewege dich nach links
        }
    }
}
