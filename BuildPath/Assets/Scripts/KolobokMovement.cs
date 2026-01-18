using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KolobokMovement : MonoBehaviour
{
    private float movementSpeed = 20f;
    private float rotationSpeed = 5f;
    private Vector3 target;
    private bool isMove = false;
    private List<Vector3> points = new List<Vector3>();
    private int curIndex = 0;
    private Animator anim;
    private Rigidbody rb;
    private float stoppingDistance = 0.2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            // ѕровер€ем, достигли ли мы текущей точки
            Vector2 pos = new Vector2(transform.position.x, transform.position.z);
            Vector2 tg = new Vector2(target.x, target.z);
            if (Vector3.Distance(pos, tg) < stoppingDistance)
            {
                NextPoint();
            }
            else
            {
                // ѕоворачиваем в сторону следующей точки
                //if (isAttack == false) 
                LookAtWaypoint();

                // ѕеремещаем врага к текущей точке
                MoveTowardsWaypoint();
            }
        }
    }

    private void LookAtWaypoint()
    {
        // ѕоворачиваем врага в сторону следующей точки
        Vector3 dir = target - transform.position; dir.y = 0f;
        Quaternion lookRot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, rotationSpeed * Time.deltaTime);
    }

    private void MoveTowardsWaypoint()
    {
        // ѕеремещаем врага к текущей точке
        Vector3 dir = target - transform.position; dir.y = 0f;
        rb.MovePosition(transform.position + dir.normalized * movementSpeed * Time.deltaTime);
    }

    private void NextPoint()
    {
        if (curIndex < points.Count)
        {
            target = points[curIndex];
            curIndex++;
            //if (isPatrouille) curIndex %= points.Count;
        }
        else
        {
            isMove = false;
            anim.SetBool("IsWalk", false);
        }
    }
    public void SetPath(List<Vector3> path, bool isPatrouille = false)
    {
        points.Clear();
        points.AddRange(path);
        curIndex = 0;
        target = points[curIndex];
        //this.isPatrouille = isPatrouille;
        isMove = true;
        anim.SetBool("IsWalk", true);
    }

    public void StopMove()
    {
        isMove = false;
        anim.SetBool("IsWalk", false);
        transform.rotation = Quaternion.Euler(new Vector3(90f, 180f, 0));
    }

}
