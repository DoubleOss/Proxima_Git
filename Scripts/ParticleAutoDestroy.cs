using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {
	public enum EDestroyType
	{
		Destroy = 0,
		Inactive
	}

    public float m_lifeTime = 0.0f;
	public EDestroyType m_destroyType = EDestroyType.Inactive;
    float m_tempTime;
    bool m_isAlive = false;
    //EffectPoolUnit objectPoolUnit;
    ParticleSystem[] m_psystems;
    void Awake()
    {
        //objectPoolUnit = gameObject.GetComponent<EffectPoolUnit>();
        m_psystems = GetComponentsInChildren<ParticleSystem>();
    }

	void OnEnable()
	{
		m_isAlive = true;
		m_tempTime = Time.time;
	}

	void DestroyParticle()
	{
		switch(m_destroyType)
		{
		    case EDestroyType.Destroy:
			    Destroy(gameObject);
			    break;
		    case EDestroyType.Inactive:                
                Destroy(gameObject);
			    break;
		}
	}

    void Update()
    {
        if (m_isAlive)
        {
            if (m_lifeTime > 0.0f)
            {
                if (Time.time > m_tempTime + m_lifeTime)
                {
                    m_isAlive = false;
					DestroyParticle();
                }
            }
            else
            {                
                bool isPlaying = false;                
                for (int i = 0; i < m_psystems.Length; i++)
                {
                    if (m_psystems[i].isPlaying)
                    {
                        isPlaying = true;
                        break;
                    }
                }

                if (!isPlaying)
                {
                    m_isAlive = false;
					DestroyParticle();
                }
            }
        }
    }
}
