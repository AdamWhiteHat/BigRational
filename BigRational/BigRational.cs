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

		public BigRational(BigInteger whole, Fraction fraction)
		{
			WholePart = whole;
			FractionalPart = fraction;
		}

		public BigRational(BigInteger value)
			: this(value, Fraction.Zero)
		{
		}

		public BigRational(BigInteger whole, BigInteger numerator, BigInteger denominator)
			: this(whole, new Fraction(numerator, denominator))
		{
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

		public static BigRational Add(BigRational augend, BigRational addend)
		{
			BigRational result = new BigRational(
					 BigInteger.Add(augend.WholePart, addend.WholePart),
					 Fraction.Add(augend.FractionalPart, addend.FractionalPart)
				);

			result.Reduce();
			return result;
		}

		public static BigRational Subtract(BigRational minuend, BigRational subtrahend)
		{
			BigRational result = new BigRational(
					BigInteger.Subtract(minuend.WholePart, subtrahend.WholePart),
					Fraction.Subtract(minuend.FractionalPart, subtrahend.FractionalPart)
				);

			result.Reduce();
			return result;
		}

		public static BigRational Multiply(BigRational multiplicand, BigRational multiplier)
		{
			BigRational result = new BigRational(
					BigInteger.Multiply(multiplicand.WholePart, multiplicand.WholePart),
					Fraction.Multiply(multiplicand.FractionalPart, multiplicand.FractionalPart)
				);

			result.Reduce();
			return result;
		}

		public static BigRational Divide(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = new BigInteger(-1);
			BigInteger quotient = BigInteger.DivRem(dividend, divisor, out remainder);

			BigRational result = new BigRational(
					quotient,
					new Fraction(remainder, divisor)
				);

			result.Reduce();
			return result;
		}

		public static BigRational Remainder(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = dividend % divisor;

			return new BigRational(
					0,
					new Fraction(remainder, divisor)
				);
		}

		public static BigRational Abs(BigRational rational)
		{
			rational.Reduce();
			return rational.WholePart.Sign < 0
				?
				new BigRational(BigInteger.Abs(rational.WholePart), rational.FractionalPart)
				:
				rational;
		}

		public static BigRational Negate(BigRational rational)
		{
			rational.Reduce();
			return new BigRational(BigInteger.Negate(rational.WholePart), rational.FractionalPart);
		}

		#endregion

		#region Conversion Operators


		public static explicit operator BigRational(Double value)
		{
			return new BigRational(value);
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
			value.Expand();
			return new Fraction(
					BigInteger.Add(value.FractionalPart.Numerator, BigInteger.Multiply(value.WholePart, value.FractionalPart.Denominator)),
					value.FractionalPart.Denominator
				);
		}

		#endregion

		#region Instance Methods

		public void Expand()
		{
			SynchronizeSigns();

			if (FractionalPart.Numerator > 0 && FractionalPart.Denominator > 1)
			{
				if (WholePart > 0)
				{
					Fraction newFractional = new Fraction(
						BigInteger.Add(FractionalPart.Numerator, BigInteger.Multiply(WholePart, FractionalPart.Denominator)),
						FractionalPart.Denominator
					);

					FractionalPart = newFractional;
					WholePart = BigInteger.Zero;
				}
			}
		}

		public void Reduce()
		{
			SynchronizeSigns();

			WholePart += FractionalPart.ReduceToProperFraction();
		}

		private void SynchronizeSigns()
		{
			if (WholePart.Sign > 0 && FractionalPart.Sign < 0)
			{
				WholePart = BigInteger.Negate(WholePart);
			}

			if (WholePart.Sign < 0 && FractionalPart.Sign > 0)
			{
				FractionalPart = Fraction.Negate(FractionalPart);
			}
		}

		public override string ToString()
		{
			Reduce();

			string first = WholePart > 0 ? $"{WholePart}" : string.Empty;
			string second = FractionalPart.Numerator > 0 ? $"{FractionalPart.Numerator} / {FractionalPart.Denominator}" : string.Empty;
			string join = string.Empty;

			if (!string.IsNullOrWhiteSpace(first) && string.IsNullOrWhiteSpace(second))
			{
				join = " + ";
			}

			return string.Concat(first, join, second);
		}

		#endregion

	}
}
