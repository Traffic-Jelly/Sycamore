using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using TMPro;

namespace Sycamore.Dialogue.UI
{
	[RequireComponent (typeof (CanvasGroup))]
	public class TextWriter : MonoBehaviour
	{
		private const string CLEAR_HEX = "<alpha=#00>";

		[SerializeField] private bool alwaysWriteInstant;
		[SerializeField] private bool playTypingSound = false;
		[SerializeField] private string[] ignoredText = new string[] { };
		[SerializeField] private TextMeshProUGUI text;

		private Coroutine writeRoutine;
		private CanvasGroup canvasGroup;

		public string LastString { get; protected set; }

		private void Awake ()
		{
			canvasGroup = GetComponent<CanvasGroup> ();
		}

		private void Start ()
		{
			Clear ();
		}

		public void Write (string s, TypingDelays typingDelay, Color color, float speed = 1f, bool additive = false, bool skippable = true, Action onComplete = null)
		{
			if (text == null)
				Debug.Log ("No text object assigned to TextWriter. Text cannot be written.");
			if (writeRoutine != null)
				StopCoroutine (writeRoutine);

			if (ignoredText.Contains (s))
			{
				Clear ();
				return;
			}

			text.color = color;

			if (alwaysWriteInstant)
			{
				WriteInstant (s);
				if (onComplete != null)
					onComplete.Invoke ();
			}
			else
				writeRoutine = StartCoroutine (WriteRoutine (s, speed, typingDelay, additive, skippable, onComplete));
		}

		public void WriteInstant (string s)
		{
			text.text = s;
		}

		public void Clear ()
		{
			text.text = string.Empty;
		}

		private IEnumerator WriteRoutine (string s, float speed, TypingDelays typingDelay, bool additive = false, bool skippable = true, Action onComplete = null)
		{
			string startText = additive ? (LastString + "\n") : string.Empty;
			string finalText = startText + s;
			int textLength = finalText.Length;

			// Have to store and check whether there's been input outside of the loop below
			// because it will be pausing alot for the different typing delays.
			bool anyKeyDown = false;
			if (skippable) StartCoroutine (CheckAnyKeyDown (() => anyKeyDown = true));

			var waitMultiplier = 1f / speed;
			var characterDelayWait = new WaitForSeconds (typingDelay.characterDelay * waitMultiplier);
			var sentenceDelayWait = new WaitForSeconds (typingDelay.sentenceDelay * waitMultiplier);
			var commaDelayWait = new WaitForSeconds (typingDelay.commaDelay * waitMultiplier);
			var finalDelayWait = new WaitForSeconds (typingDelay.finalDelay * waitMultiplier);

			int startIndex = startText.Length;
			for (int i = startIndex; i < textLength; i++)
			{
				if (anyKeyDown)
					break;

				char c = finalText[i];

				if (c == '<')
				{
					while (finalText[i] != '>' && i < finalText.Length)
					{
						i++;
						c = finalText[i];
					}
					i++;
				}
				else
				{
					if (playTypingSound)
						AudioController.Play ("Key Press");

					yield return characterDelayWait;

					if (c == '.' || c == '?' || c == '!')
						yield return sentenceDelayWait;
					else if (c == ',')
						yield return commaDelayWait;
				}

				// "Reveal" characters instead of adding them to prevent character movement from text formatting and alignment.
				text.text = finalText.Insert (i, CLEAR_HEX);
			}

			text.text = finalText;

			yield return finalDelayWait;

			if (onComplete != null) onComplete.Invoke ();

			LastString = finalText;
		}

		private IEnumerator CheckAnyKeyDown (Action onAnyKeyDown)
		{
			while (!Input.anyKeyDown && !Input.GetMouseButtonDown (0))
				yield return null;
			onAnyKeyDown.Invoke ();
		}
	}
}