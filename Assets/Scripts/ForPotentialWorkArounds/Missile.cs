using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject missileTarget;
    public Vector3 currentTarget;
    public float speed;
    public bool triggered; // True, jos missile on osunnut targettiin ja voi etsiä pelaajan kohteekseen


    void Start()
    {
        missileTarget = GameObject.FindGameObjectWithTag("MissileTarget");
        currentTarget = missileTarget.transform.position;
    }

    
    void Update()
    {
        Vector3 vectorToTarget = currentTarget - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if(triggered && Vector3.Distance(transform.position, currentTarget) <1)
        {
            Destroy(gameObject); // Voit myös instansioida jonkin partikkeliefektin tms. poksahduksen efektinä.
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MissileTarget"))
        {
            triggered = true;
            currentTarget = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
}
