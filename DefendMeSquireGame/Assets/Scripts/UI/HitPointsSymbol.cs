using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointsSymbol : MonoBehaviour
{
    public Sprite HealthySprite;
    public Sprite DamagedSprite;
    private Image heart;
    // Start is called before the first frame update
    private void Awake()
    {
        heart = GetComponent<Image>();
        heart.sprite = HealthySprite;
    }

    public void Damage()
    {
        heart.sprite = DamagedSprite;
    }

    public void Heal()
    {
        heart.sprite = HealthySprite;
    }
}
