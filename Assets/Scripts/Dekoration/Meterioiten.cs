using System.Collections;
using UnityEngine;

public class Meterioiten : MonoBehaviour
{
    public GameObject[] objekte;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnerMeteoriten());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnerMeteoriten()  //spawne alle 2 bis 20 sekunden ein random meteorit
    {
        while (true) //wiederhole unendlich mal
        {
            yield return new WaitForSecondsRealtime(Random.Range(2,21)); //warte zwischen 2 und 20 sekunden
            Instantiate(objekte[Random.Range(0,4)], new Vector3(Random.Range(-8f,8.0001f),9,0), transform.rotation);   //spawnt eine Meteorit zufällig überm bildschirm
        }
    }
}
