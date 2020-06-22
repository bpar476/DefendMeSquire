using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointsHeart : MonoBehaviour
{
    public Sprite HealthyHeart;
    public Sprite BrokenHeart;
    private Image heart;
    // Start is called before the first frame update
    void Start()
    {
        heart = GetComponent<Image>();
        heart.sprite = HealthyHeart;
    }
}
