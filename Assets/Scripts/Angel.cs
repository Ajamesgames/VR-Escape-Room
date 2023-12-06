using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel : MonoBehaviour
{
    [SerializeField]
    private float _angelSpeed = 5.0f;
    [SerializeField]
    private float _rotateSpeed = 100f;
    private bool _canMove = true;
    private GameObject _player;
    [SerializeField]
    private GameObject _keyCabinet;
    [SerializeField]
    private GameObject _deathPosObject;
    private Vector3 _deathPos;
    [SerializeField]
    private GameObject _rayInteractor;
    private bool _isFrozen = false;
    private Vector3 _currentPos;

    // Start is called before the first frame update
    void Start()
    {
        _deathPos = _deathPosObject.transform.position;
        _player = GameObject.FindWithTag("Player");
        if(_player == null)
        {
            Debug.LogError("Can't locate Player Tag");
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
    }

    public void PlayerNotLooking()
    {
        _canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _currentPos = transform.position;
            _player.transform.position = _deathPos;
            _player.transform.rotation = _deathPosObject.transform.rotation;
            _isFrozen = true;
            _rayInteractor.SetActive(true);
        }

        if (other.CompareTag("Location"))
        {
            _keyCabinet.GetComponent<Rigidbody>().isKinematic = false; //opens key door
            Destroy(this.gameObject);
        }
    }
}
