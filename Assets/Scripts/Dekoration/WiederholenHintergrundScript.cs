using UnityEngine;

public class WiederholenHintergrundScript : MonoBehaviour
{
    public GameObject Hintergrundprefab;
    private bool wiederholung = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.name = "Hintergrund";  //nenne den hintergeund hintergrund weil unity automatisch immer (cloned) schreibt wenn ein objekt mit dem gleichen namen erstellt wird damit es nicht irgentwan einen namen mit unendlich vielen (cloned) am ende hat
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= -24 && wiederholung) //wenn der hintergrund zu weit unten ist und wiederholung wahr ist
        {
            wiederholung = false;   //damit es nicht jeden frame einen neuen hintergrund erstellt wenn es zu weit unten ist
            Instantiate(Hintergrundprefab, transform.position + new Vector3(0f, 63.32f, 0f), transform.rotation); //wenn der hintergrund zu weit unten ist dann wird ein neuer erstellt der wieder oben ist
        }
        if (transform.position.y <= -39)
        {
            Destroy(gameObject);   //entferne den hintergrund weil er zu weit unten ist um gesehen zu werden }
        }
    }
}
