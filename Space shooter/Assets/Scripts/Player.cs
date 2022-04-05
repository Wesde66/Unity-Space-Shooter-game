using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _Laser;
    [SerializeField] GameObject _TrippleShot;
    [SerializeField] float _fireRate;
    [SerializeField] float speed = 3.5f;
    float _canFire = -1f;
    [SerializeField] private int _Lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private bool _isTrippleShotActive = false;


    // Start is called before the first frame update
    void Start()
    {
       
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovment();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            Shoot();
        }
    }

    void CalculateMovment()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, 3.5f));

       

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, 0);
        }
        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    void Shoot()
    {
        _canFire = Time.time + _fireRate;



        if (_isTrippleShotActive == true)
        {
            Instantiate(_TrippleShot, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_Laser, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

        }



    }

    public void Damage()
    {
        _Lives--;

        if(_Lives < 1)
        {
            _spawnManager.OnPlayerDeath();

            Destroy(this.gameObject);
        }
    }
}
