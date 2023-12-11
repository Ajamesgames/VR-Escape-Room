using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    [SerializeField]
    private float _angelSpeed = 0f;
    [SerializeField]
    private float _rotateSpeed = 100f;
    [SerializeField]
    private GameObject _keyCabinet;
    [SerializeField]
    private GameObject _deathPosObject;
    [SerializeField]
    private GameObject _rayInteractor;
    [SerializeField]
    private GameObject _lights;
    [SerializeField]
    private GameObject _locationLight;
    [SerializeField]
    private GameObject _key;
    [SerializeField]
    private GameObject _saw;
    [SerializeField]
    private GameObject _keyDoor;
    [SerializeField]
    private GameObject _scaryMusicObject;
    [SerializeField]
    private GameObject _backgroundMusicObject;
    [SerializeField]
    private GameObject _endLocation;
    private GameObject _player;

    private AudioSource _scaryMusic;
    private AudioSource _backgroundMusic;
    private AudioSource _victorySound;
    private AudioSource _doorCreekSound;
    private AudioSource _stoneMovementSound;

    private bool _canMove = true;
    private bool _isFrozen = false;
    private Vector3 _currentPos;
    private Vector3 _deathPos;

    // Start is called before the first frame update
    void Start()
    {
        _deathPos = _deathPosObject.transform.position;
        _player = GameObject.FindWithTag("Player");
        if(_player == null)
        {
            Debug.LogError("Can't locate Player Tag");
        }
        _scaryMusic = _scaryMusicObject.GetComponent<AudioSource>();
        if(_scaryMusic == null)
        {
            Debug.Log("Scary music is null");
        }
        _backgroundMusic = _backgroundMusicObject.GetComponent<AudioSource>();
        if (_backgroundMusic == null)
        {
            Debug.Log("Background music is null");
        }
        _doorCreekSound = _keyDoor.GetComponent<AudioSource>();
        if (_doorCreekSound == null)
        {
            Debug.Log("Door creek sound is null");
        }
        _victorySound = _endLocation.GetComponent<AudioSource>();
        if (_victorySound == null)
        {
            Debug.Log("Victory sound is null");
        }
        _stoneMovementSound = GetComponent<AudioSource>();
        if (_stoneMovementSound == null)
        {
            Debug.Log("Stone movement sound is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
        FreezePlayer();
    }

    private void EnemyMovement()
    {
        //if canmove true, move and rotate towards player
        if (_canMove == true)
        {
            Quaternion _lookRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, _rotateSpeed);

            transform.Translate(Vector3.forward * _angelSpeed * Time.deltaTime);
        }
    }
    
    private void FreezePlayer()
    {
        if (_isFrozen == true)
        {
            _player.transform.position = _deathPos;
            transform.position = _currentPos;
        }
    }

    public void PlayerLooking()
    {
        _canMove = false;
        _stoneMovementSound.Stop();
    }

    public void PlayerNotLooking()
    {
        _canMove = true;
        _stoneMovementSound.Play();
    }

    public void IncreaseSpeed()
    {
        _angelSpeed = 5.0f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other != null)
            {
                other.GetComponent<AudioSource>().Play();
            }
            _currentPos = transform.position;
            _player.transform.position = _deathPos;
            _player.transform.rotation = _deathPosObject.transform.rotation;
            _isFrozen = true;
            _rayInteractor.SetActive(true);
            _lights.SetActive(false);
        }

        if (other.CompareTag("Location"))
        {
            if (other != null)
            {
                _keyCabinet.GetComponent<Rigidbody>().isKinematic = false; //opens key door
            }
            _lights.SetActive(true);
            _locationLight.SetActive(false);
            _key.SetActive(true);
            _saw.SetActive(true);
            _doorCreekSound.Play();
            _victorySound.Play();
            _scaryMusic.Stop();
            _backgroundMusic.Play();
            Destroy(this.gameObject);
        }
    }
}
