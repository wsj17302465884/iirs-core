using System;
using System.Collections.Generic;
using System.Linq;

using Umfrage.Abstractions;
using Umfrage.Extensions;

namespace Umfrage.Implementations
{
	public class SelectableList : Question {

		protected IList<IOption> _options;
		public IEnumerable<IOption> Options => _options;

		internal bool ShowAsRadio { get; set; } = false;

		public int VisibleOptions { get; internal set; }
		public SelectableList AddOption(IOption option) {
            _options.Add(option);
			return this;
		}

		public SelectableList(string question, IEnumerable<IOption> options = null, string hint = "", string defaultAnswer = null, int visibleOptions = 4, IQuestionnaire questionnaire = null) :
			base(question, questionnaire, hint, defaultAnswer) {

            _options = new List<IOption>();

			if (options != null) {
				foreach (IOption option in options) {
                    _options.Add(option);
				}
			}

            VisibleOptions = visibleOptions;
		}

		protected override IQuestion TakeAnswer() {

			IUserTerminal terminal = Terminal;

			int column = Console.CursorLeft;
			int line = Console.CursorTop;

			int activeOption = -1;
			if (State == QuestionStates.Initilaized) {
                DrawOptions();
			}

			Console.SetCursorPosition(column, line);
			activeOption = HandleInput(column, line, activeOption);

            State = Validate() ? QuestionStates.Valid : QuestionStates.Invalid;

			if (State == QuestionStates.Invalid) {
				Console.SetCursorPosition(0, line + VisibleOptions + 1);
                PrintValidationErrors();
				Console.SetCursorPosition(column, line);
                TakeAnswer();

			} else {
				this.ClearLines(line + 1, line + VisibleOptions + 1);
			}

			// resets the cursor to the next line, 
			// because of the options the cursor might be in wrong position for the next question
			Console.SetCursorPosition(0, line + 1);

			terminal.ResetColor();
			return this;

		}

		protected virtual int HandleInput(int column, int line, int activeOptionIndex) {

			IUserTerminal terminal = Terminal;

			bool answered = false;

			while (!answered) {

				ConsoleKeyInfo keyInfo = Console.ReadKey();

				switch (keyInfo.Key) {
					case ConsoleKey.Enter:
                        Answer = activeOptionIndex > -1 ? _options[activeOptionIndex].Text : null;

						if (Answer == null && DefaultAnswer != null) {

                            _options
							.Where(op => DefaultAnswer.Split(',').Contains(op.Text))
							.Select(op => op).ToList().ForEach(defaultSelected => defaultSelected.Selected = true); ;

							Console.SetCursorPosition(column, line );
							terminal.ForegroundColor = Questionnaire.Settings.AnswerColor;
                            terminal.Printer.WriteLine(DefaultAnswer);

                            Answer = DefaultAnswer;
						}

						answered = true;
						break;
					case ConsoleKey.UpArrow:
						Console.SetCursorPosition(column, line);

						this.ClearLines(line + 1, line + _options.Count);
						Console.SetCursorPosition(column, line);

						--activeOptionIndex;
						if (activeOptionIndex == -1) {
							activeOptionIndex = _options.Count - 1;
						}

                        DrawOptions(activeOptionIndex);
						Console.SetCursorPosition(column, line);

						this.ClearAnswer(line);
						terminal.ForegroundColor = Questionnaire.Settings.AnswerColor;
						terminal.Printer.Write(_options[ activeOptionIndex ].Text);

						break;
					case ConsoleKey.DownArrow:
						Console.SetCursorPosition(column, line);

						this.ClearLines(line + 1, line + _options.Count);
						Console.SetCursorPosition(column, line);

						++activeOptionIndex;
						if (activeOptionIndex == _options.Count) {
							activeOptionIndex = 0;
						}

                        DrawOptions(activeOptionIndex);
						Console.SetCursorPosition(column, line);

						this.ClearAnswer(line);
						terminal.ForegroundColor = Questionnaire.Settings.AnswerColor;
						terminal.Printer.Write(_options[ activeOptionIndex ].Text);
						break;
					default:
						break;
				}
			}

			return activeOptionIndex;
		}

		protected virtual void DrawOptions(int active = -1) {

			IUserTerminal terminal = Terminal;

			// set terminal to next line
			terminal.ForegroundColor = Questionnaire.Settings.QuestionColor;
			terminal.Printer.WriteLine();

			int page = active / VisibleOptions;
			List<IOption> visible_items = _options.Skip(page * VisibleOptions).Take(VisibleOptions).ToList();

			for (int index = 0; index < visible_items.Count; index++) {

                PrintIndividualOption(visible_items[index], index == (active >= VisibleOptions ? active - VisibleOptions : active));

			}
		}

		protected virtual void PrintIndividualOption(IOption option, bool isActive, bool highlight = false) {

			IUserTerminal terminal = Terminal;

			// which option is selected
			if (isActive) {

				terminal.ForegroundColor = Questionnaire.Settings.AnswerColor;

				if (ShowAsRadio) {
					terminal.Printer.Write("    (O) ");
				} else {
					terminal.Printer.Write("    > ");
				}

			} else {

				if (ShowAsRadio) {
					terminal.Printer.Write("    ( ) ");
				} else {
					terminal.Printer.Write("    > ");
				}

			}
			//	**********************************************************

			terminal.Printer.WriteLine($"{option.Text}");
			terminal.ForegroundColor = Questionnaire.Settings.QuestionColor;
		}


	}
}
