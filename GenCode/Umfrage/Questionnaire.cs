using System;
using System.Collections.Generic;
using System.Linq;

using Umfrage.Abstractions;
using Umfrage.Builders;
using Umfrage.Builders.Abstractions;
using Umfrage.Implementations;

namespace Umfrage
{
        // Questionnaire
	public class Questionnaire : IQuestionnaire {

		#region fields: 

		private int currentStep = 0;
		private IBranch currentBranch = null;

		private bool hasStarted = false;
		private bool branchSwitched = false;

		private List<IBranch> _branches;
		private List<IQuestion> _questions;
		private List<IQuestion> _processedQuestions;

		#endregion

		#region properties : 

		public bool CanProceed => NextQuestion != null;

		//public IQuestionBuilder Builder { get; }

		public IUserTerminal Terminal { get; private set; }

		public IEnumerable<IBranch> Branches => _branches;
		public IEnumerable<IQuestion> Questions => _questions;

		public IEnumerable<IQuestion> ProcessedQuestions => _processedQuestions;


		private int Count => currentBranch?.Questions.Count() ?? _questions.Count;

		public IQuestion PreviousQuestion {
			get {
				// you are at hte beginning of the list , so no previous questions
				if (currentStep == 0) {
					return null;
				}

                return currentBranch == null ? _questions[currentStep - 1] : currentBranch.Questions.ToList()[currentStep - 1];
			}
		}

		public IQuestion CurrentQuestion {
			get {
				// this.should never happen
				if (currentStep >= Count) {
					return null;
				}

                return currentBranch == null ? _questions[currentStep] : currentBranch.Questions.ToList()[currentStep];
			}
		}

		public IQuestion NextQuestion {
			get {
				// you are at the last question so no more questions exists
				if (currentStep + 1 == Count) {
					return null;
				}

				try {
                    return currentBranch == null ? _questions[currentStep + 1] : currentBranch.Questions.ToList()[currentStep + 1];
				} catch {
					return null;
				}

			}
		}

		public QuestionnaireSetting Settings { get; set; }

		#endregion

		public Questionnaire(IUserTerminal userConsole = null, QuestionnaireSetting settings = null ) {

            //this.Builder = builder ?? new QuestionBuilder();
            Terminal = userConsole ?? new UserTerminal();
            Settings = settings ?? new QuestionnaireSetting();

            _branches = new List<IBranch>();
            _questions = new List<IQuestion>();
            _processedQuestions = new List<IQuestion>();
		}

		private void AskQuestionWaitAnswer() {

			IQuestion question = CurrentQuestion;

			question.Ask();

            _processedQuestions.Add(CurrentQuestion);
		}

		private IBranch FindBranch(string branchName) {
			IBranch branch = _branches.SingleOrDefault( b => b.Name == branchName );

			if (branch == null) {
				throw new InvalidOperationException($"The branch '{branchName}' does not exists");
			}

			return branch;
		}

		public IQuestionnaire Start() {

			if (_questions.Count == 0) {
				throw new InvalidOperationException("There is no questions in this questionnaire, please first add some");
			}

			if (hasStarted) {
				throw new InvalidOperationException("You cannot start a questionnaire more than once.");
			}

            hasStarted = true;

            Terminal.Printer.WriteLine(Settings.WelcomeMessage);

            AskQuestionWaitAnswer();

			return this;
		}

		public IQuestionnaire GoToBranch(string branchName) {

			if (CurrentQuestion.State != QuestionStates.Finished) {
				throw new InvalidOperationException("You cannot go to next step, unless you finish the previous question");
			}

			IBranch branch = FindBranch( branchName );

			if (branch.Questions.Count() == 0) {
				throw new InvalidOperationException("You can not switch to a branch without questions");
			}

            // Coz after this switching the user must call 
            // "Next" method to activate the first question in the branch
            currentStep = 0;
            currentBranch = branch;
            branchSwitched = true;

			return this;
		}

		public IQuestionnaire Next() {

			if (!hasStarted) {
				throw new InvalidOperationException("You have not strted the questionnaire yet");
			}

			// do not proceed if the currently asked question is not in Valid state
			if (CurrentQuestion.State != QuestionStates.Valid) {
				return this;
			}

			// checks to see whether we have just switched to the new branch, 
			// if so, there is no need to prceed as the pointer already points to the first question
			if (branchSwitched) {
                branchSwitched = false;
			} else {
                // proceeds the counter
                currentStep++;
			}

			if (currentStep == Count) {
                // to cancel the effect of previous addition to the current step;
                currentStep--;
				throw new InvalidOperationException("Your at the end of the questionnaire, there is no more questions.");
			}

            AskQuestionWaitAnswer();

			return this;
		}

		public IQuestionnaire GotToStep(int step, string branchName = null) {

			if (CurrentQuestion.State != QuestionStates.Finished) {
				throw new InvalidOperationException("You cannot go to next step, unless you finish the previous question");
			}

			// check wheather the user wants to swithc to a step in another branch or not
			IBranch branch = branchName != null ? FindBranch( branchName ) : null;

			// the step should not be more than the length of questions in the target branch
			int count = branch != null ? branch.Questions.Count( ) : _questions.Count;

			// if step is out of range then an IndexOutOfRangeException will be thrown
			if (step < 0 || step > count) {
				throw new IndexOutOfRangeException($@"{nameof(step)} should be between 0 and {count}, 
                                                            number of questions in your branch.");
			}

            // Set current branch and current step
            currentBranch = branch;
            currentStep = step - 1;

			return this;

		}

		public IQuestionnaire Add(IBranch branch) {
            _branches.Add(branch);

			return this;
		}

		public IQuestionnaire Add(IQuestion question, IBranch branch = null, bool here = false) {

			// the question already blongs to a quesstionnnaire
			if( question.Questionnaire != null) {
				throw new InvalidOperationException($"The {nameof(question)} already belongs to a questionnaire, cannot add it.");
			}

			question.Questionnaire = this;

			if (branch == null) {

				if (here) {
                    _questions.Insert(currentStep + 1, question);
				} else {
                    _questions.Add(question);
				}
			} else {

				if (here) {
					branch.Add(question, currentStep + 1);
				} else {
					branch.Add(question);
				}

			}
			return this;
		}

		public void End() {
            Terminal.Printer.WriteLine();
            Terminal.Printer.Write("---- END OF Questionnaire ----");
            Terminal.Printer.WriteLine();
		}

		public IQuestionnaire Prev() {

			if (currentStep == 0) {
				throw new InvalidOperationException("You are at the beginning of the questionnaire, there is no previous question.");
			}

            currentStep--;

            AskQuestionWaitAnswer();

			return this;

		}

	}
}
