using UnityEngine;

public class Startscript : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject explosion;
    public GameObject Difficulty;
    private int DifficultyWertIn;
    private GameObject DifficultyCopy;
    private GameObject TitleCopy;
    private GameObject DifficultyManager;
    public GameObject[] Difficultys;
    public GameObject Title;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DifficultyManager = GameObject.Find("DifficultyManager");  //finde den difficulty manager
        GameObject.Find("Hintergrundmusik").GetComponent<AudioLowPassFilter>().enabled = true; //dämpfe die hintergrundmusik
        DifficultyWertIn = DifficultyManager.GetComponent<DifficultyManager>().InDifficultyWert;  //suche den wert In
        Instantiate(Difficultys[DifficultyWertIn], new Vector3(20f, 0.1f, 0), transform.rotation);   //spawnt den richtigen Difficulty knopf von der jetzigen Difficulty
        DifficultyCopy = Instantiate(Difficulty, new Vector3 (4.5f,9,0), transform.rotation);   //spawnt den Difficulty knopf
        TitleCopy = Instantiate(Title, new Vector3(0, 11, 0), transform.rotation);   //spawnt den Title knopf
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y >= 1)         //wenn es zu weit oben ist bewegt es sich und die Difficulty runter
        {
            transform.position = new Vector3(-5f, transform.position.y - 0.1f, 0f);
            DifficultyCopy.transform.position = new Vector3(4.5f, transform.position.y + 0.1f, 0f);
            TitleCopy.transform.position = new Vector3(0, transform.position.y + 2, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)          //wenn eine collision mit dem start passiert
    {
        if (collision.gameObject.tag == "Schussgut")         // wenn das objekt was start berührt hat eine kugel ist
        {
            Destroy(collision.gameObject);        //zerstört die kugel
            GameObject.Find("Hintergrundmusik").GetComponent<AudioLowPassFilter>().enabled = false; //entdämpfe die hintergrundmusik
            Instantiate(explosion, transform.position, transform.rotation);   //spawnt eine explosion am start knopf
            Instantiate(Spawner, transform.position, transform.rotation);   //spawnt den Spawner um gegner zu spawnen
            Instantiate(explosion, DifficultyCopy.transform.position, transform.rotation);   //spawnt eine explosion am Difficulty knopf
            Destroy(DifficultyCopy);// zerstöre den Difficulty Knopf
            Instantiate(explosion, TitleCopy.transform.position, transform.rotation);   //spawnt eine explosion am Titel
            Destroy(TitleCopy);// zerstöre den Titel
            Destroy(GameObject.Find("DifficultyText")); //Zerstört die Difficulty Texte
            Destroy(gameObject);  //zerstört den start knopf
        }
    }
}
