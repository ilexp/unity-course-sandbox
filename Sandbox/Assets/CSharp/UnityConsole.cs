using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace CSharpTutorial
{
	public class UnityConsole : MonoBehaviour
	{
		[SerializeField] private InputField input = null;
		[SerializeField] private Text output = null;
		[SerializeField] private ScrollRect outputView = null;

		private StringBuilder builder = new StringBuilder();
		private List<string> history = new List<string>();
		private int moveToBottom = 0;


		public void Write(string line)
		{
			this.history.Add(line ?? string.Empty);
			this.UpdateOutputText();
			this.ScrollToEnd();
		}
		public void ScrollToEnd()
		{
			this.moveToBottom = 5;
		}

		private void UpdateOutputText()
		{
			this.builder.Length = 0;
			for (int i = 0; i < this.history.Count; i++)
			{
				if (i > 0) this.builder.AppendLine();
				this.builder.Append(this.history[i]);
			}
			this.output.text = this.builder.ToString();
		}

		private void OnEnable()
		{
			this.UpdateOutputText();
			this.input.onEndEdit.AddListener(this.OnReadLine);
		}
		private void OnDisable()
		{
			this.input.onEndEdit.RemoveListener(this.OnReadLine);
		}
		private void Update()
		{
			// Due to lazy layout, we don't know the new content size until a few frames in.
			// Make sure we'll clamp to bottom at least X frames to be sure.
			if (this.moveToBottom > 0)
			{
				this.outputView.verticalScrollbar.value = 0.0f;
				this.moveToBottom--;
			}
		}

		private void OnReadLine(string text)
		{
			this.input.text = null;
			this.input.ActivateInputField();
			this.Write(text);
		}
	}
}