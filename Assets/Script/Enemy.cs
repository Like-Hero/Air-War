using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator anim;
    public int hp;
    public float speed;
    public int score;
    public bool isDead;
    private const int DEAD_SPEED_RATE = 2;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isDead)
        {
            JudgeDead();
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            Destroy(other.gameObject);
            hp--;
        }else if (other.CompareTag("Player_1") || other.CompareTag("Player_2"))
        {
            hp = 0;
            other.gameObject.GetComponent<PlayerController>().nowHP = 0;
        }
    }
    private void JudgeDead()
    {
        if (transform.position.y < GameObject.Find("DestroyDownPoint").transform.position.y)
        {
            Dead();
        }
        else if (hp <= 0 && GameManager.Ins.gameIsPlaying)
        {
            isDead = true;
            PlayerData.Score += GetComponent<Enemy>().score;
            speed /= DEAD_SPEED_RATE;//死亡之后速度减慢
            anim.SetTrigger("Dead");
        }
        
    }
    private void Dead()
    {
        Destroy(gameObject);
    }
}
