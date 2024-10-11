using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    float m_speed = 1f;

    Vector3 m_dir = Vector3.zero;

    bool isCurve = false;

    Vector3 startPoint;
    Vector3 endPoint;
    Vector3 controlPoint1;
    Vector3 controlPoint2;


    Ray m_ray;

    string m_thrower = "";

    float m_damage = 1f;


    bool left = false;

    float length = 0;

    float t = 0f;

    bool m_isPlay = false;

    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0; // (1-t)^3 * p0
        point += 3 * uu * t * p1; // 3 * (1-t)^2 * t * p1
        point += 3 * u * tt * p2; // 3 * (1-t) * t^2 * p2
        point += ttt * p3; // t^3 * p3

        return point;
    }

    void aEventNavStartBullet()
    {
        m_isPlay = true;
    }
    public void SetBullet(Vector3 dir, float speed, float damage, string thrower = "NULL")
    {
        m_dir = dir;
        m_speed = speed;
        m_thrower = thrower;
        m_damage = damage;
    }

    public void SetCurveBullet(Ray ray, float speed, float damage, bool left, string thrower = "NULL")
    {
        m_ray = ray;
        m_damage = damage;
        m_speed = speed;
        isCurve = true;
        m_thrower = thrower;
        this.left = left;

        startPoint = transform.position;

        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = ray.GetPoint(50);
        }

        Vector3 random = Random.insideUnitSphere;

        endPoint = new Vector3(endPoint.x , endPoint.y ,endPoint.z );

        controlPoint1 = startPoint + Vector3.left + Vector3.right * (endPoint.x - startPoint.x) * 0.5f;
        controlPoint2 = endPoint + Vector3.left + Vector3.right * (endPoint.x - startPoint.x) * 0.5f;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (!m_isPlay)
            return;
        else
        {
            m_isPlay = false;

            foreach (Transform bullets in transform.GetComponentInChildren<Transform>())
            {
                if(bullets.transform.gameObject.CompareTag("DummyBullet"))
                {
                    var prefab = Resources.Load("Prefab/Bullet/Bullet") as GameObject;
                    var obj = Instantiate(prefab);
                    obj.transform.localScale = bullets.transform.lossyScale;
                    obj.transform.position = bullets.transform.position;

                    obj.GetComponent<BulletHit>().SetCurveBullet(m_ray, m_speed, m_damage,left ,m_thrower);
                }

            }

            Destroy(gameObject);
        }
        /*
        if(isCurve)
        {
            t += m_speed * Time.deltaTime;

            if(t>1f)
            {
                /*
                var effect1 = Instantiate(m_effect1);
                var effect2 = Instantiate(m_effect2);

                effect1.transform.position = transform.position;
                effect2.transform.position = transform.position;

                Quaternion look = Quaternion.LookRotation(endPoint);
                effect1.transform.rotation = look;
                effect2.transform.rotation = look;



        Destroy(gameObject);
            }

            Vector3 position = CalculateBezierPoint(t, startPoint, controlPoint1, controlPoint2, endPoint);
            transform.position = position;

                }
                else
        {
            transform.position += m_dir * m_speed * Time.deltaTime;
        }
        */


    }

}
