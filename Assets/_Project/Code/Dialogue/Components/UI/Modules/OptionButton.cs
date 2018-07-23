using UnityEngine;
using System;

namespace Sycamore.Dialogue.UI
{
	public abstract class OptionButton : MonoBehaviour
	{
		public abstract void Show (float delay);
		public abstract void Hide (float delay);
		public abstract void Select ();

		public abstract void SetText (string text);
		public abstract void SetInteractable (bool interactable);
		public abstract void SetTransparency (float transparency);

		public abstract void AddListener (Action onClick);
		public abstract void RemoveAllListeners ();
	}
}