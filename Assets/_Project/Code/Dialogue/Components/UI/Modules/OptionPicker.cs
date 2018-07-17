using NodeCanvas.DialogueTrees;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sycamore.Dialogue.UI
{
	public class OptionPicker : MonoBehaviour
	{
		[SerializeField] private float staggerDuration = 0.1f;
		[SerializeField] private OptionButton buttonTemplate;

		private List<OptionButton> buttonPool;

		private void Awake ()
		{
			if (buttonTemplate == null)
				Debug.Log ("OptionPicker is missing a button template, so it can't create buttons.");
			else
				buttonTemplate.gameObject.SetActive (false);
		}

		public void CreateOptions (Dictionary<IStatement, int> options, Action<int> onOptionPicked)
		{
			if (buttonPool == null)
				buttonPool = new List<OptionButton> ();

			while (buttonPool.Count < options.Count)
				buttonPool.Add (null);

			int i = 0;
			foreach (var option in options)
			{
				if (buttonPool[i] == null)
				{
					buttonPool[i] = Instantiate (buttonTemplate, buttonTemplate.transform.parent);
					buttonPool[i].gameObject.SetActive (true);
					buttonPool[i].SetAlpha (0f);
				}

				if (i < options.Count)
				{
					buttonPool[i].Show (i * staggerDuration);
					buttonPool[i].Text.text = option.Key.text;
					buttonPool[i].Button.onClick.RemoveAllListeners ();
					buttonPool[i].Button.onClick.AddListener (() => onOptionPicked.Invoke (option.Value));
				}
				else
					buttonPool[i].Hide (i * staggerDuration);

				i++;
			}
		}

		public void HideOptions ()
		{
			for (int i = 0; i < buttonPool.Count; i++)
				if (buttonPool[i] != null)
					buttonPool[i].Hide (i * staggerDuration);
		}
	}
}