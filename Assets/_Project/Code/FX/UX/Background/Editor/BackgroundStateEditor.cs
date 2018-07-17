using UnityEngine;
using UnityEditor;

namespace Sycamore.FX.UX
{
	[CustomEditor (typeof (BackgroundState))]
	public class BackgroundStateEditor : Editor
	{
		private BackgroundManager managerInstance;

		public override void OnInspectorGUI ()
		{
			base.OnInspectorGUI ();

			if (managerInstance == null)
				managerInstance = FindObjectOfType<BackgroundManager> ();
			if (managerInstance == null)
				return;

			var state = target as BackgroundState;

			managerInstance.SetColorA (state.colorA);
			managerInstance.SetColorB (state.colorB);
			managerInstance.SetScale (state.scale);
			managerInstance.SetOctaves (state.octaves);
			managerInstance.SetSpeed (state.speed);
			managerInstance.SetBias (state.bias);
		}
	}
}