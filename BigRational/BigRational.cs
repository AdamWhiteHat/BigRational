using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ExtendedNumerics
{
	public class BigRational : IComparable, IComparable<BigRational>, IEquatable<BigRational>
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
				WholePart = BigInteger.Zero;
				FractionalPart = Fraction.One;
			}
			else if (value == -1)
			{
				WholePart = BigInteger.Zero;
				FractionalPart = Fraction.MinusOne;
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
			return new BigRational(BigInteger.Zero, Fraction.Add(augend, addend));
		}

		public static BigRational Add(BigRational augend, BigRational addend)
		{
			Fraction fracExpandedAugend = BigRational.Expand(augend).FractionalPart;
			Fraction fracExpandedAddend = BigRational.Expand(addend).FractionalPart;

			BigRational result = Add(fracExpandedAugend, fracExpandedAddend);
			BigRational reduced = BigRational.Reduce(result);
			return reduced;
		}

		public static BigRational Subtract(Fraction minuend, Fraction subtrahend)
		{
			return new BigRational(BigInteger.Zero, Fraction.Subtract(minuend, subtrahend));
		}

		public static BigRational Subtract(BigRational minuend, BigRational subtrahend)
		{
			Fraction fracExpandedMinuend = BigRational.Expand(minuend).FractionalPart;
			Fraction fracExpandedSubtrahend = BigRational.Expand(subtrahend).FractionalPart;

			BigRational result = Subtract(fracExpandedMinuend, fracExpandedSubtrahend);
			BigRational reduced = BigRational.Reduce(result);
			return reduced;
		}

		public static BigRational Multiply(Fraction multiplicand, Fraction multiplier)
		{
			return new BigRational(BigInteger.Zero, Fraction.Multiply(multiplicand, multiplier));
		}

		public static BigRational Multiply(BigRational multiplicand, BigRational multiplier)
		{
			Fraction fracExpandedMultiplicand = BigRational.Expand(multiplicand).FractionalPart;
			Fraction fracExpandedMultiplier = BigRational.Expand(multiplier).FractionalPart;

			BigRational result = Multiply(fracExpandedMultiplicand, fracExpandedMultiplier);
			BigRational reduced = BigRational.Reduce(result);
			return reduced;
		}

		public static BigRational Divide(Fraction dividend, Fraction divisor)
		{
			return new BigRational(BigInteger.Zero, Fraction.Divide(dividend, divisor));
		}

		public static BigRational Divide(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = new BigInteger(-1);
			BigInteger quotient = BigInteger.DivRem(dividend, divisor, out remainder);

			BigRational result = new BigRational(
					quotient,
					new Fraction(remainder, divisor)
				);

			return result;
		}

		public static BigRational Divide(BigRational dividend, BigRational divisor)
		{
			Fraction left = BigRational.Expand(dividend).FractionalPart;
			Fraction right = BigRational.Expand(divisor).FractionalPart;

			BigRational result = Divide(left, right);
			BigRational reduced = BigRational.Reduce(result);
			return reduced;
		}

		public static BigRational Remainder(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = dividend % divisor;
			return new BigRational(BigInteger.Zero, new Fraction(remainder, divisor));
		}

		public static BigRational Abs(BigRational rational)
		{
			BigRational input = BigRational.Reduce(rational);

			return input.WholePart.Sign < 0
				?
				new BigRational(BigInteger.Abs(input.WholePart), input.FractionalPart)
				:
				input;
		}

		public static BigRational Negate(BigRational rational)
		{
			BigRational input = rational;
			return new BigRational(BigInteger.Negate(input.WholePart), input.FractionalPart);
		}

		#endregion

		#region Conversion Operators


		public static explicit operator BigRational(Double value)
		{
			return new BigRational(BigInteger.Zero, (Fraction)value);
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
			return Fraction.Simplify(new Fraction(
					BigInteger.Add(value.FractionalPart.Numerator, BigInteger.Multiply(value.WholePart, value.FractionalPart.Denominator)),
					value.FractionalPart.Denominator
				));
		}

		#endregion

		#region Comparison Operators

		public static bool operator ==(BigRational left, BigRational right) { return Compare(left, right) == 0; }
		public static bool operator !=(BigRational left, BigRational right) { return Compare(left, right) != 0; }
		public static bool operator <(BigRational left, BigRational right) { return Compare(left, right) < 0; }
		public static bool operator <=(BigRational left, BigRational right) { return Compare(left, right) <= 0; }
		public static bool operator >(BigRational left, BigRational right) { return Compare(left, right) > 0; }
		public static bool operator >=(BigRational left, BigRational right) { return Compare(left, right) >= 0; }

		// IComparable
		int IComparable.CompareTo(Object obj)
		{
			if (obj == null) { return 1; }
			if (!(obj is BigRational)) { throw new ArgumentException($"Argument must be of type {nameof(BigRational)}", nameof(obj)); }
			return Compare(this, (BigRational)obj);
		}

		// IComparable<Fraction>
		public int CompareTo(BigRational other)
		{
			return Compare(this, other);
		}

		public static int Compare(BigRational left, BigRational right)
		{
			BigRational leftRed = BigRational.Expand(left);
			BigRational rightRed = BigRational.Expand(right);

			return Fraction.Compare(leftRed.FractionalPart, rightRed.FractionalPart);
		}


		#endregion

		#region Equality Methods

		public Boolean Equals(BigRational other)
		{
			Fraction fracExpandedThis = BigRational.Expand(this).FractionalPart;
			Fraction fracExpandedOther = BigRational.Expand(other).FractionalPart;
			return fracExpandedThis.Equals(fracExpandedOther);
		}

		public override bool Equals(Object obj)
		{
			if (obj == null) { return false; }
			if (!(obj is BigRational)) { return false; }
			return this.Equals((BigRational)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				return WholePart.GetHashCode() * FractionalPart.GetHashCode();
			}
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
			BigRational result = Fraction.ReduceToProperFraction(input.FractionalPart);
			return result;
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
