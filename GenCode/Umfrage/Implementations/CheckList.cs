using System;
using System.Collections.Generic;
using System.Linq;

using Umfrage.Abstractions;
using Umfrage.Extensions;

namespace Umfrage.Implementations
{

	public class CheckList : SelectableList {

		public CheckList(string question, IEnumerable<IOption> options = null, string hint = "" ,  string defaultAnswer = null , int visibleOptions = 4, IQuestionnaire questionnaire = null) :
			base(question, options, hint , defaultAnswer, visibleOptions, questionnaire) {
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

		protected override int HandleInput(int column, int line, int activeOptionIndex) {

			IUserTerminal terminal = Terminal;

			bool answered = false;

			while (!answered) {

				ConsoleKeyInfo keyInfo = Console.ReadKey();

				switch (keyInfo.Key) {
					case ConsoleKey.Enter:
                        Answer = RedrawAnswer(line);

                        if (Answer.Trim().Length == 0 && DefaultAnswer != null)
                        {

                            _options
							.Where(op => DefaultAnswer.Split(',').Contains(op.Text) )
							.Select( op => op ).ToList().ForEach( defaultSelected => defaultSelected.Selected = true ); ;

                            Answer = RedrawAnswer(line);
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

						break;
					case ConsoleKey.Spacebar:

						Console.SetCursorPosition(column, line);

						IOption selectedItem = activeOptionIndex > -1 ? _options[ activeOptionIndex ] : null;
						if (selectedItem != null) {
							selectedItem.Selected = !selectedItem.Selected;
                            DrawOptions(activeOptionIndex);
						}

                        RedrawAnswer(line);

						break;

					case ConsoleKey.F2:
					default:
                        RedrawAnswer(line);
						break;
				}
			}

			return activeOptionIndex;
		}

		private string RedrawAnswer(int line) {

			var terminal = Terminal;

			string selected = "";
			this.ClearAnswer(line);
			terminal.ForegroundColor = Questionnaire.Settings.AnswerColor;

            var query = _options
							.Where(op => op.Selected == true)
							.Select(opt => opt.Text);

			if (query.Count() > 0) {
				selected = query.Aggregate((a, b) => $"{a}, {b}");
			}

			terminal.Printer.Write(selected);
			return selected;
		}

		protected override void DrawOptions(int active = -1) {
			IUserTerminal terminal = Terminal;

			// set terminal to next line
			terminal.ForegroundColor = Questionnaire.Settings.QuestionColor;
			terminal.Printer.WriteLine();

			int page = active / VisibleOptions;
			List<IOption> visible_items = Options.ToList().Skip(page * VisibleOptions).Take(VisibleOptions).ToList();

			for (int index = 0; index < visible_items.Count; index++) {
                PrintIndividualOption(visible_items[index], visible_items[index].Selected, active % VisibleOptions == index);
			}
		}

		protected override void PrintIndividualOption(IOption option, bool isActive, bool highlight = false) {

			IUserTerminal terminal = Terminal;

			if (highlight ) {
				terminal.ForegroundColor = Questionnaire.Settings.AnswerColor;
			}

			// which option is selected
			if (isActive) {
				terminal.Printer.Write("    [x] ");
			} else {
				terminal.Printer.Write("    [ ] ");
			}
			//	**********************************************************

			terminal.Printer.WriteLine($"{option.Text}");
			terminal.ForegroundColor = Questionnaire.Settings.QuestionColor;
		}

	}
}
