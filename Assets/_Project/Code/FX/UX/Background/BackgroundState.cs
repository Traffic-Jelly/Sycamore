using UnityEngine;

namespace Sycamore.FX.UX
{
	[CreateAssetMenu (menuName = "Sycamore/FX/UX/Background State")]
	public class BackgroundState : ScriptableObject
	{
		public Color colorA = Color.white;
		public Color colorB = Color.black;
		public float scale = 1f;
		[Range (1f, 4f)]
		public float octaves = 2f;
		public float speed = 1f;
		public float bias = 1f;
		public float contrast = 1f;
	}
}