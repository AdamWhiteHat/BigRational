using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ExtendedNumerics
{
	public class BigRational
	{
		#region Constructors

		public BigRational(BigInteger value)
			: this(value, Fraction.Zero)
		{
		}

		public BigRational(BigInteger whole, Fraction fraction)
			: this(whole, fraction.Numerator, fraction.Denominator)
		{
		}

		public BigRational(BigInteger whole, BigInteger numerator, BigInteger denominator)
		{
			WholePart = whole;
			FractionalPart = new Fraction(numerator, denominator);
		}

		public BigRational(Double value)
		{
			if (Double.IsNaN(value))
			{
				throw new ArgumentException("Value is not a number", nameof(value));
			}
			if (Double.IsInfinity(value))
			{
				throw new ArgumentException("Cannot represent infinity", nameof(value));
			}

			if (value == 0)
			{
				WholePart = BigInteger.Zero;
				FractionalPart = Fraction.Zero;
			}
			else if (value == 1)
			{
				WholePart = BigInteger.One;
				FractionalPart = Fraction.Zero;
			}
			else if (value == -1)
			{
				WholePart = BigInteger.MinusOne;
				FractionalPart = Fraction.Zero;
			}
			else
			{
				WholePart = (BigInteger)Math.Truncate(value);
				Double fract = value % 1;
				FractionalPart = (fract == 0) ? Fraction.Zero : new Fraction(fract);
			}
		}

		#endregion

		#region Properties

		public BigInteger WholePart { get; private set; }
		public Fraction FractionalPart { get; private set; }

		#endregion

		#region Arithmetic Methods

		public static BigRational Add(Fraction augend, Fraction addend)
		{
			return BigRational.Reduce(new BigRational(BigInteger.Zero, Fraction.Add(augend, addend)));
		}

		public static BigRational Add(BigRational augend, BigRational addend)
		{
			BigRational result = new BigRational(
					 BigInteger.Add(augend.WholePart, addend.WholePart),
					 Fraction.Add(augend.FractionalPart, addend.FractionalPart)
				);

			return BigRational.Reduce(result);
		}

		public static BigRational Subtract(Fraction minuend, Fraction subtrahend)
		{
			return BigRational.Reduce(new BigRational(BigInteger.Zero, Fraction.Subtract(minuend, subtrahend)));
		}

		public static BigRational Subtract(BigRational minuend, BigRational subtrahend)
		{
			BigRational left = BigRational.Expand(minuend);
			BigRational right = BigRational.Expand(subtrahend);

			BigRational result = new BigRational(
					BigInteger.Zero,
					Fraction.Subtract(left.FractionalPart, right.FractionalPart)
				);

			return BigRational.Reduce(result);
		}

		public static BigRational Multiply(Fraction multiplicand, Fraction multiplier)
		{
			return BigRational.Reduce(new BigRational(BigInteger.Zero, Fraction.Multiply(multiplicand, multiplier)));
		}

		public static BigRational Multiply(BigRational multiplicand, BigRational multiplier)
		{
			BigRational result = new BigRational(
					BigInteger.Multiply(multiplicand.WholePart, multiplicand.WholePart),
					Fraction.Multiply(multiplicand.FractionalPart, multiplicand.FractionalPart)
				);

			return BigRational.Reduce(result);
		}

		public static BigRational Divide(Fraction dividend, Fraction divisor)
		{
			return BigRational.Reduce(new BigRational(BigInteger.Zero, Fraction.Divide(dividend, divisor)));
		}

		public static BigRational Divide(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = new BigInteger(-1);
			BigInteger quotient = BigInteger.DivRem(dividend, divisor, out remainder);

			BigRational result = new BigRational(
					quotient,
					new Fraction(remainder, divisor)
				);

			return BigRational.Reduce(result);
		}

		public static BigRational Divide(BigRational dividend, BigRational divisor)
		{
			BigRational left = BigRational.Expand(dividend);
			BigRational right = BigRational.Expand(divisor);

			BigRational result = new BigRational(BigInteger.Zero, Fraction.Divide(left.FractionalPart, right.FractionalPart));
			return BigRational.Reduce(result);
		}

		public static BigRational Remainder(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = dividend % divisor;
			return BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(remainder, divisor)));
		}

		public static BigRational Abs(BigRational rational)
		{
			BigRational input = BigRational.Reduce(rational);

			return input.WholePart.Sign < 0
				?
				BigRational.Reduce(new BigRational(BigInteger.Abs(input.WholePart), input.FractionalPart))
				:
				BigRational.Reduce(input);
		}

		public static BigRational Negate(BigRational rational)
		{
			BigRational input = BigRational.Reduce(rational);
			return BigRational.Reduce(new BigRational(BigInteger.Negate(input.WholePart), input.FractionalPart));
		}

		#endregion

		#region Conversion Operators


		public static explicit operator BigRational(Double value)
		{
			return BigRational.Reduce(new BigRational(value));
		}

		public static explicit operator Double(BigRational value)
		{
			Double fract = (Double)value.FractionalPart;
			Double whole = (Double)value.WholePart;
			Double result = whole + (fract);
			return result;
		}

		public static explicit operator Fraction(BigRational value)
		{
			BigRational input = BigRational.Expand(value);
			return Fraction.Simplify(new Fraction(
					BigInteger.Add(input.FractionalPart.Numerator, BigInteger.Multiply(input.WholePart, input.FractionalPart.Denominator)),
					input.FractionalPart.Denominator
				));
		}

		#endregion

		#region Instance Methods

		public static BigRational Expand(BigRational value)
		{
			BigRational input = SynchronizeSigns(value);

			if (value.FractionalPart.Numerator > 0 || value.FractionalPart.Denominator > 1)
			{
				if (value.WholePart > 0)
				{
					Fraction newFractional = new Fraction(
						BigInteger.Add(value.FractionalPart.Numerator, BigInteger.Multiply(value.WholePart, value.FractionalPart.Denominator)),
						value.FractionalPart.Denominator
					);

					return new BigRational(BigInteger.Zero, newFractional);
				}
			}

			return value;
		}

		public static BigRational Reduce(BigRational value)
		{
			BigRational input = SynchronizeSigns(value);
			return Fraction.ReduceToProperFraction(input.FractionalPart);
		}

		private static BigRational SynchronizeSigns(BigRational value)
		{
			BigInteger whole;
			Fraction fract;

			if (value.WholePart.Sign > 0 && value.FractionalPart.Sign < 0)
			{
				whole = BigInteger.Negate(value.WholePart);
			}
			else
			{
				whole = value.WholePart;
			}

			if (value.WholePart.Sign < 0 && value.FractionalPart.Sign > 0)
			{
				fract = Fraction.Negate(value.FractionalPart);
			}
			else
			{
				fract = new Fraction(value.FractionalPart.Numerator, value.FractionalPart.Denominator);
			}

			return new BigRational(whole, fract);
		}

		public override string ToString()
		{
			string first = WholePart > 0 ? $"{WholePart}" : "0";
			string second = FractionalPart.Numerator > 0 ? $"{FractionalPart.Numerator} / {FractionalPart.Denominator}" : string.Empty;
			string join = string.Empty;

			if (!string.IsNullOrWhiteSpace(first) && !string.IsNullOrWhiteSpace(second))
			{
				join = " + ";
			}

			return string.Concat(first, join, second);
		}

		#endregion

	}
}
