using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.AI;

public class Enemy : GameBehaviour
{
    public static event Action<GameObject> OnEnemyHit = null;
    public static event Action<GameObject> OnEnemyDie = null;

    public PatrolType myPatrol;
    float baseSpeed = 2f;
    public float mySpeed = 1f;
    float moveDistance = 1000;

    int baseHealth = 100;
    int maxHealth;
    public int myHealth;
    public int myScore;
    public int myDamage = 20;
    EnemyHealthBar healthBar;

    public string myName;

    [Header("AI")]
    public EnemyType myType;
    public Transform moveToPos; //Needed for all patrols
    Transform startPos;         //Needed for loop patrol movement
    Transform endPos;           //Needed for loop patrol movement
    bool reverse;               //Needed for loop patrol movement
    int patrolPoint = 0;        //Needed for linear patrol movement
    public float attackDistance = 2f;
    public float detectTime = 5f;
    public float detectDistance = 10f;
    int currentWayPoint;
    NavMeshAgent agent;

    Animator anim;
    AudioSource audioSource;


    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        healthBar = GetComponentInChildren<EnemyHealthBar>();
        SetName(_EM.GetEnemyName());

        switch (myType)
        {
            case EnemyType.OneHand:
                myHealth = maxHealth = baseHealth;
                mySpeed = baseSpeed;
                myPatrol = PatrolType.Patrol;
                myScore = 100;
                myDamage = 20;
                break;
            case EnemyType.TwoHand:
                myHealth = maxHealth = baseHealth * 2;
                mySpeed = baseSpeed / 2;
                myPatrol = PatrolType.Patrol;
                myScore = 200;
                myDamage = 40;
                break;
            case EnemyType.Archer:
                myHealth = maxHealth = baseHealth / 2;
                mySpeed = baseSpeed * 2;
                myPatrol = PatrolType.Patrol;
                myScore = 300;
                myDamage = 10;
                break;
        }

        SetupAI();

        if (GetComponentInChildren<EnemyWeapon>() != null)
            GetComponentInChildren<EnemyWeapon>().damage = myDamage;
    }

    void SetupAI()
    {
        currentWayPoint = UnityEngine.Random.Range(0, _EM.spawnPoints.Length);
        agent.SetDestination(_EM.spawnPoints[currentWayPoint].position);
        ChangeSpeed(mySpeed);
    }

    void ChangeSpeed(float _speed)
    {
        agent.speed = _speed;
    }

    private void Update()
    {
        if (myPatrol == PatrolType.Die)
            return;

        //Always get the distance between the player and me
        float disToPlayer = Vector3.Distance(transform.position, _PLAYER.transform.position);

        if (disToPlayer <= detectDistance && myPatrol != PatrolType.Attack)
        {
            if (myPatrol != PatrolType.Chase)
            {
                myPatrol = PatrolType.Detect;
            }
        }

        //Set the animators speed parameter to that of my speed
        anim.SetFloat("Speed", mySpeed);

        //Switching patrol states logic.
        switch (myPatrol)
        {
            case PatrolType.Patrol:
                //Get the distance between us and the current waypoint
                float disToWaypoint = Vector3.Distance(transform.position, _EM.spawnPoints[currentWayPoint].position);
                //If the distance is close enough, get a new waypoint
                if (disToWaypoint < 1)
                    SetupAI();
                //Reset the detect time
                detectTime = 5;
                break;

            case PatrolType.Detect:
                //Set the destination to ourself, essentially stopping us
                agent.SetDestination(transform.position);
                //Stop our speed
                ChangeSpeed(0);
                //Decrement our detect time
                detectTime -= Time.deltaTime;
                if (disToPlayer <= detectDistance)
                {
                    myPatrol = PatrolType.Chase;
                    detectTime = 5;
                }

                if (detectTime <= 0)
                {
                    myPatrol = PatrolType.Patrol;
                    SetupAI();
                }
                break;
            case PatrolType.Chase:
                //Set the destination to that of the player
                agent.SetDestination(_PLAYER.transform.position);
                //Increase the speed of which to chase the player
                ChangeSpeed(mySpeed * 2);
                //If the player gets outside the detect distance, go back to the detect state
                if (disToPlayer > detectDistance)
                    myPatrol = PatrolType.Detect;
                //If we are close to the player, then attack
                if (disToPlayer <= attackDistance)
                    StartCoroutine(Attack());
                break;
        }
    }

    public void SetName(string _name)
    {
        name = _name;
        healthBar.SetName(_name);
    }

    IEnumerator Attack()
    {
        myPatrol = PatrolType.Attack;
        ChangeSpeed(0);
        PlayAnimation("Attack");
        _AM.PlaySound(_AM.GetEnemyAttackSound(), audioSource);
        yield return new WaitForSeconds(1);
        ChangeSpeed(mySpeed);
        myPatrol = PatrolType.Chase;
    }

    private void Hit(int _damage)
    {
        myHealth -= _damage;
        healthBar.UpdateHealthBar(myHealth, maxHealth);
        //ScaleObject(this.gameObject, transform.localScale * 1.1f);

        if (myHealth <= 0)
        {
            Die();
        }
        else
        {
            PlayAnimation("Hit");
            OnEnemyHit?.Invoke(this.gameObject);
            _AM.PlaySound(_AM.GetEnemyHitSound(), audioSource);
        }
    }

    private void Die()
    {
        myPatrol = PatrolType.Die;
        ChangeSpeed(0);
        GetComponent<Collider>().enabled = false;
        PlayAnimation("Die");
        StopAllCoroutines();
        OnEnemyDie?.Invoke(this.gameObject);
        _AM.PlaySound(_AM.GetEnemyDieSound(), audioSource);

        //_GM.AddScore(myScore * 2);
        //_EM.KillEnemy(this.gameObject);
        //Destroy(this.gameObject);
    }

    void PlayAnimation(string _animationName)
    {
        int rnd = UnityEngine.Random.Range(1, 4);
        anim.SetTrigger(_animationName + rnd);
    }


    public void PlayFootstep()
    {
        _AM.PlaySound(_AM.FootstepsSounds(), audioSource, 0.1f);
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Projectile"))
        {
            Hit(collision.gameObject.GetComponent<Projectile>().damage);
            Destroy(collision.gameObject);
        }
    }

    /*IEnumerator Move()
    {
        for(int i = 0; i < moveDistance; i++)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
            yield return null;
        }
        transform.Rotate(Vector3.up * 180);
        yield return new WaitForSeconds(Random.Range(1, 3));
        StartCoroutine(Move());
    }*/

}
