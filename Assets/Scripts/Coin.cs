using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _value = 1;
    
    public int Collect()
    {
        Destroy(gameObject);
        return _value;
    }
}
