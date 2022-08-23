using System;

using Umfrage.Abstractions;
using Umfrage.Extensions;

namespace Umfrage.Implementations
{

	public class Prompt : Question {

		public Prompt(string prompt, string hint = "", string defaultAnswer = null, IQuestionnaire questionnaire = null) :
			base(question: prompt, questionnaire: questionnaire,hint: hint , defaultAnswer: defaultAnswer) {
		}

		protected override IQuestion TakeAnswer() {

			IUserTerminal terminal = Terminal;

			// this should be before ReadLine, 
			// because ReadLine will eliminate the current Lef position and reset it to 0
			int cursorLeft = Console.CursorLeft;

            // always set the color of terminal to AnswerColor
            terminal.ForegroundColor = Questionnaire.Settings.AnswerColor;
            Answer = terminal.Scanner.ReadLine();

			// this should be after ReadLine because 
			// before the user enters the input he/she might resize the console, 
			// hence the top will change 
			int cursorTop = Console.CursorTop;

			if (Answer.Trim().Length == 0 && DefaultAnswer != null) {
				Console.SetCursorPosition(left: cursorLeft, top: cursorTop - 1);
                terminal.Printer.Write(DefaultAnswer);
                Answer = DefaultAnswer;
			}

            bool result = Validate();
            State = result ? QuestionStates.Valid : QuestionStates.Invalid;

			if (result) {
				this.ClearLine(cursorTop);
            }
            else
            {

                PrintValidationErrors();

				// -1 because of read line
				int line = cursorTop - 1;
				this.ClearAnswer(line: line);
				Console.SetCursorPosition(left: cursorLeft, top: line);
				return TakeAnswer();
			}

			terminal.ResetColor();

			return this;
		}

	}
}