using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    // Делегат для уведомления о конце уровня
    public delegate void LevelFinishEventHandler();
    public event LevelFinishEventHandler OnLevelFinish;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            KolobokMovement km = other.gameObject.GetComponent<KolobokMovement>();
            if (km != null)
            {
                km.StopMove();
                Invoke("KolobokFinished", 1.5f);
            }
        }
    }

    private void KolobokFinished()
    {
        OnLevelFinish?.Invoke(); // Уведомляем подписчиков
    }
}
