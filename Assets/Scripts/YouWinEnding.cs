using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWinEnding : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _rayInteractor;
    private Vector3 _currentPos;
    private bool _isFrozen = false;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isFrozen == true)
        {
            _player.transform.position = _currentPos;
        }    
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _rayInteractor.SetActive(true);
            _currentPos = _player.transform.position;
            _isFrozen = true;
        }
    }


}
