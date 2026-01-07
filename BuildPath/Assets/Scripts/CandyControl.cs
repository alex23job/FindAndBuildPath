using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyControl : MonoBehaviour
{
    [SerializeField] private int _candyID = -1;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _speedRotation = 45f;

    private bool _isMove = false;
    private bool _isRotation = false;
    private Vector3 _target;
    private Vector3 _angles;
    public int CandyID {  get { return _candyID; } }

    // Start is called before the first frame update
    void Start()
    {
        _angles = new Vector3(180f, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (_isMove)
        {
            Vector3 delta = _target - transform.position;
            if (delta.magnitude > 0.2f)
            {
                delta.Normalize();
                transform.position += delta * _speed * Time.deltaTime;
                if (_isRotation)
                {
                    Vector3 rot = transform.rotation.eulerAngles;
                    rot.x += Time.deltaTime * _speedRotation * 0.7f;
                    rot.y += Time.deltaTime * _speedRotation * 0.5f;
                    rot.z += Time.deltaTime * _speedRotation;
                    transform.rotation = Quaternion.Euler(rot);
                }
            }
            else
            {
                transform.position = _target;
                transform.rotation = Quaternion.Euler(_angles);
                _isMove = false;
            }
        }
    }

    public void SetTarget(Vector3 tg, bool isRot = false)
    {
        _target = tg;
        _isMove = true;
        _isRotation = isRot;
    }

    public bool CmpCandyID(int id)
    {
        return _candyID == id;
    }

    public void RemoveCandy(Vector3 tg)
    {
        SetTarget(tg, true);
        Destroy(gameObject, 2f);
    }
}
