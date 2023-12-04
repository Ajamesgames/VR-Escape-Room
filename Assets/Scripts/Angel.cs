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

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindWithTag("Player");
        if(_player == null)
        {
            Debug.LogError("Can't locate Player Tag");
        }


    }

    // Update is called once per frame
    void Update()
    {
        //if canmove true, move and rotate towards player
        if (_canMove == true)
        {
            Quaternion _lookRotation = Quaternion.LookRotation(_player.transform.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, _rotateSpeed);

            transform.Translate(Vector3.forward * _angelSpeed * Time.deltaTime);
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
        if (other.CompareTag("Location"))
        {
            Destroy(this.gameObject);
            _keyCabinet.GetComponent<Rigidbody>().isKinematic = false; //opens key door
        }
    }
}
