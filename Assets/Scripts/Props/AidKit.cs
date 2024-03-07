using UnityEngine;

public class AidKit : MonoBehaviour
{
    [SerializeField] private int _value = 25;

    public int Collect()
    {
        Destroy(gameObject);
        return _value;
    }
}