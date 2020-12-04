using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWindow : MonoBehaviour
{

	public Sprite[] sp_item;
	private SpriteRenderer sprenderer;

	private void Awake()
	{
		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		SetSprite(sp_item[0]);
	}


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


	private void SetSprite(Sprite spr)
	{
		sprenderer.sprite = spr;
	}

	public void window_item_set(int kd)
	{
		GG.window_item = kd + 1;
		if (sp_item.Length > GG.window_item)
		{
			SetSprite(sp_item[GG.window_item]);
		}
	}

}
