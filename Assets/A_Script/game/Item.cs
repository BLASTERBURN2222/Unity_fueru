using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Item : MonoBehaviour, IPointerClickHandler
{

	private GameObject GO_gameMgr;
	private GameObject pf_getscore;

	private int dead_flg;
	private int item_kind = 0;
	public Sprite[] sp_item;
	private SpriteRenderer sprenderer;
	private int lifetime;
	private GameObject GO_ItemWindow;

	private void Awake()
	{
		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		GO_gameMgr = GameObject.Find("GO_W_MgrGame");
		GO_ItemWindow = GameObject.Find("ItemWindow");
		pf_getscore = (GameObject)Resources.Load("getpoint");
		dead_flg = 0;
		lifetime = 300;
	}


	// Start is called before the first frame update
	void Start()
    {
		if (item_kind != 0)
		{
			if (sp_item.Length > item_kind)
			{
				SetSprite(sp_item[item_kind]);
			}
		}
	}

	public void item_kind_set(int kd)
	{
		item_kind = kd;
	}

	private void SetSprite(Sprite spr)
	{
		sprenderer.sprite = spr;
	}




	// Update is called once per frame
	void Update()
    {
		if (dead_flg == 1) return;
		lifetime--;
		if(lifetime <= 0){
			dead_flg = 1;
			Destroy(this.gameObject);
		}
    }

	public void OnPointerClick(PointerEventData eventData)
	{
		if (GG.pause == 1) return;

		if (dead_flg == 1) return;
		gettakara();
	}

	void gettakara()
	{
		dead_flg = 1;
		GO_ItemWindow.gameObject.SendMessage("window_item_set", item_kind);

		Destroy(this.gameObject);

	}




}
