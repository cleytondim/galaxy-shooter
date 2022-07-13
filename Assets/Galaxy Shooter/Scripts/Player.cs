using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _fireRate = 0.25F;

    [SerializeField]
    private int lifes = 3;

    public bool canTripleShot = false;


    private float _nextFire = 0.0f;
    private void Start()
    {

        transform.position = new Vector3(0, 0, 0);
    }

    private void Update()
    {
        Movement();

        //if (Input.GetButton("Fire1"))
        if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time > _nextFire)
        {
            Shot();
        }

    }
    
    private void Shot()
    {
        if (canTripleShot)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            //canTripleShot = false;
        }
        else
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        _nextFire = Time.time + _fireRate;
    }

    
    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(((Vector3.right * horizontalInput) + (Vector3.up * verticalInput)) * _speed * Time.deltaTime);
        //transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);
        //transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedPowerupOn()
    {
        _speed = _speed * 2;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        canTripleShot = false;
    }

    public IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5);
        _speed = _speed / 2;
    }

    public void enemyDamage()
    {
        lifes--;
        if (lifes <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
