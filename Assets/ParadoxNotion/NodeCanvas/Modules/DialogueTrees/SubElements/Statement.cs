using NodeCanvas.Framework;
using UnityEngine;
using System.Linq;

namespace NodeCanvas.DialogueTrees
{
	public enum InputWaitMode { None, Beginning, End }

	///An interface to use for whats being said by a dialogue actor
	public interface IStatement
	{
		string text { get; }
		AudioClip audio { get; }
		bool additive { get; }
		bool skippable { get; }
		float speed { get; }
		float endDelay { get; }
		float fadeInDuration { get; }
		float fadeOutDuration { get; }
		InputWaitMode inputWaitMode { get; }
		string meta { get; }
	}

	///Holds data of what's being said usualy by an actor
	[System.Serializable]
	public class Statement : IStatement
	{

		[SerializeField]
		private string _text = string.Empty;
		[SerializeField]
		private AudioClip _audio;
		[SerializeField]
		private bool _additive;
		[SerializeField]
		private bool _skippable = true;
		[SerializeField]
		private InputWaitMode _inputWaitMode = InputWaitMode.End;
		[SerializeField]
		private float _speed = 1f;
		[SerializeField]
		private float _endDelay;
		[SerializeField]
		private float _fadeInDuration;
		[SerializeField]
		private float _fadeOutDuration;
		[SerializeField]
		private string _meta = string.Empty;

		public string text
		{
			get { return _text; }
			set { _text = value; }
		}

		public AudioClip audio
		{
			get { return _audio; }
			set { _audio = value; }
		}

		public bool additive
		{
			get { return _additive; }
			set { _additive = value; }
		}

		public bool skippable
		{
			get { return _skippable; }
			set { _skippable = value; }
		}

		public InputWaitMode inputWaitMode
		{
			get { return _inputWaitMode; }
			set { _inputWaitMode = value; }
		}

		public float speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

		public float endDelay
		{
			get { return _endDelay; }
			set { _endDelay = value; }
		}

		public float fadeInDuration
		{
			get { return _fadeInDuration; }
			set { _fadeInDuration = value; }
		}

		public float fadeOutDuration
		{
			get { return _fadeOutDuration; }
			set { _fadeOutDuration = value; }
		}

		public string meta
		{
			get { return _meta; }
			set { _meta = value; }
		}

		//required
		public Statement () { }
		public Statement (string text)
		{
			this.text = text;
		}

		public Statement (string text, AudioClip audio)
		{
			this.text = text;
			this.audio = audio;
		}

		public Statement (
			string text, 
			AudioClip audio, 
			bool additive, 
			bool skippable, 
			InputWaitMode inputWaitMode, 
			float speed, 
			float endDelay, 
			float fadeInDuration, 
			float fadeOutDuration, 
			string meta)
		{
			this.text = text;
			this.audio = audio;
			this.additive = additive;
			this.skippable = skippable;
			this.inputWaitMode = inputWaitMode;
			this.speed = speed;
			this.endDelay = endDelay;
			this.fadeInDuration = fadeInDuration;
			this.fadeOutDuration = fadeOutDuration;
			this.meta = meta;
		}

		///Replace the text of the statement found in brackets, with blackboard variables ToString and returns a Statement copy
		public Statement BlackboardReplace (IBlackboard bb)
		{
			var s = text;
			var i = 0;
			while ((i = s.IndexOf ('[', i)) != -1)
			{

				var end = s.Substring (i + 1).IndexOf (']');
				var input = s.Substring (i + 1, end); //what's in the brackets
				var output = s.Substring (i, end + 2); //what should be replaced (includes brackets)

				object o = null;
				if (bb != null)
				{ //referenced blackboard replace
					var v = bb.GetVariable (input, typeof (object));
					if (v != null)
					{
						o = v.value;
					}
				}

				if (input.Contains ("/"))
				{ //global blackboard replace
					var globalBB = GlobalBlackboard.Find (input.Split ('/').First ());
					if (globalBB != null)
					{
						var v = globalBB.GetVariable (input.Split ('/').Last (), typeof (object));
						if (v != null)
						{
							o = v.value;
						}
					}
				}

				s = s.Replace (output, o != null ? o.ToString () : output);

				i++;
			}

			var copy = ParadoxNotion.Serialization.JSONSerializer.Clone<Statement> (this);
			copy.text = s;
			return copy;
		}

		public override string ToString ()
		{
			return text;
		}
	}
}