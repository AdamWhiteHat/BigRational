using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ExtendedNumerics
{
	public class Fraction : IComparable, IComparable<Fraction>, IEquatable<Fraction>
	{
		#region Constructors

		public Fraction()
			: this(new BigInteger(0), new BigInteger(1))
		{
		}

		public Fraction(Fraction fraction)
			: this(fraction.Numerator, fraction.Denominator)
		{
		}

		public Fraction(BigInteger value)
			: this(value, BigInteger.One)
		{
		}

		public Fraction(BigInteger numerator, BigInteger denominator)
		{
			Numerator = numerator;
			Denominator = denominator;
		}

		public Fraction(Double value)
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
				Numerator = BigInteger.Zero;
			}
			else if (value == 1)
			{
				Numerator = BigInteger.One;
			}
			else if (value == -1)
			{
				Numerator = BigInteger.MinusOne;
			}
			else if (value % 1 == 0)
			{
				Numerator = (BigInteger)value;
			}
			else
			{
				Double exponent = value.ToString(CultureInfo.InvariantCulture)
										.TrimEnd('0')
										.SkipWhile(c => c != '.').Skip(1)
										.Count();
				if (exponent > 0)
				{
					Double numerator = value * Math.Pow(10d, exponent);
					Numerator = (BigInteger)numerator;
					Denominator = BigInteger.Multiply(Denominator, BigInteger.Pow(10, (int)exponent));

				}
				else
				{
					Numerator = (BigInteger)value;
				}
			}
		}

		#endregion

		#region Properties

		public BigInteger Numerator { get; private set; }
		public BigInteger Denominator { get; private set; }

		public Int32 Sign { get { return Fraction.Simplify(this).Numerator.Sign; } }

		#region Static Properties

		public static Fraction Zero { get { return _zero; } }
		public static Fraction One { get { return _one; } }
		public static Fraction MinusOne { get { return _minusOne; } }

		private static readonly Fraction _zero = new Fraction(BigInteger.Zero, BigInteger.One);
		private static readonly Fraction _one = new Fraction(BigInteger.One, BigInteger.One);
		private static readonly Fraction _minusOne = new Fraction(BigInteger.MinusOne, BigInteger.One);

		#endregion

		#endregion

		#region Arithmetic Methods

		public static Fraction Add(Fraction augend, Fraction addend)
		{
			Fraction result;

			if (augend.Denominator == addend.Denominator)
			{
				result = new Fraction(augend.Numerator + addend.Numerator, augend.Denominator);
			}
			else
			{
				result = new Fraction();
				result.Denominator = LCM(augend.Denominator, addend.Denominator);

				BigInteger augendNumerator = result.Denominator / augend.Denominator;
				BigInteger addendNumerator = result.Denominator / addend.Denominator;

				result.Numerator = augendNumerator + addendNumerator;
			}

			return Fraction.Simplify(result);
		}

		public static Fraction Subtract(Fraction minuend, Fraction subtrahend)
		{
			Fraction result;

			if (minuend.Denominator == subtrahend.Denominator)
			{
				result = new Fraction(minuend.Numerator - subtrahend.Numerator, minuend.Denominator);
			}
			else
			{
				result = new Fraction();
				result.Denominator = LCM(minuend.Denominator, subtrahend.Denominator);

				BigInteger minuendNumerator = result.Denominator / minuend.Denominator;
				BigInteger subtrahendNumerator = result.Denominator / subtrahend.Denominator;

				result.Numerator = minuendNumerator - subtrahendNumerator;
			}

			return Fraction.Simplify(result);
		}

		public static Fraction Multiply(Fraction multiplicand, Fraction multiplier)
		{
			Fraction result = new Fraction(
					BigInteger.Multiply(multiplicand.Numerator, multiplier.Numerator),
					BigInteger.Multiply(multiplicand.Denominator, multiplier.Denominator)
				);

			return Fraction.Simplify(result);
		}

		public static Fraction Divide(Fraction dividend, Fraction divisor)
		{
			Fraction inverseDivisor = Reciprocal(divisor);
			return Multiply(dividend, inverseDivisor);
		}

		public static Fraction Remainder(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = dividend % divisor;
			return new Fraction(remainder, divisor);
		}

		public static Fraction Remainder(Fraction dividend, Fraction divisor)
		{
			return new Fraction(
				BigInteger.Multiply(dividend.Numerator, divisor.Denominator) % BigInteger.Multiply(dividend.Denominator, divisor.Numerator),
				BigInteger.Multiply(dividend.Denominator, divisor.Denominator)
			);
		}

		public static Fraction DivRem(Fraction dividend, Fraction divisor, out Fraction remainder)
		{
			// a/b / c/d  == (ad)/(bc) ; a/b % c/d  == (ad % bc)/bd
			BigInteger ad = dividend.Numerator * divisor.Denominator;
			BigInteger bc = dividend.Denominator * divisor.Numerator;
			BigInteger bd = dividend.Denominator * divisor.Denominator;
			remainder = new Fraction((ad % bc), bd);
			return new Fraction(ad, bc);
		}

		public static BigInteger DivRem(BigInteger dividend, BigInteger divisor, out Fraction remainder)
		{
			BigInteger remaind = new BigInteger(-1);
			BigInteger quotient = BigInteger.DivRem(dividend, divisor, out remaind);

			remainder = new Fraction(remaind, divisor);
			return quotient;
		}

		public static Fraction Pow(Fraction value, BigInteger exponent)
		{
			if (exponent.Sign == 0)
			{
				return Fraction.One;
			}

			Fraction inputValue;
			BigInteger inputExponent;

			if (exponent.Sign < 0)
			{
				if (value == Fraction.Zero)
				{
					throw new ArgumentException("Cannot raise zero to a negative power", nameof(value));
				}
				// n^(-e) -> (1/n)^e
				inputValue = Reciprocal(value);
				inputExponent = BigInteger.Negate(exponent);
			}
			else
			{
				inputValue = new Fraction(value);
				inputExponent = exponent;
			}

			Fraction result = inputValue;
			while (inputExponent > BigInteger.One)
			{
				result = Multiply(result, inputValue);
				inputExponent--;
			}

			return result;
		}

		public static Fraction Reciprocal(Fraction fraction)
		{
			Fraction result = new Fraction(fraction.Denominator, fraction.Numerator);
			return Fraction.Simplify(result);
		}

		public static Fraction Abs(Fraction fraction)
		{
			return (fraction.Numerator.Sign < 0 ? new Fraction(BigInteger.Abs(fraction.Numerator), fraction.Denominator) : fraction);
		}

		public static Fraction Negate(Fraction fraction)
		{
			return new Fraction(BigInteger.Negate(fraction.Numerator), fraction.Denominator);
		}

		#endregion

		#region Arithmetic Operators

		public static Fraction operator +(Fraction fraction) { return Abs(fraction); }
		public static Fraction operator -(Fraction fraction) { return Negate(fraction); }
		public static Fraction operator ++(Fraction fraction) { return Add(fraction, Fraction.One); }
		public static Fraction operator --(Fraction fraction) { return Subtract(fraction, Fraction.One); }
		public static Fraction operator +(Fraction left, Fraction right) { return Add(left, right); }
		public static Fraction operator -(Fraction left, Fraction right) { return Subtract(left, right); }
		public static Fraction operator *(Fraction left, Fraction right) { return Multiply(left, right); }
		public static Fraction operator /(Fraction left, Fraction right) { return Divide(left, right); }
		public static Fraction operator %(Fraction left, Fraction right) { return Remainder(left, right); }

		#endregion

		#region Comparison Operators

		public static bool operator ==(Fraction left, Fraction right) { return Compare(left, right) == 0; }
		public static bool operator !=(Fraction left, Fraction right) { return Compare(left, right) != 0; }
		public static bool operator <(Fraction left, Fraction right) { return Compare(left, right) < 0; }
		public static bool operator <=(Fraction left, Fraction right) { return Compare(left, right) <= 0; }
		public static bool operator >(Fraction left, Fraction right) { return Compare(left, right) > 0; }
		public static bool operator >=(Fraction left, Fraction right) { return Compare(left, right) >= 0; }

		public static int Compare(Fraction left, Fraction right)
		{
			return BigInteger.Compare(
					BigInteger.Multiply(left.Numerator, right.Denominator),
					BigInteger.Multiply(right.Numerator, left.Denominator)
				);
		}

		// IComparable
		int IComparable.CompareTo(Object obj)
		{
			if (obj == null) { return 1; }
			if (!(obj is Fraction)) { throw new ArgumentException("Argument must be of type Fraction", "obj"); }
			return Compare(this, (Fraction)obj);
		}

		// IComparable<Fraction>
		public int CompareTo(Fraction other)
		{
			return Compare(this, other);
		}

		#endregion

		#region Equality Methods

		public Boolean Equals(Fraction other)
		{
			if (this.Denominator == other.Denominator) { return Numerator == other.Numerator; }
			else { return (Numerator * other.Denominator) == (Denominator * other.Numerator); }
		}

		public override bool Equals(Object obj)
		{
			if (obj == null) { return false; }
			if (!(obj is Fraction)) { return false; }
			return this.Equals((Fraction)obj);
		}

		public override int GetHashCode()
		{
			return (Numerator / Denominator).GetHashCode();
		}

		#endregion

		#region Conversion Operators

		public static explicit operator BigRational(Fraction value)
		{
			return new BigRational(BigInteger.Zero, value);
		}

		public static explicit operator Fraction(Double value)
		{
			return new Fraction(value);
		}

		public static explicit operator Double(Fraction value)
		{
			if (IsInRangeDouble(value.Numerator) && IsInRangeDouble(value.Denominator))
			{
				return (Double)value.Numerator / (Double)value.Denominator;
			}

			BigInteger scaledup = BigInteger.Multiply(value.Numerator, _doublePrecision) / value.Denominator;
			if (scaledup.IsZero)
			{
				return 0d; // underflow. throw exception here instead?
			}

			bool isDone = false;
			Double result = 0;
			int scale = _doubleMaxScale;
			while (scale > 0)
			{
				if (!isDone)
				{
					if (IsInRangeDouble(scaledup))
					{
						result = (Double)scaledup;
						isDone = true;
					}
					else
					{
						scaledup = scaledup / 10;
					}
				}

				result = result / 10;
				scale--;
			}

			if (isDone)
			{
				return result;
			}
			else
			{
				return (value.Sign < 0) ? Double.NegativeInfinity : Double.PositiveInfinity;
			}
		}

		private static bool IsInRangeDouble(BigInteger number)
		{
			return ((BigInteger)Double.MinValue < number && number < (BigInteger)Double.MaxValue);
		}
		private static readonly int _doubleMaxScale = 308;
		private static readonly BigInteger _doublePrecision = BigInteger.Pow(10, _doubleMaxScale);

		#endregion

		#region Instance Methods

		public static BigRational ReduceToProperFraction(Fraction value)
		{
			Fraction input = Fraction.Simplify(value);

			if (input.Numerator > input.Denominator)
			{
				BigInteger remainder = new BigInteger(-1);
				BigInteger wholeUnits = BigInteger.DivRem(input.Numerator, input.Denominator, out remainder);
				return new BigRational(wholeUnits, new Fraction(remainder, input.Denominator));
			}

			return new BigRational(BigInteger.Zero, input.Numerator, input.Denominator);
		}

		public static Fraction Simplify(Fraction value)
		{
			if (value.Numerator.IsZero || value.Numerator.IsOne || value.Numerator == BigInteger.MinusOne)
			{
				return new Fraction(value);
			}

			BigInteger num = value.Numerator;
			BigInteger denom = value.Denominator;
			BigInteger gcd = GCD(num, denom);
			if (gcd > BigInteger.One)
			{
				return new Fraction(num / gcd, denom / gcd);
			}

			if (denom.Sign < 0)
			{
				return new Fraction(BigInteger.Negate(num), BigInteger.Negate(denom));
			}

			return new Fraction(value);
		}

		public override string ToString()
		{
			return $"{Numerator} / {Denominator}";
		}

		#endregion

		#region LCM & GCD

		private static BigInteger LCM(BigInteger value1, BigInteger value2)
		{
			BigInteger absValue1 = BigInteger.Abs(value1);
			BigInteger absValue2 = BigInteger.Abs(value2);
			return (absValue1 * absValue2) / GCD(absValue1, absValue2);
		}

		private static BigInteger GCD(BigInteger value1, BigInteger value2)
		{
			BigInteger absValue1 = BigInteger.Abs(value1);
			BigInteger absValue2 = BigInteger.Abs(value2);

			while (absValue1 != 0 && absValue2 != 0)
			{
				if (absValue1 > absValue2)
				{
					absValue1 %= absValue2;
				}
				else
				{
					absValue2 %= absValue1;
				}
			}

			return BigInteger.Max(absValue1, absValue2);
		}

		#endregion
	}
}
