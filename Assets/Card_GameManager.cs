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
	//public int limitCount = 30;//���Ʋ���
	public Text stepCount;

	//public GameObject winPanel;
	//public GameObject losePanel;

	void Start()
	{

		CardObjs = new List<GameObject>();
		FaceCards = new List<Card>();

		//��12�ſ���������ɺ���ӵ�CardObjs����
		for (int i = 0; i < 9; i++)
		{
			Sprite FrontSprite = FrontSprites[i];
			for (int j = 0; j < 2; j++)
			{
				//ʵ��������
				GameObject go = (GameObject)Instantiate(CardPre);
				//��ȡCard������г�ʼ��������¼�����Ϸ������ͳһ����
				//���Կ��Ƶĵ���¼��ļ����ڹ�����ָ��
				Card card = go.GetComponent<Card>();
				card.InitCard(i, FrontSprite, BackSprite, SuccessSprite);
				card.cardBtn.onClick.AddListener(() => CardOnClick(card));

				CardObjs.Add(go);
			}
		}

		while (CardObjs.Count > 0)
		{
			//ȡ�����������ҿ�����
			int ran = Random.Range(0, CardObjs.Count);
			GameObject go = CardObjs[ran];
			//������ָ����Panel��Ϊ�����壬�����ͻᱻ���ǵ�����Զ�����
			//go.transform.parent = CardsView;
			go.transform.SetParent(CardsView);
			//local�ͱ�ʾ����ڸ�������������ϵ���˴���У������
			go.transform.localPosition = Vector3.zero;
			go.transform.localScale = Vector3.one;
			//��CardObjs�б����Ƴ�������ָ������б������������1��
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
			//���ж��Ƿ���Ե�����ɵ����ֱ�ӷ���
			card.SetFanPai();
			//��ӵ��ȶ�������
			FaceCards.Add(card);
			//����+1
			count++;
			stepCount.text = "step��" + count;//�ı����ݵ���ʾ
										   //������������ˣ��򲻿��ٵ��������Эͬ����
			if (FaceCards.Count == 2)
			{
				canPlayerClick = false;
				StartCoroutine(JugdeTwoCards());
			}
		}
	}

	IEnumerator JugdeTwoCards()
	{
		//��ȡ�����ſ��ƶ���
		Card card1 = FaceCards[0];
		Card card2 = FaceCards[1];
		//��ID���бȶ�
		if (card1.ID == card2.ID)
		{
			yield return new WaitForSeconds(0.8f);
			card1.SetSuccess();
			card2.SetSuccess();
			curCardCouples++;
			
		}
		else
		{
			//���ʧ�ܣ�ͣ1.5f,Ȼ�����Ŷ�����ȥ
			yield return new WaitForSeconds(1.5f);
			card1.SetRecover();
			card2.SetRecover();
		}

		FaceCards = new List<Card>();
		canPlayerClick = true;
	}

	
}
