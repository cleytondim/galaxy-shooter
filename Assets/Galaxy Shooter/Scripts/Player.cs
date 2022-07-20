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
    private GameObject _explosionPrefab;

    [SerializeField]
    private GameObject _shieldGameObject;

    [SerializeField]
    private GameObject[] _engines;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _fireRate = 0.25F;

    [SerializeField]
    private int lifes = 3;

    private int hitCount = 0;

    [SerializeField]
    private UIManager _uiManager;

    [SerializeField]
    private GameManager _gameManager;

    private AudioSource _audioSource;

    [SerializeField]
    private bool isShieldActive = false;

    public bool canTripleShot = false;


    private float _nextFire = 0.0f;

    private void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        hitCount = 0;

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager != null)
        {
            _uiManager.UpdateLives(lifes);
        }

        _audioSource = GetComponent<AudioSource>();
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
        _audioSource.Play();
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

    void Explode()
    {

        Destroy(this.gameObject);
        Instantiate(_explosionPrefab, this.transform.position, Quaternion.identity);
    }

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void ShieldPowerupOn()
    {
        isShieldActive = true;
        //this.transform.Find("Shields").gameObject.SetActive(true);
        _shieldGameObject.SetActive(true);
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
        if (isShieldActive)
        {
            isShieldActive = false;

            //Debug.Log(this.transform.GetChild(0).name);
            //Debug.Log(this.transform.Find("Shields").name);
            //this.transform.Find("Shields").gameObject.SetActive(false);
            _shieldGameObject.SetActive(false);
            return;
        }
        lifes--;
        hitCount++;
        if(hitCount <= 2)
        {
            bool affected = false;
            int engineToFail = 0;
            while (!affected)
            {
                engineToFail = Random.Range(0, 2);
                if (!_engines[engineToFail].activeInHierarchy)
                {
                    _engines[engineToFail].SetActive(true);
                    affected = true;
                }
            }
        }

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lifes);
        }


        if (lifes <= 0)
        {
            Explode();
            _gameManager.GameOver();
        }
    }
}
