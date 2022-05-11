using UnityEngine;

public class SpawnPrefabOnKeyDown : MonoBehaviour
{
    public GameObject m_Prefab;
    public KeyCode m_KeyCode;

    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Instantiate(m_Prefab, transform.position, transform.rotation);
            transform.position += new Vector3(-50, 0, 0);
        }
            
    }
}
