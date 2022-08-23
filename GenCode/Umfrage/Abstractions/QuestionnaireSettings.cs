﻿using System;

namespace Umfrage.Abstractions
{
	public class QuestionnaireSetting
	{
		public string WelcomeMessage { get; set; } = "";
		public string QuestionIcon { get; set; } = "?";
		public ConsoleColor HintColor { get; set; } = ConsoleColor.Gray;
		public ConsoleColor AnswerColor { get; set; } = ConsoleColor.DarkCyan;
		public ConsoleColor QuestionColor { get; set; } = ConsoleColor.White;
		public ConsoleColor QuestionIconColor { get; set; } = ConsoleColor.DarkGreen;
		public string ValidationIcon { get; set; } = ">>";
		public ConsoleColor ValidationIconColor { get; set; } = ConsoleColor.DarkRed;
	}
}
