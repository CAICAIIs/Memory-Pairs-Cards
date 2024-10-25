using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Card_GameManager : MonoBehaviour
{

	private const int winCardCouples = 9;
	private int curCardCouples = 0;
	private bool canPlayerClick = true;

	public Sprite BackSprite;
	public Sprite SuccessSprite;
	public Sprite[] FrontSprites;

	public GameObject CardPre;
	public Transform CardsView;
	private List<GameObject> CardObjs;
	private List<Card> FaceCards;

	public int count = 0;
	//public int limitCount = 30;//限制步数
	public Text stepCount;

	//public GameObject winPanel;
	//public GameObject losePanel;

	void Start()
	{

		CardObjs = new List<GameObject>();
		FaceCards = new List<Card>();

		//将12张卡牌制作完成后添加到CardObjs数组
		for (int i = 0; i < 9; i++)
		{
			Sprite FrontSprite = FrontSprites[i];
			for (int j = 0; j < 2; j++)
			{
				//实例化对象
				GameObject go = (GameObject)Instantiate(CardPre);
				//获取Card组件进行初始化，点击事件由游戏管理器统一处理
				//所以卡牌的点击事件的监听在管理器指定
				Card card = go.GetComponent<Card>();
				card.InitCard(i, FrontSprite, BackSprite, SuccessSprite);
				card.cardBtn.onClick.AddListener(() => CardOnClick(card));

				CardObjs.Add(go);
			}
		}

		while (CardObjs.Count > 0)
		{
			//取随机数，左闭右开区间
			int ran = Random.Range(0, CardObjs.Count);
			GameObject go = CardObjs[ran];
			//将对象指定给Panel作为子物体，这样就会被我们的组件自动布局
			//go.transform.parent = CardsView;
			go.transform.SetParent(CardsView);
			//local就表示相对于父物体的相对坐标系，此处做校正处理
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			//从CardObjs列表中移除该索引指向对象，列表对象数量减少1个
			CardObjs.RemoveAt(ran);
		}
	}

	private void Update()
	{
		
	}


	private void CardOnClick(Card card)
	{
		if (canPlayerClick)
		{
			//先判断是否可以点击，可点击则直接翻牌
			card.SetFanPai();
			//添加到比对数组中
			FaceCards.Add(card);
			//步数+1
			count++;
			stepCount.text = "step：" + count;//文本内容的显示
										   //如果有两张牌了，则不可再点击，进入协同程序
			if (FaceCards.Count == 2)
			{
				canPlayerClick = false;
				StartCoroutine(JugdeTwoCards());
			}
		}
	}

	IEnumerator JugdeTwoCards()
	{
		//获取到两张卡牌对象
		Card card1 = FaceCards[0];
		Card card2 = FaceCards[1];
		//对ID进行比对
		if (card1.ID == card2.ID)
		{
			yield return new WaitForSeconds(0.8f);
			card1.SetSuccess();
			card2.SetSuccess();
			curCardCouples++;
			
		}
		else
		{
			//配对失败，停1.5f,然后两张都翻过去
			yield return new WaitForSeconds(1.5f);
			card1.SetRecover();
			card2.SetRecover();
		}

		FaceCards = new List<Card>();
		canPlayerClick = true;
	}

	
}
