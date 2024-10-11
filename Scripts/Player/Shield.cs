using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    public float m_durability = 100f;

    [SerializeField]
    public PlayerControll m_PlayerControll;

    [SerializeField]
    AudioClip m_clip;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            if (other.name.Equals("GunBullet(Clone)"))
                return;
            m_durability -= 10;
            AudioSource.PlayClipAtPoint(m_clip, transform.position, 1f);

        }
        if (m_durability < 0)
        {
            m_durability = 0;
            gameObject.SetActive(false);
        }
    }

    public void decreaseShield(float damage)
    {
        m_durability -= damage;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
