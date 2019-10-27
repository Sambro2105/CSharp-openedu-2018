/*Практика «Pluralization»
Скачайте проект Pluralize

Напишите метод, склоняющий существительное «рублей» следующее за указанным числительным.

Например, для аргумента 10, метод должен вернуть «рублей», для 1 — вернуть «рубль», для 2 — «рубля».

Для проверки своего решения запустите скачанный проект.*/

namespace Pluralize
{
	public static class PluralizeTask
	{
		public static string PluralizeRubles(int count)
		{
			if (count % 100 == 11 || count % 100 == 12 || count % 100 == 13 || count % 100 == 14)
				return "рублей";
			else if (count % 10 == 0)
				return "рублей";
			else if (count % 10 == 1)
				return "рубль";
			else if (count % 10 == 2 || count % 10 == 3 || count % 10 == 4)
				return "рубля";
			else return "рублей";
		}
	}
}