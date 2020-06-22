using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPointsUI : MonoBehaviour
{
	public int MaxHitPoints { get; set; }
	private int _hitPoints = 0;
	public int HitPoints
	{
		get { return _hitPoints; }
		internal set
		{
			_hitPoints = value;
            hptext.text = $"{_hitPoints}";
		}
	}

	public Text hptext;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
