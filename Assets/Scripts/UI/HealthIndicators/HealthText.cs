using TMPro;

public class HealthText : HealthIndicator
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    
    protected override void SetHealth()
    {
        _text.text = $"Health: {_health.CurrentHealth} / {_health.MaxHealth}";
    }
}