using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBigRational
{
	public static class HelperMethods
	{
		public static string FormatTimeSpan(TimeSpan timeSpan)
		{
			List<string> entries = new List<string>()
			{
				FormatTimeUnit(nameof(TimeSpan.Days), timeSpan.Days),
				FormatTimeUnit(nameof(TimeSpan.Hours), timeSpan.Hours),
				FormatTimeUnit(nameof(TimeSpan.Minutes), timeSpan.Minutes),
				FormatTimeUnit(nameof(TimeSpan.Seconds), timeSpan.Seconds),
				FormatTimeUnit(nameof(timeSpan.Milliseconds), timeSpan.Milliseconds),
			};
			return string.Join(", ", entries.Where(s => !string.IsNullOrEmpty(s)));
		}

		private static string FormatTimeUnit(string unitName, int quantity)
		{
			return (quantity > 0) ? $"{quantity} {unitName}" : string.Empty;
		}
	}
}
