using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public PatrolType myPatrol;
    public float speed = 1f;
    float moveDistance = 1000;

    public float Health;

    [Header("AI")]
    public EnemyType myType;
    public Transform moveToPos; //Needed for all patrols.
    public EnemyManager _EM;
    Transform startPos;         //Needed for loop patrol movement.
    Transform endPos;           //Needed for loop patrol movement.
    bool reverse;               //Needed for loop patrol movement.
    int patrolPoint = 0;        //Needed for linear patrol movement.

    void Start()
    {
        _EM = FindObjectOfType<EnemyManager>();
        switch (myType)
        {
            case EnemyType.OneHand:
                Health = 80f;
                speed = 3f;
                myPatrol = PatrolType.Linear;
                break;
            case EnemyType.TwoHand:
                Health = 100f;
                speed = 5f;
                myPatrol = PatrolType.Random;
                break;
            case EnemyType.Archer:
                Health = 60f;
                speed = 1f;
                myPatrol= PatrolType.Loop;
                break;
        }
        SetupAI();
    }

    void SetupAI()
    {
        startPos = Instantiate(new GameObject(), transform.position, transform.rotation).transform;
        endPos = _EM.GetRandomSpawnPoint();
        moveToPos = endPos;
        StartCoroutine(Move());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            StopAllCoroutines();
    }

    IEnumerator Move()
    {
        switch(myPatrol)
        {
            case PatrolType.Linear:
                moveToPos = _EM.spawnPoint[patrolPoint];
                patrolPoint = patrolPoint != _EM.spawnPoint.Length ? patrolPoint + 1 : 0;
                break;
            case PatrolType.Random:
                moveToPos = _EM.GetRandomSpawnPoint();
                break;
            case PatrolType.Loop:
                moveToPos = reverse ? startPos : endPos;
                reverse = !reverse;
                break;
        }


        transform.LookAt(moveToPos);
        while(Vector3.Distance(transform.position, moveToPos.position) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveToPos.position, Time.deltaTime * speed);
            yield return null;
        }

        yield return new WaitForSeconds(1);
        StartCoroutine(Move());
    }

    /*IEnumerator Move()
    {
        for(int i = 0; i < moveDistance; i++)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            yield return null;
        }

        transform.Rotate(Vector3.up * 180);
        yield return new WaitForSeconds(Random.Range(1, 3));
        StartCoroutine(Move());
    }
    */
}
