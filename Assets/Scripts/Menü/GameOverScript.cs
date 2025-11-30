using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    public GameObject playerdeko;
    public GameObject start;
    private float wann;
    private bool Check = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        wann = Time.time + 5;
        GameObject.Find("Hintergrundmusik").GetComponent<AudioLowPassFilter>().enabled = true; //dämpfe die hintergrundmusik
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (transform.position.y >= 3 && Check)         //wenn es zu weit oben ist bewegt es sich runter und es noch nicht angekommen ist
        {
            Bewegen(-0.1f);
        }

        if (wann <= Time.time && Check)    //wenn die zeit vergangen ist und es zum ersten mal laufen würde
        {
            Check = false; //damit es nicht nochmal die folgenden sachen spawnt
            Instantiate(playerdeko, new Vector3(0, -9, 0), transform.rotation);   //spawnt den eine Variante vom Spieler die nicht steuerbar ist unter dem bildschirm
            Instantiate(start, new Vector3(0,9,0), transform.rotation);   //spawnt den Start text über dem bildschirm
        }

        if (transform.position.y <= 9f && !Check)  //wenn nicht ausserhalb vom bildschirm und alles wurde schon gespawnt 
        {
            Bewegen(0.1f); //bewege dich nach oben
        }
        else if (!Check) //wenn ausserhalb vom bildschirm
        {
            Destroy(gameObject);  //zerstöre dich
        }
    }

    private void Bewegen (float y)   //funktion für daas bewegen entlang der y koordinate
    {
        transform.position = new Vector3(0f, transform.position.y + y, 0f);
    }
}
