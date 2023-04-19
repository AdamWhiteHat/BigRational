﻿using System;
using System.Linq;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;

namespace ExtendedNumerics
{
	public class BigRational : IComparable, IComparable<BigRational>, IEquatable<BigRational>
	{
		#region Constructors

		public BigRational()
			: this(BigInteger.Zero, Fraction.Zero)
		{
		}

		public BigRational(int value)
			: this((BigInteger)value, Fraction.Zero)
		{
		}

		public BigRational(BigInteger value)
			: this(value, Fraction.Zero)
		{
		}

		public BigRational(Fraction fraction)
			: this(BigInteger.Zero, fraction)
		{
		}

		public BigRational(BigInteger whole, Fraction fraction)
			: this(whole, fraction.Numerator, fraction.Denominator)
		{
		}

		public BigRational(BigInteger numerator, BigInteger denominator)
			: this(new Fraction(numerator, denominator))
		{
		}

		public BigRational(BigInteger whole, BigInteger numerator, BigInteger denominator)
		{
			WholePart = whole;
			FractionalPart = new Fraction(numerator, denominator);
			NormalizeSign();
		}

		public BigRational(float value)
		{
			if (!CheckForWholeValues(value))
			{
				WholePart = (BigInteger)Math.Truncate(value);
				float fract = Math.Abs(value) % 1;
				FractionalPart = (fract == 0) ? Fraction.Zero : new Fraction(fract);
				NormalizeSign();
			}
		}

		public BigRational(double value)
		{
			if (!CheckForWholeValues(value))
			{
				WholePart = (BigInteger)Math.Truncate(value);
				double fract = Math.Abs(value) % 1;
				FractionalPart = (fract == 0) ? Fraction.Zero : new Fraction(fract);
				NormalizeSign();
			}
		}

		public BigRational(decimal value)
		{
			if (!CheckForWholeValues((double)value))
			{
				WholePart = (BigInteger)Math.Truncate(value);
				decimal fract = Math.Abs(value) % 1;
				FractionalPart = (fract == 0) ? Fraction.Zero : new Fraction(fract);
				NormalizeSign();
			}
		}

		static BigRational()
		{
			One = new BigRational(BigInteger.One);
			Zero = new BigRational(BigInteger.Zero);
			MinusOne = new BigRational(BigInteger.MinusOne);
		}

		private bool CheckForWholeValues(double value)
		{
			if (double.IsNaN(value))
			{
				throw new ArgumentException("Value is not a number", nameof(value));
			}
			if (double.IsInfinity(value))
			{
				throw new ArgumentException("Cannot represent infinity", nameof(value));
			}

			if (value == 0)
			{
				WholePart = BigInteger.Zero;
				FractionalPart = Fraction.Zero;
				return true;
			}
			else if (value == 1)
			{
				WholePart = BigInteger.Zero;
				FractionalPart = Fraction.One;
				return true;
			}
			else if (value == -1)
			{
				WholePart = BigInteger.Zero;
				FractionalPart = Fraction.MinusOne;
				return true;
			}
			return false;
		}


		#endregion

		#region Properties

		public BigInteger WholePart { get; private set; }
		public Fraction FractionalPart { get; private set; }

		public int Sign { get { return NormalizeSign(this).WholePart.Sign; } }
		public bool IsZero { get { return (WholePart.IsZero && FractionalPart.IsZero); } }

		#region Static Properties

		public static BigRational One = null;
		public static BigRational Zero = null;
		public static BigRational MinusOne = null;

		#endregion

		#endregion

		#region Arithmetic Methods

		public static BigRational Add(BigRational augend, BigRational addend)
		{
			Fraction fracAugend = augend.GetImproperFraction();
			Fraction fracAddend = addend.GetImproperFraction();

			BigRational result = Add(fracAugend, fracAddend);
			BigRational reduced = BigRational.Reduce(result);
			return reduced;
		}

		public static BigRational Subtract(BigRational minuend, BigRational subtrahend)
		{
			Fraction fracMinuend = minuend.GetImproperFraction();
			Fraction fracSubtrahend = subtrahend.GetImproperFraction();

			BigRational result = Subtract(fracMinuend, fracSubtrahend);
			BigRational reduced = BigRational.Reduce(result);
			return reduced;
		}

