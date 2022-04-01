using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{

    public float moveSpeed; // Rushin liikkumisnopeus
    public int direction; // Suunta suhteessa pelaajaan 1 tai -1
    public float hitPoints; // Ei viel‰ k‰ytˆss‰
    public Animator animator;
    public Rigidbody2D rb2D;
    public GameObject rollingBullet;
    public GameObject missile;
    public GameObject spawnMissile;
    public GameObject spawnRollingBullet;
    public GameObject mario;
    public float idleCounter;
    public float maxIdleCounter; // S‰‰t‰m‰ll‰ t‰t‰ pienemm‰ksi, saa Bossin toimimaan nopeammin. 
    public bool idling; // Tiedet‰‰n, onko boss idless‰. Jos on, sille voi antaa jonkun toiminnon
    public int currentAction; // Numero, joka kertoo mik‰ toiminto pit‰‰ suorittaa 


    // Start is called before the first frame update
    void Start()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(idleCounter > maxIdleCounter && idling)
        {
            idleCounter = 0;
            EndIdle();
        }
        else
        {
            idleCounter += Time.deltaTime;
        }

        transform.Translate(moveSpeed * direction * Time.deltaTime, 0, 0);
    }

    void Idling()
    {
        idling = true;
        StopAllCoroutines(); // test

    }

    void GoToIdle()
    {
        Debug.Log("Going to idle");
        animator.SetTrigger("GoToIdle");


    }

    void EndIdle()
    {
        idling = false;
        currentAction = Mathf.FloorToInt(Random.Range(0, 3)); // tarkoitus on saada jokin arvo 0-2 v‰lill‰.
        // 0 = Rush, 1 =  Missile, 2 = RollingBullet
        Debug.Log("ACTION: " + currentAction);
        switch (currentAction)
        {
            case 0:
                Rush();
                break;
            case 1:
                OpenLid();
                break;
            case 2:
                OpenLid();
                break;
        }


    }

    void CheckDirection()
    {
        // Tarkastaa pelaajan sijainnin suhteessa bossiin
        if(transform.position.x < mario.transform.position.x)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

    }

    void OpenLid()
    {
        CheckDirection();
        transform.localScale = new Vector3(direction * -1, 1, 1);
        animator.SetTrigger("LidAction"); // lis‰t‰‰n luukunavausanimaation loppuun AnimationEvent, jolla k‰ynnistetaan MakeAction()
    }

    void CloseLid()
    {
        animator.SetTrigger("LidAction");
        StartCoroutine(JustWait(3));

    }


    void MakeAction()
    {
        if(currentAction == 1)
        {
            ShootMissiles();
        }
        if(currentAction == 2)
        {
            ShootRollingBullet();
        }

    }

    void ShootMissiles()
    {
        StartCoroutine(ShootDelayMissile());
    }

    IEnumerator ShootDelayMissile()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(missile, spawnMissile.transform.position, Quaternion.identity);
            yield return new WaitForSecondsRealtime(0.5f);
        }
        CloseLid();
    }

    void ShootRollingBullet()
    {
        GameObject rollBulletInstance = Instantiate(rollingBullet, spawnRollingBullet.transform.position, Quaternion.identity);
        rollBulletInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(8 * direction, 5), ForceMode2D.Impulse);
        Destroy(rollBulletInstance, 6);
        CloseLid();

    }

    public void Rush()
    {
        CheckDirection();
        animator.SetTrigger("RushAction");
        idling = false;
        moveSpeed = 10;
    }

    public void EndRush()
    {
        CheckDirection();
        rb2D.AddForce(new Vector2(5 * direction, 10), ForceMode2D.Impulse);
        moveSpeed = 0;
        // sen aikaa kun bossi on ilmassa, j‰‰d‰‰n odottamaan.
        StartCoroutine(JustWait(5));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BossWall"))
        {
            EndRush();
        }
    }

    // Coroutinet tulee t‰nne

    IEnumerator JustWait(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        
        GoToIdle();
    }
}
