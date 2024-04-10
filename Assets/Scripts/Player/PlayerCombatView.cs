using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
public class PlayerCombatView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffect;
    private PlayerCombat _playerCombat;

    private void Awake()
    {
        _playerCombat = GetComponent<PlayerCombat>();
    }

    private void OnEnable()
    {
        _playerCombat.Hit += ShowHitEffect;
    }

    private void OnDisable()
    {
        _playerCombat.Hit -= ShowHitEffect;
    }

    private void ShowHitEffect(Vector3 hitPosition)
    {
        _hitEffect.transform.position = hitPosition;
        _hitEffect.Play();
    }
}