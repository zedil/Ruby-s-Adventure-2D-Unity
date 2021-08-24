using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    public float speed;
    public bool vertical;
    Rigidbody2D rigidbody2D;
    public float changeTime = 3.0f;
    float timer;
    bool broken = true;
    int direction = 1;
    public ParticleSystem smokeEffect;
    public ParticleSystem hiteffect;
    public AudioClip hitclip;
    AudioSource audioSource;
    public GameObject other;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -=Time.deltaTime;

        if(timer<0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if(!broken)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;
        
        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        
        rigidbody2D.MovePosition(position);

          if(!broken)
        {
            return;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        //EnemyController enemyC = other.gameObject.GetComponent<EnemyController>();
        if(player != null)
        {
            player.ChangeHealth(-1);
            hiteffect.Play();
        }
    }

    public void Fix()
    {
        RubyController rb = gameObject.GetComponent<RubyController>();
        
        broken = false;
        rigidbody2D.simulated = false;
        animator.SetTrigger("Fixed");
        other.GetComponent<RubyController>().PlaySound(hitclip);
        smokeEffect.Stop();
    }
}
