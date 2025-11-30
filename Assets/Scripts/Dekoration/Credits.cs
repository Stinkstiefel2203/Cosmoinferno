using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private float min;
    public GameObject Startobj;
    private bool Startcredits = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Hintergrundmusik").GetComponent<AudioSource>().Stop(); //Stoppe die hintergrundmusik falls der boss es davor noch nicht tat
        StartCoroutine(WaitForCredits());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Startcredits)   //seperat damit nix anderes passiert bevor die credits anfangen sollen
        {
            if (transform.position.y > -37)   //wenn die credits zu weit oben sind
            {
                transform.position = transform.position - new Vector3(0f, 0.025f, 0f);        //setzt die position vom objekt zu weiter nach unten
            }
            else if (Time.time > min)  //wenn eine minute vergangen ist und die credits noch nicht ausm bildschirm draußen sind
            {
                if (transform.position.y > -47)
                {
                    transform.position = transform.position - new Vector3(0f, 0.1f, 0f);        //setzt die position vom objekt zu weiter nach unten
                }
                else
                {
                    Instantiate(Startobj, new Vector3(0, 9, 0), transform.rotation);  //spawnt den start knopf
                    GameObject.Find("Hintergrundmusik").GetComponent<AudioSource>().Play(); //Startet die hintergrundmusik wieder
                    Destroy(gameObject);   //zerstöre die credits
                }
            }
        }
    }

    IEnumerator WaitForCredits()  //warte bis die credits beginnen sollen dann starte alles
    {
        yield return new WaitForSecondsRealtime(7); //warte 7 sekunden
        Startcredits = true;  //starte die credits
        min = Time.time + 57; //wenn der song vergangen ist
        gameObject.GetComponent<AudioSource>().Play(); //starte die credits musik
    }
}
