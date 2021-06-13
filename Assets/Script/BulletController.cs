using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    public int attack;
    private void Update()
    {
        if (GameManager.Ins.gameIsPause) return;
        if (!GameManager.Ins.gameIsPlaying)
        {
            gameObject.SetActive(false);
            return;
        }
        if(transform.position.y > GameObject.Find("DestroyUpPoint").transform.position.y || transform.position.y < GameObject.Find("DestroyDownPoint").transform.position.y)
        {
            if (tag.Equals("PlayerBullet")) GameManager.Ins.playerBulletPool.Add(gameObject);
            if (tag.Equals("Enemy0Bullet")) GameManager.Ins.enemy0BulletPool.Add(gameObject);
            if (tag.Equals("Enemy1Bullet")) GameManager.Ins.enemy1BulletPool.Add(gameObject);
            if (tag.Equals("Enemy2Bullet")) GameManager.Ins.enemy2BulletPool.Add(gameObject);
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Ins.gameIsPause) return;
        if (!GameManager.Ins.gameIsPlaying) return;
        if (CompareTag("PlayerBullet"))
        {
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        }else if (CompareTag("TrackBullet"))
        {
            Track();
        }
        else if (CompareTag("Enemy0Bullet") || CompareTag("Enemy1Bullet") || CompareTag("Enemy2Bullet"))
        {
            transform.Translate(Vector3.down * speed * Time.fixedDeltaTime);
        }
    }
    private void Track()
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if(enemy != null)
        {
            transform.Translate((enemy.transform.position - transform.position).normalized * speed * Time.fixedDeltaTime);
        }
        else
        {
            transform.Translate(Vector3.up * speed * Time.fixedDeltaTime);
        }
    }
}
