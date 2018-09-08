using UnityEngine;
using NodeCanvas.DialogueTrees;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Sycamore.Dialogue.UI
{
	public class OptionPicker : MonoBehaviour
	{
		[SerializeField] private float staggerInDuration = 0.1f;
		[SerializeField] private float staggerOutDuration = 0.1f;
		[SerializeField] private OptionButton buttonTemplate;

		private List<OptionButton> buttonPool;

		private void Awake ()
		{
			if (buttonTemplate == null)
				Debug.Log ("OptionPicker is missing a button template, so it can't create buttons.");
				buttonTemplate.gameObject.SetActive (false);
		}

		public void CreateOptions (Dictionary<IStatement, int> options, Action<int, OptionButton> onOptionPicked)
		{
			if (buttonPool == null)
				buttonPool = new List<OptionButton> ();

			while (buttonPool.Count < options.Count)
				buttonPool.Add (null);

			int i = 0;
			foreach (var option in options)
			{
				var button = buttonPool[i];
				if (button == null)
				{
					button = Instantiate (buttonTemplate, buttonTemplate.transform.parent);
					button.gameObject.SetActive (true);
					button.SetTransparency (0f);
					buttonPool[i] = button;
				}

				if (i < options.Count)
				{
					button.Show (i * staggerInDuration);
					button.SetText (option.Key.text);
					button.RemoveAllListeners ();
					button.AddListener (() =>
					{
						onOptionPicked.Invoke (option.Value, button);
					});
				}
				else
					button.Hide (i * staggerInDuration);

				i++;
			}
		}

		public void HideOptions (OptionButton ignore)
		{
			for (int i = 0; i < buttonPool.Count; i++)
				if (buttonPool[i] != null && buttonPool[i] != ignore)
					buttonPool[i].Hide (i * staggerOutDuration);
		}

		private void DelayedInvoke (Action action, float delay)
		{
			StartCoroutine (DelayedInvokeRoutine (action, delay));
		}
		private IEnumerator DelayedInvokeRoutine (Action action, float delay)
		{
			yield return new WaitForSeconds (delay);
			action.Invoke ();
		}
	}
}
