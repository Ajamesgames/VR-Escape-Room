using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPos;
    [SerializeField]
    private GameObject _spawnPos;
    [SerializeField]
    private GameObject _rayInteractor;
    [SerializeField]
    private GameObject _angel;
    private bool _canMove = false;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_canMove == false)
        {
            transform.position = _spawnPos.transform.position; //lock position
        }
    }

    public void StartGameButton()
    {
        _canMove = true;
        _rayInteractor.SetActive(false);
        transform.position = _startPos.transform.position;
        transform.rotation = _startPos.transform.rotation;
        //_angel.SetActive(true);
    }


}
