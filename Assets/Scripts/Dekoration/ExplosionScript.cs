using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float time = 2.083f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,time);  //zerstört die explosion nach dem abschließen der animation
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
