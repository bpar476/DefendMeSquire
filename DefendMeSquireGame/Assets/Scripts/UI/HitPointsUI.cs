using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointsUI : MonoBehaviour
{
    public HitPointsSymbol hitPointsSymbol;
    private int _maxHitPoints;
    public int MaxHitPoints
    {
        get { return _maxHitPoints; }
        set
        {
            var anchoredPos = GetComponent<RectTransform>().anchoredPosition3D;
            for (var i = _maxHitPoints; i < value; i++)
            {
                HitPointsSymbol heart = Instantiate(hitPointsSymbol, Vector3.zero, Quaternion.identity, transform);
                var heartRectTransform = heart.GetComponent<RectTransform>();
                heartRectTransform.anchoredPosition3D = new Vector3(anchoredPos.x + i * heartWidth, anchoredPos.y, anchoredPos.z);
                heartRectTransform.localScale = Vector3.one;
                renderedHearts.Add(heart);
            }
            for (var i = value; i < _maxHitPoints; i++)
            {
                GameObject.Destroy(renderedHearts[i]);
            }
            _maxHitPoints = value;
        }
    }
    private int _hitPoints = 0;
    private float heartWidth = 50;
    private List<HitPointsSymbol> renderedHearts;
    public int HitPoints
    {
        get { return _hitPoints; }
        internal set
        {
            _hitPoints = value;
            for (var i = 0; i < MaxHitPoints; i++)
            {
                var heart = renderedHearts[i];
                if (i >= _hitPoints)
                {
                    heart.Damage();
                }
                else
                {
                    heart.Heal();
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        renderedHearts = new List<HitPointsSymbol>(MaxHitPoints * 2);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