		public static BigRational Multiply(BigRational multiplicand, BigRational multiplier)
		{
			Fraction fracMultiplicand = multiplicand.GetImproperFraction();
			Fraction fracMultiplier = multiplier.GetImproperFraction();

			BigRational result = Fraction.ReduceToProperFraction(Fraction.Multiply(fracMultiplicand, fracMultiplier));
			BigRational reduced = BigRational.Reduce(result);
			return reduced;
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
			// a/b / c/d  == (ad)/(bc)			
			Fraction l = dividend.GetImproperFraction();
			Fraction r = divisor.GetImproperFraction();

			BigInteger ad = BigInteger.Multiply(l.Numerator, r.Denominator);
			BigInteger bc = BigInteger.Multiply(l.Denominator, r.Numerator);

			Fraction newFraction = new Fraction(ad, bc);
			BigRational result = Fraction.ReduceToProperFraction(newFraction);
			return result;
		}

		public static BigRational Remainder(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = (dividend % divisor);
			return new BigRational(BigInteger.Zero, new Fraction(remainder, divisor));
		}

		public static BigRational Mod(BigRational number, BigRational mod)
		{
			Fraction num = number.GetImproperFraction();
			Fraction modulus = mod.GetImproperFraction();

			return new BigRational(Fraction.Remainder(num, modulus));
		}

		public static BigRational Pow(BigRational baseValue, BigInteger exponent)
		{
			Fraction fractPow = Fraction.Pow(baseValue.GetImproperFraction(), exponent);
			return new BigRational(fractPow);
		}

		public static BigRational Sqrt(BigRational value)
		{
			Fraction input = value.GetImproperFraction();
			Fraction result = Fraction.Sqrt(input);
			return Fraction.ReduceToProperFraction(result);
		}

		public static BigRational NthRoot(BigRational value, int root)
		{
			Fraction input = value.GetImproperFraction();
			Fraction result = Fraction.NthRoot(input, root);
			return Fraction.ReduceToProperFraction(result);
		}

		public static double Log(BigRational rational)
		{
			return Fraction.Log(rational.GetImproperFraction());
		}

		public static BigRational Abs(BigRational rational)
		{
			BigRational input = BigRational.Reduce(rational);
			return new BigRational(BigInteger.Abs(input.WholePart), input.FractionalPart);
		}

		public static BigRational Negate(BigRational rational)
		{
			BigRational input = BigRational.Reduce(rational);
			return new BigRational(BigInteger.Negate(input.WholePart), input.FractionalPart);
		}

		public static BigRational Add(Fraction augend, Fraction addend)
		{
			return new BigRational(BigInteger.Zero, Fraction.Add(augend, addend));
		}

		public static BigRational Subtract(Fraction minuend, Fraction subtrahend)
		{
			return new BigRational(BigInteger.Zero, Fraction.Subtract(minuend, subtrahend));
		}

		public static BigRational Multiply(Fraction multiplicand, Fraction multiplier)
		{
			return new BigRational(BigInteger.Zero, Fraction.Multiply(multiplicand, multiplier));
		}

		public static BigRational Divide(Fraction dividend, Fraction divisor)
		{
			return new BigRational(BigInteger.Zero, Fraction.Divide(dividend, divisor));
		}

		#region GCD & LCM

		public static BigRational LeastCommonDenominator(BigRational left, BigRational right)
		{
			Fraction leftFrac = left.GetImproperFraction();
			Fraction rightFrac = right.GetImproperFraction();

			return BigRational.Reduce(new BigRational(Fraction.LeastCommonDenominator(leftFrac, rightFrac)));
		}

		public static BigRational GreatestCommonDivisor(BigRational left, BigRational right)
		{
			Fraction leftFrac = left.GetImproperFraction();
			Fraction rightFrac = right.GetImproperFraction();

			return BigRational.Reduce(new BigRational(Fraction.GreatestCommonDivisor(leftFrac, rightFrac)));
		}

		#endregion

		#endregion

		#region Arithmetic Operators

		public static BigRational operator +(BigRational augend, BigRational addend) => Add(augend, addend);
		public static BigRational operator -(BigRational minuend, BigRational subtrahend) => Subtract(minuend, subtrahend);
		public static BigRational operator *(BigRational multiplicand, BigRational multiplier) => Multiply(multiplicand, multiplier);
		public static BigRational operator /(BigRational dividend, BigRational divisor) => Divide(dividend, divisor);
		public static BigRational operator %(BigRational dividend, BigRational divisor) => Mod(dividend, divisor);
		// Unitary operators
		public static BigRational operator +(BigRational rational) => Abs(rational);
		public static BigRational operator -(BigRational rational) => Negate(rational);
		public static BigRational operator ++(BigRational rational) => Add(rational, BigRational.One);
		public static BigRational operator --(BigRational rational) => Subtract(rational, BigRational.One);

