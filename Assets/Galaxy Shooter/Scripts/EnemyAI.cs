using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private GameObject _enemyExplosionPrefab;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _clip;


    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                Explode();
                player.enemyDamage();
            }
        }
        else if(other.tag == "PlayerLaser")
        {
            Destroy(other.gameObject);
            Explode();
        }
    }

    void Explode()
    {
        if (_uiManager != null)
        {
            _uiManager.UpdateScore();
        }
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
        Destroy(this.gameObject);
        Instantiate(_enemyExplosionPrefab, this.transform.position, Quaternion.identity);
    }

    void Movement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -6)
        {
            float randomX = Random.Range(-8, 9);
            transform.position = new Vector3(randomX, 6, transform.position.z);
        }
    }
}
