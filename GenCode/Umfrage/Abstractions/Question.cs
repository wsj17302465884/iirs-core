using System;

namespace Umfrage.Abstractions
{

	public enum QuestionStates {
		Initilaized, Valid, Invalid, Finished
	}

	public abstract class Question : IQuestion {

		public string Hint { get; set; } = "";
		public string Answer { get; protected set; }
		public string DefaultAnswer { get; set; } = null;
		public string Text { get; internal set; }

		public string ErrorMessage { get; set; } = "";

		public QuestionStates State { get; protected set; } = QuestionStates.Initilaized;
		public IQuestionnaire Questionnaire { get; set; }

		public IUserTerminal Terminal => Questionnaire.Terminal ?? null;

		private Func<IQuestion, bool> _Validator;

		public Question(string question, IQuestionnaire questionnaire = null , string hint = "" , string defaultAnswer = null) {

            Text = question ?? throw new ArgumentNullException($"{nameof(question)} cannot be null");
            Hint = hint;
            DefaultAnswer = defaultAnswer;
			Questionnaire = questionnaire;

		}

		// prints the question
		public virtual IQuestion Ask() {

			IUserTerminal terminal = Terminal;

			// 1. ask the question, simply buy just printing it
			terminal.ForegroundColor = Questionnaire.Settings.QuestionIconColor;
			terminal.Printer.Write($"{Questionnaire.Settings.QuestionIcon} ");


			terminal.ForegroundColor = Questionnaire.Settings.QuestionColor;
			terminal.Printer.Write(Text);

			// prints any hints available
			if (Hint.Trim().Length > 0) {
				terminal.ForegroundColor = Questionnaire.Settings.HintColor;
				terminal.Printer.Write($"( {Hint} )");
			}

			// a single space to seperate q/a
			terminal.Printer.Write(" ");

            // 2. wait for the user to give answer to the question
            _ = TakeAnswer();

			terminal.ResetColor();

			return this;
		}

		public virtual void PrintResult() {
			Terminal.Printer.WriteLine($"{Text}: {Answer}");
		}

		public override string ToString() {
			return $"{Text}: {Answer}";
		}

		private Action<IQuestion> OnFinish;
		// the finishing touches of the question, for example rendering some extra texts
		public void Finish(Action<IQuestion> done = null) {

			if (done != null) {
                OnFinish = done;
				return;
			}

			OnFinish?.Invoke(this);
			State = QuestionStates.Finished;
		}

		public IQuestion Validator(Func<IQuestion, bool> validator, string errorMessage) {
            ErrorMessage = errorMessage ?? throw new ArgumentNullException($"{nameof(errorMessage) } cannot be null");
            _Validator = validator ?? throw new ArgumentNullException($"{nameof(validator)} cannot be null");
			return this;
		}

		protected virtual bool Validate() {

			bool result = false;

			//try {
			if (_Validator == null) {
                // if no validator is provided then all the values will be considered true.
                _Validator = exp => true;
				//throw new InvalidOperationException(
				//	$@"The validation function is 'null', you should provide validation function by calling {nameof(this.Validator)} method}.");
			}

			result = _Validator(this); //.Invoke(this);

            State = result ? QuestionStates.Valid : QuestionStates.Invalid;

			if (result) {
                OnFinish?.Invoke(this);
			}

			//} catch(Exception ex) {

			//	//this.OnFinish?.Invoke(this, ex);
			//}

			return result;
		}

		// waits for the user to answer the question
		// Prints the validation errors
		protected virtual IQuestion PrintValidationErrors() {

			if (State == QuestionStates.Invalid && ErrorMessage.Trim().Length == 0) {
				throw new InvalidOperationException($"You must fill the property {nameof(ErrorMessage)}, when providing any validator. ");
			}

            Terminal.ForegroundColor = Questionnaire.Settings.ValidationIconColor;
			Terminal.Printer.Write(Questionnaire.Settings.ValidationIcon + " ");
			Terminal.ForegroundColor = Questionnaire.Settings.QuestionColor;
			Terminal.Printer.WriteLine(ErrorMessage);
			return this;
		}
		protected abstract IQuestion TakeAnswer();

		public object Clone() {
			return MemberwiseClone();
		}

	}

}