		#endregion

		#region Comparison Operators

		public static bool operator ==(BigRational left, BigRational right) { return Compare(left, right) == 0; }
		public static bool operator !=(BigRational left, BigRational right) { return Compare(left, right) != 0; }
		public static bool operator <(BigRational left, BigRational right) { return Compare(left, right) < 0; }
		public static bool operator <=(BigRational left, BigRational right) { return Compare(left, right) <= 0; }
		public static bool operator >(BigRational left, BigRational right) { return Compare(left, right) > 0; }
		public static bool operator >=(BigRational left, BigRational right) { return Compare(left, right) >= 0; }

		#endregion

		#region Compare

		public static int Compare(BigRational left, BigRational right)
		{
			BigRational leftRed = BigRational.Reduce(left);
			BigRational rightRed = BigRational.Reduce(right);

			if (leftRed.WholePart == rightRed.WholePart)
			{
				Fraction leftFrac = (leftRed.Sign == -1) ? Fraction.Negate(leftRed.FractionalPart) : leftRed.FractionalPart;
				Fraction rightFrac = (rightRed.Sign == -1) ? Fraction.Negate(rightRed.FractionalPart) : rightRed.FractionalPart;
				return Fraction.Compare(leftFrac, rightFrac);
			}
			else
			{
				return BigInteger.Compare(leftRed.WholePart, rightRed.WholePart);
			}
		}

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

		#endregion

		#region Conversion

