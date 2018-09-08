using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Sycamore.FX.UX
{
	[RequireComponent (typeof (Image))]
	[ExecuteInEditMode]
	public class BackgroundManager : MonoSingleton<BackgroundManager>
	{
		private readonly string COLOR_A = "_ColorA";
		private readonly string COLOR_B = "_ColorB";
		private readonly string SCALE = "_Scale";
		private readonly string OCTAVES = "_Octaves";
		private readonly string BIAS = "_Bias";
		private readonly string CONTRAST = "_Contrast";
		private readonly string OFFSET = "_Offset";
		private readonly string ANGLE = "_Angle";

		[HideInInspector] [SerializeField] private float speed = 0.2f;
		[HideInInspector] [SerializeField] private float rotationSpeed = 0f;

		private Image _image;
		public Image Image
		{
			get
			{
				if (_image == null)
					_image = GetComponent<Image> ();
				return _image;
			}
		}

		private Material _material;
		public Material Material
		{
			get
			{
				if (_material == null)
					_material = Image.material = new Material (Image.material);
				return _material;
			}
		}

		private void Start ()
		{
			SetOffset (0f);
		}
		private void Update ()
		{
			SetOffset (GetOffset () + speed * Time.deltaTime);
			SetAngle (GetAngle () + rotationSpeed * Time.deltaTime);
		}

		public void SetColorA (Color color)
		{
			Material.SetColor (COLOR_A, color);
		}
		public Color GetColorA ()
		{
			return Material.GetColor (COLOR_A);
		}

		public void SetColorB (Color color)
		{
			Material.SetColor (COLOR_B, color);
		}
		public Color GetColorB ()
		{
			return Material.GetColor (COLOR_B);
		}

		public void SetScale (float scale)
		{
			Material.SetFloat (SCALE, scale);
		}
		public float GetScale ()
		{
			return Material.GetFloat (SCALE);
		}

		public void SetOctaves (float octaves)
		{
			Material.SetFloat (OCTAVES, Mathf.Clamp (octaves, 1f, 3f));
		}
		public float GetOctaves ()
		{
			return Material.GetFloat (OCTAVES);
		}

		public void SetBias (float bias)
		{
			Material.SetFloat (BIAS, bias);
		}
		public float GetBias ()
		{
			return Material.GetFloat (BIAS);
		}

		public void SetContrast (float contrast)
		{
			Material.SetFloat (CONTRAST, contrast);
		}
		public float GetContrast ()
		{
			return Material.GetFloat (CONTRAST);
		}

		public void SetSpeed (float speed)
		{
			this.speed = speed;
		}
		public float GetSpeed ()
		{
			return speed;
		}

		public void SetRotationSpeed (float rotationSpeed)
		{
			this.rotationSpeed = rotationSpeed;
		}
		public float GetRotationSpeed ()
		{
			return rotationSpeed;
		}

		private void SetOffset (float offset)
		{
			Material.SetFloat (OFFSET, offset);
		}
		private float GetOffset ()
		{
			return Material.GetFloat (OFFSET);
		}

		private void SetAngle (float angle)
		{
			Material.SetFloat (ANGLE, angle);
		}
		private float GetAngle ()
		{
			return Material.GetFloat (ANGLE);
		}

		public void TweenState (BackgroundState state, float duration, AnimationCurve curve = null)
		{
			TweenColorA (state.colorA, duration, curve);
			TweenColorB (state.colorB, duration, curve);
			TweenScale (state.scale, duration, curve);
			TweenOctaves (state.octaves, duration, curve);
			TweenSpeed (state.speed, duration, curve);
			TweenBias (state.bias, duration, curve);
			TweenRotationSpeed (state.rotationSpeed, duration, curve);
		}
		public void TweenState (BackgroundState state, float duration, Ease ease)
		{
			TweenColorA (state.colorA, duration, ease);
			TweenColorB (state.colorB, duration, ease);
			TweenScale (state.scale, duration, ease);
			TweenOctaves (state.octaves, duration, ease);
			TweenSpeed (state.speed, duration, ease);
			TweenBias (state.bias, duration, ease);
			TweenRotationSpeed (state.rotationSpeed, duration, ease);
		}

		public void TweenColorA (Color to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetColorA (), (c) => SetColorA (c), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenColorA (Color to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetColorA (), (c) => SetColorA (c), to, duration);
			t.SetEase (ease);
		}

		public void TweenColorB (Color to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetColorB (), (c) => SetColorB (c), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenColorB (Color to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetColorB (), (c) => SetColorB (c), to, duration);
			t.SetEase (ease);
		}

		public void TweenScale (float to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetScale (), (s) => SetScale (s), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenScale (float to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetScale (), (s) => SetScale (s), to, duration);
			t.SetEase (ease);
		}

		public void TweenOctaves (float to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetOctaves (), (o) => SetOctaves (o), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenOctaves (float to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetOctaves (), (o) => SetOctaves (o), to, duration);
			t.SetEase (ease);
		}

		public void TweenBias (float to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetBias (), (o) => SetBias (o), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenBias (float to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetBias (), (o) => SetBias (o), to, duration);
			t.SetEase (ease);
		}

		public void TweenContrast (float to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetContrast (), (o) => SetContrast (o), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenContrast (float to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetContrast (), (o) => SetContrast (o), to, duration);
			t.SetEase (ease);
		}

		public void TweenSpeed (float to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetSpeed (), (s) => SetSpeed (s), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenSpeed (float to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetSpeed (), (s) => SetSpeed (s), to, duration);
			t.SetEase (ease);
		}

		public void TweenRotationSpeed (float to, float duration, AnimationCurve curve = null)
		{
			var t = DOTween.To (() => GetRotationSpeed (), (s) => SetRotationSpeed (s), to, duration);
			if (curve != null)
				t.SetEase (curve);
		}
		public void TweenRotationSpeed (float to, float duration, Ease ease)
		{
			var t = DOTween.To (() => GetRotationSpeed (), (s) => SetRotationSpeed (s), to, duration);
			t.SetEase (ease);
		}
	}
}