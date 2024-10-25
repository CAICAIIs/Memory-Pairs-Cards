using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Card : MonoBehaviour
{

	public int ID
	{
		get
		{
			return id;
		}
	}
	private int id;

	private Sprite frontImg;//未翻开前看到的图片
	private Sprite backImg;//翻看后看到的图片
	private Sprite successImg;//显示表示已经为正确翻开过的图片

	private Image showImg;//挂载的图片组件
	public Button cardBtn;//挂载的按钮组件

	public void InitCard(int Id, Sprite FrontImg, Sprite BackImg, Sprite SuccessImg)
	{
		this.id = Id;
		this.frontImg = FrontImg;
		this.backImg = BackImg;
		this.successImg = SuccessImg;

		showImg = GetComponent<Image>();
		showImg.sprite = this.backImg;

		cardBtn = GetComponent<Button>();
	}

	public void SetFanPai()
	{
		showImg.sprite = frontImg;
		cardBtn.interactable = false;
	}

	public void SetSuccess()
	{
		showImg.sprite = successImg;
	}

	public void SetRecover()
	{
		showImg.sprite = backImg;
		cardBtn.interactable = true;
	}
}
