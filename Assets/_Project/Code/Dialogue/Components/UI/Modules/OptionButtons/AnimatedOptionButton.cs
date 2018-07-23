using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

namespace Sycamore.Dialogue.UI
{
	public class AnimatedOptionButton : OptionButton
	{
		[SerializeField]
		private string showAnimationName = "Show";
		[SerializeField]
		private string hideAnimationName = "Hide";
		[SerializeField]
		private string selectAnimationName = "Select";

		[Header ("References")]
		[SerializeField]
		private Button button;
		[SerializeField]
		private TextMeshProUGUI text;
		[SerializeField]
		private new Animation animation;
		[SerializeField]
		private CanvasGroup canvasGroup;


		public override void Show (float delay)
		{
			StartCoroutine (ShowRoutine (delay));
		}
		public override void Hide (float delay)
		{
			StartCoroutine (HideRoutine (delay));
		}
		public override void Select ()
		{
			animation.Play (selectAnimationName);
		}

		public override void SetText (string text)
		{
			this.text.text = text;
		}
		public override void SetInteractable (bool interactable)
		{
			canvasGroup.interactable = interactable;
		}
		public override void SetTransparency (float transparency)
		{
			canvasGroup.alpha = transparency;
		}

		public override void AddListener (Action onClick)
		{
			button.onClick.AddListener (onClick.Invoke);
		}
		public override void RemoveAllListeners ()
		{
			button.onClick.RemoveAllListeners ();
		}

		private IEnumerator ShowRoutine (float delay)
		{
			yield return new WaitForSeconds (delay);
			animation.Play (showAnimationName);
		}
		private IEnumerator HideRoutine (float delay)
		{
			yield return new WaitForSeconds (delay);
			animation.Play (hideAnimationName);
		}
	}
}