		public static implicit operator BigRational(byte value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(SByte value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(Int16 value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(UInt16 value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(Int32 value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(UInt32 value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(Int64 value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(UInt64 value)
		{
			return new BigRational((BigInteger)value);
		}

		public static implicit operator BigRational(BigInteger value)
		{
			return new BigRational(value);
		}

		/*
		public static explicit operator BigInteger(BigRational value)
		{
			if (value.FractionalPart != Fraction.Zero)
			{
				throw new InvalidCastException("Value is not an integer value. This conversion would lost precision.");
			}
			return value.WholePart;
		}
		*/

		public static explicit operator BigRational(float value)
		{
			return new BigRational(value);
		}

		public static explicit operator BigRational(double value)
		{
			return new BigRational(value);
		}

		public static explicit operator BigRational(decimal value)
		{
			return new BigRational(value);
		}

		public static explicit operator double(BigRational value)
		{
			double fract = (double)value.FractionalPart;
			double whole = (double)value.WholePart;
			double result = whole + (fract * (value.Sign == 0 ? 1 : value.Sign));
			return result;
		}

		public static explicit operator decimal(BigRational value)
		{
			decimal fract = (decimal)value.FractionalPart;
			decimal whole = (decimal)value.WholePart;
			decimal result = whole + (fract * (value.Sign == 0 ? 1 : value.Sign));
			return result;
		}

		public static implicit operator Fraction(BigRational value)
		{
			return Fraction.Simplify(new Fraction(
					BigInteger.Add(value.FractionalPart.Numerator, BigInteger.Multiply(value.WholePart, value.FractionalPart.Denominator)),
					value.FractionalPart.Denominator
				));
		}

		public static BigRational Parse(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("Argument cannot be null, empty or whitespace.");
			}

			string[] parts = value.Trim().Split('/');
			if (parts.Length == 1)
			{
				BigInteger whole;
				if (!BigInteger.TryParse(parts[0], out whole))
				{
					throw new ArgumentException("Invalid string given for number.");
				}
				return new BigRational(whole);
			}
			else if (parts.Length == 2)
			{
				BigInteger whole = BigInteger.Zero, numerator, denominator;

				string[] firstParts = parts[0].Trim().Split(new char[] { '+', ' ' }, StringSplitOptions.RemoveEmptyEntries);
				if (firstParts.Length == 1)
				{
					if (!BigInteger.TryParse(parts[0].Trim(), out numerator))
					{
						throw new ArgumentException("Invalid string given for numerator.");
					}
				}
				else if (firstParts.Length == 2)
				{
					if (!BigInteger.TryParse(firstParts[0].Trim(), out whole))
					{
						throw new ArgumentException("Invalid string given for whole number.");
					}
					if (!BigInteger.TryParse(firstParts[1].Trim(), out numerator))
					{
						throw new ArgumentException("Invalid string given for numerator.");
					}
				}
				else
				{
					throw new ArgumentException("Invalid fraction given as string to parse.");
				}

				if (!BigInteger.TryParse(parts[1].Trim(), out denominator))
				{
					throw new ArgumentException("Invalid string given for denominator.");
				}
				return new BigRational(whole, numerator, denominator);
			}
			else
			{
				throw new ArgumentException("Invalid fraction given as string to parse.");
			}
		}

		#endregion

		#region Equality Methods

		public bool Equals(BigRational other)
		{
			BigRational reducedThis = BigRational.Reduce(this);
			BigRational reducedOther = BigRational.Reduce(other);

			bool result = true;

			result &= reducedThis.WholePart.Equals(reducedOther.WholePart);
			result &= reducedThis.FractionalPart.Numerator.Equals(reducedOther.FractionalPart.Numerator);
			result &= reducedThis.FractionalPart.Denominator.Equals(reducedOther.FractionalPart.Denominator);

			return result;
		}

		public override bool Equals(Object obj)
		{
			if (obj == null) { return false; }
			if (!(obj is BigRational)) { return false; }
			return Equals((BigRational)obj);
		}

		public override int GetHashCode()
		{
			return CombineHashCodes(WholePart.GetHashCode(), FractionalPart.GetHashCode());
		}

		internal static int CombineHashCodes(int h1, int h2)
		{
			return (((h1 << 5) + h1) ^ h2);
		}

		#endregion

		#region Transform Methods

		public Fraction GetImproperFraction()
		{
			BigRational input = NormalizeSign(this);

			if (input.WholePart == 0 && input.FractionalPart.Sign == 0)
			{
				return Fraction.Zero;
			}

			if (input.FractionalPart.Sign != 0 || input.FractionalPart.Denominator > 1)
			{
				if (input.WholePart.Sign != 0)
				{
					BigInteger whole = BigInteger.Multiply(input.WholePart, input.FractionalPart.Denominator);

					BigInteger remainder = input.FractionalPart.Numerator;

					if (input.WholePart.Sign == -1)
					{
						remainder = BigInteger.Negate(remainder);
					}

					BigInteger total = BigInteger.Add(whole, remainder);
					Fraction newFractional = new Fraction(total, input.FractionalPart.Denominator);
					return newFractional;
				}
				else
				{
					return input.FractionalPart;
				}
			}
			else
			{
				return new Fraction(input.WholePart, BigInteger.One);
			}
		}

		public static BigRational Reduce(BigRational value)
		{
			BigRational input = NormalizeSign(value);
			BigRational reduced = Fraction.ReduceToProperFraction(input.FractionalPart);
			BigRational result = new BigRational(value.WholePart + reduced.WholePart, reduced.FractionalPart);
			return result;
		}

		public static BigRational NormalizeSign(BigRational value)
		{
			return value.NormalizeSign();
		}

		internal BigRational NormalizeSign()
		{
			FractionalPart = Fraction.NormalizeSign(FractionalPart);
			if (WholePart > 0 && WholePart.Sign == 1 && FractionalPart.Sign == -1)
			{
				WholePart = BigInteger.Negate(WholePart);
				FractionalPart = Fraction.Negate(FractionalPart);
			}
			return this;
		}

		#endregion

		#region Overrides

		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		public String ToString(String format)
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		public String ToString(IFormatProvider provider)
		{
			return ToString("R", provider);
		}

		public String ToString(String format, IFormatProvider provider)
		{
			NumberFormatInfo numberFormatProvider = (NumberFormatInfo)provider.GetFormat(typeof(NumberFormatInfo));
			if (numberFormatProvider == null)
			{
				numberFormatProvider = CultureInfo.CurrentCulture.NumberFormat;
			}

			string zeroString = numberFormatProvider.NativeDigits[0];

			BigRational input = BigRational.Reduce(this);

			string whole = input.WholePart != 0 ? String.Format(provider, "{0}", input.WholePart.ToString(format, provider)) : string.Empty;
			string fractional = input.FractionalPart.Numerator != 0 ? String.Format(provider, "{0}", input.FractionalPart.ToString(format, provider)) : string.Empty;
			string join = string.Empty;

			if (!string.IsNullOrWhiteSpace(whole) && !string.IsNullOrWhiteSpace(fractional))
			{
				if (input.WholePart.Sign < 0)
				{
					join = $" {numberFormatProvider.NegativeSign} ";
				}
				else
				{
					join = $" {numberFormatProvider.PositiveSign} ";
				}
			}

			if (string.IsNullOrWhiteSpace(whole) && string.IsNullOrWhiteSpace(join) && string.IsNullOrWhiteSpace(fractional))
			{
				return zeroString;
			}

			return string.Concat(whole, join, fractional);
		}

		#endregion
	}
}

