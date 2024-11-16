using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI _tiempo, _mensaje;
    private float _temporizador,_temporizador2;
    public GameObject[] _enemigos;
    private bool _reset = true;
    public GameObject _player;
    private CharacterController _characterController;
    private Vector3 _posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = _player.GetComponent<CharacterController>();
        _posicionInicial = _player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_reset)
        {
            _characterController.enabled = false;
            StartCoroutine(Esperar());
        }
    }

    public IEnumerator Esperar()
    {
        _temporizador2 += Time.deltaTime;
        if(_temporizador2 >= 2)
        {
            _temporizador2 = 0;
            _reset = false;
            _temporizador = 0f;
            _mensaje.text = "";
            _player.transform.position = _posicionInicial;
            _characterController.enabled = true;
            _player.transform.rotation = Quaternion.Euler(0, 0, 0);
            foreach (GameObject enemigo in _enemigos)
            {
                enemigo.GetComponent<EnemyAI>().Run();
            }
            StartCoroutine(ContarTiempo());
            yield break;
        }
    }

    private IEnumerator ContarTiempo()
    {
        while (true)
        {
            _temporizador += Time.deltaTime;
            int minutes = Mathf.FloorToInt(_temporizador / 60F);
            int seconds = Mathf.FloorToInt(_temporizador % 60F);
            _tiempo.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            if (minutes >= 1)
            {
                foreach (GameObject enemigo in _enemigos)
                {
                    enemigo.GetComponent<EnemyAI>().Stop();
                }
                _mensaje.text = "¡Perdiste!";
                _reset = true;
                yield break;
            }
            
            foreach(GameObject enemigo in _enemigos)
            {
                float distance = enemigo.GetComponent<EnemyAI>().distance;
                if(distance < 1f && _temporizador > 2f)
                {
                    foreach (GameObject enemigo2 in _enemigos)
                    {
                        enemigo2.GetComponent<EnemyAI>().Stop();
                    }
                    _mensaje.text = "¡Perdiste!";
                    _reset = true;
                    yield break;
                }
            }

            if(_player.transform.position.x >= 42f && _player.transform.position.x <= 53f && _player.transform.position.z >= 68f)
            {
                foreach (GameObject enemigo in _enemigos)
                {
                    enemigo.GetComponent<EnemyAI>().Stop();
                }
                _mensaje.text = "¡Ganaste!";
                _reset = true;
                yield break;
            }
            yield return null;
        }
    }
}
