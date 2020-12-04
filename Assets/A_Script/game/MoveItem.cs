using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class MoveItem : MonoBehaviour
{

	public Sprite[] sp_item;
	private SpriteRenderer sprenderer;
	private GameObject GO_gameMgr;
	private GameObject GO_goalobj;
	private int move_set = 0;
	private int item_kind;
	private Sequence mySequence;

	private void Awake()
	{
		GO_gameMgr = GameObject.Find("GO_W_MgrGame");
	}
	private void SetSprite(Sprite spr)
	{
		sprenderer.sprite = spr;
	}

	// Start is called before the first frame update
	void Start()
    {
		mySequence = DOTween.Sequence();
		mySequence.Append
	   (
			this.gameObject.transform.DOLocalMove(new Vector3(goal_xf, goal_yf, 0.0f), 1.0f)
		);
		mySequence.Play();

	}

	// Update is called once per frame
	void Update()
    {
		if (move_set == 1)
		{
			if (GO_goalobj == null)
			{
				kieru();
			}
		}
    }


	private float goal_xf;
	private float goal_yf;
	public void set_start_goal(int itemnum,GameObject goal_obj){

		Vector3 goal_pos = goal_obj.transform.position;
		goal_xf = goal_pos.x;
		goal_yf = goal_pos.y;
		GO_goalobj = goal_obj;
		move_set = 1;
		item_kind = itemnum;

		sprenderer = this.gameObject.GetComponent<SpriteRenderer>();
		SetSprite(sp_item[itemnum]);

	}

	public int get_itemkind(){
		int ret = item_kind;
		return ret;
	}
	public void kieru(){
		mySequence.Kill();
		Destroy(this.gameObject);
	}

}
