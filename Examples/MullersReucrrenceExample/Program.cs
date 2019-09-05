using ExtendedNumerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MullersReucrrenceExample
{
	class Program
	{
		static void Main(string[] args)
		{
			int iterationCount = 141;
			List<string>[] matrix = new[] {
				InterationsList(iterationCount,11).ToList(),
				MullersRecurrence<float>(iterationCount,10).ToList(),
				MullersRecurrence<double>(iterationCount,20).ToList(),
				MullersRecurrence<decimal>(iterationCount,31).ToList(),
				MullersRecurrence<BigRational>(iterationCount,31).ToList()
			};
			for (int y = 0; y < iterationCount; ++y)
			{
				for (int x = 0; x < matrix.Length; ++x)
				{
					Console.Write($" {matrix[x][y]}");
				}
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine("Press Enter To Continue");
			Console.ReadLine();
		}

		public static IEnumerable<string> InterationsList(int iterations, int stringLength)
		{
			yield return "Iterations".PadRight(stringLength);
			for (int i = 1; i < iterations; ++i)
			{
				yield return i.ToString().PadLeft(3).PadRight(stringLength);
			}
		}

		public static IEnumerable<string> MullersRecurrence<T>(int iterations, int stringLength)
		{
			string typeString = typeof(T).ToString();
			yield return $"{typeString.Substring(typeString.LastIndexOf('.')+1)}".PadRight(stringLength);
			T c108 = Convert<T>(108d);
			T c815 = Convert<T>(815d);
			T c1500 = Convert<T>(1500d);

			T x0 = Convert<T>(4d);
			T x1 = Convert<T>(4.25d);

			yield return FormatString(x0, stringLength);
			yield return FormatString(x1, stringLength);

			for (int i = 3; i < iterations; ++i)
			{
				// c108 - ((c185 - 1500 / x0) / x1);
				T part1 = Divide(c1500, x0);
				T part2 = Subtract(c815, part1);
				T part3 = Divide(part2, x1);
				T part4 = Subtract(c108, part3);

				T xNext = part4;

				yield return FormatString(xNext, stringLength);

				x0 = x1;
				x1 = xNext;
			}
		}

		public static string FormatString<T>(T value, int stringLength)
		{
			// Convert the value to a 30 digit string, which equates to Decimal type
			string stringValue = Convert<T, Decimal>(value).ToString();
			int decimalPointPos = stringValue.IndexOf('.');
			if (decimalPointPos<0)
			{
				stringValue = $"{stringValue}.0";
				decimalPointPos = stringValue.IndexOf('.');
			}
			if (decimalPointPos < 3)
			{
				stringValue = stringValue.PadLeft(stringValue.Length + 3 - decimalPointPos);
			}
			if (stringValue.Length < stringLength)
			{
				stringValue = stringValue.PadRight(stringLength);
			}
			return stringValue;
		}


		public static T Convert<T>(double a) => Convert<double, T>(a);
		public static T2 Convert<T1, T2>(T1 a)
		{
			return ConvertImplementation<T1, T2>.Function(a);
		}
		internal static class ConvertImplementation<T1, T2>
		{
			internal static Func<T1, T2> Function = (T1 a) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T1));
				Expression BODY = Expression.Convert(A, typeof(T2));
				Function = Expression.Lambda<Func<T1, T2>>(BODY, A).Compile();
				return Function(a);
			};
		}

		public static T Subtract<T>(T a, T b)
		{
			return SubtractImplementation<T>.Function(a, b);
		}

		internal static class SubtractImplementation<T>
		{
			internal static Func<T, T, T> Function = (T a, T b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T));
				ParameterExpression B = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Subtract(A, B);
				Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}

		public static T Divide<T>(T a, T b)
		{
			return DivideImplementation<T>.Function(a, b);
		}

		internal static class DivideImplementation<T>
		{
			internal static Func<T, T, T> Function = (T a, T b) =>
			{
				ParameterExpression A = Expression.Parameter(typeof(T));
				ParameterExpression B = Expression.Parameter(typeof(T));
				Expression BODY = Expression.Divide(A, B);
				Function = Expression.Lambda<Func<T, T, T>>(BODY, A, B).Compile();
				return Function(a, b);
			};
		}
	}
}
