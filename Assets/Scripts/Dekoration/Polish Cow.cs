using UnityEngine;

public class PolishCow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject.Find("Hintergrundmusik").GetComponent<AudioLowPassFilter>().enabled = true; //dämpfe die hintergrundmusik
        GameObject.Find("Boss").GetComponent<AudioLowPassFilter>().enabled = true; //dämpfe die Boss musik
        //Damit man die Polish cow musik besser hört
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 3.5f)    //wenn die polish cow im ufo ist entferne die polish cow
        {
            GameObject.Find("Hintergrundmusik").GetComponent<AudioLowPassFilter>().enabled = false; //entdämpfe die hintergrundmusik
            GameObject Bossobj = GameObject.Find("Boss");  //finde den boss
            if (Bossobj != null)  //wenn der boss existiert
            {
                Bossobj.GetComponent<AudioLowPassFilter>().enabled = false; //entdämpfe die Boss musik
            }
            Destroy(gameObject);   //entferne die polish Cow
        }
    }
}
