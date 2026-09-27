using System;
using System.Linq;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;

namespace ExtendedNumerics
{
	/// <summary>
	/// Represents an arbitrarily large mixed fraction.
	/// If you want an arbitrarily large rational number, <see cref="BigRational" />.
	/// Implements the <see cref="IComparable" />
	/// Implements the <see cref="IComparable{BigRational}" />
	/// Implements the <see cref="IEquatable{BigRational}" />
	/// </summary>
	/// <seealso cref="IComparable" />
	/// <seealso cref="IComparable{BigRational}" />
	/// <seealso cref="IEquatable{BigRational}" />
	public struct MixedFraction : IComparable, IComparable<MixedFraction>, IEquatable<MixedFraction>
	{

		#region Properties

		/// <summary>The whole-number (non-fractional) integer value.</summary>
		public BigInteger WholePart { get; private set; }

		/// <summary>The fractional part of the value.</summary>		
		public BigRational FractionalPart { get; private set; }

		/// <summary>
		/// Gets the sign of the number.
		/// Returns a positive one (1) if the value is positive,
		/// a negative one (-1) if the value is negative,
		/// and zero (0) if the value is zero.
		/// </summary>		
		public int Sign { get { NormalizeSign(); return (WholePart != 0) ? WholePart.Sign : FractionalPart.Sign; } }

		/// <summary>Indicates whether the value of the current instance is zero (0).</summary>
		/// <value><c>true</c> if this instance is zero; otherwise, <c>false</c>.</value>
		public bool IsZero { get { return (WholePart.IsZero && FractionalPart.IsZero); } }

		#region Static Properties

		/// <summary>Gets a value that represents the number one (1).</summary>
		public static MixedFraction One = new MixedFraction(BigInteger.One);

		/// <summary>Gets a value that represents the number zero (0).</summary>
		public static MixedFraction Zero = new MixedFraction(BigInteger.Zero);

		/// <summary>Gets a value that represents the number negative one (-1).</summary>
		public static MixedFraction MinusOne = new MixedFraction(BigInteger.MinusOne);

		#endregion

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using a 32-bit signed integer value.
		/// </summary>
		/// <param name="value">A 32-bit signed integer.</param>
		public MixedFraction(int value)
			: this((BigInteger)value, BigRational.Zero)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		///  an arbitrarily large signed integer.
		/// </summary>
		/// <param name="value">An arbitrarily large signed integer.</param>
		public MixedFraction(BigInteger value)
			: this(value, BigRational.Zero)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		/// an arbitrarily large rational number.
		/// </summary>
		/// <param name="fraction">An arbitrarily large rational number (as a Fraction).</param>
		public MixedFraction(BigRational fraction)
			: this(BigInteger.Zero, fraction)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		/// an arbitrarily large signed integer and an arbitrarily large rational number.
		/// </summary>
		/// <param name="whole">An arbitrarily large signed integer whole number.</param>
		/// <param name="fraction">An arbitrarily large rational number (as a Fraction).</param>
		public MixedFraction(BigInteger whole, BigRational fraction)
			: this(whole, fraction.Numerator, fraction.Denominator)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		///  an arbitrarily large signed integer numerator and denominator.
		/// </summary>
		/// <param name="numerator">An arbitrarily large signed integer numerator.</param>
		/// <param name="denominator">An arbitrarily large signed integer denominator.</param>
		public MixedFraction(BigInteger numerator, BigInteger denominator)
			: this(new BigRational(numerator, denominator))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		///  an arbitrarily large signed integer whole number value,
		///  a numerator and a denominator.
		/// </summary>
		/// <param name="whole">An arbitrarily large signed integer whole number.</param>
		/// <param name="numerator">An arbitrarily large signed integer numerator.</param>
		/// <param name="denominator">An arbitrarily large signed integer denominator.</param>
		public MixedFraction(BigInteger whole, BigInteger numerator, BigInteger denominator)
		{
			WholePart = whole;
			FractionalPart = new BigRational(numerator, denominator);
			NormalizeSign();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		/// a single-precision floating-point value.
		/// </summary>
		/// <param name="value">A single-precision floating-point value.</param>
		public MixedFraction(float value)
		{
			Tuple<BigInteger, BigRational> result = CheckForWholeValues((double)value);
			if (result != null)
			{
				WholePart = result.Item1;
				FractionalPart = result.Item2;
			}
			else
			{
				WholePart = (BigInteger)Math.Truncate(value);
				float fract = Math.Abs(value) % 1;
				FractionalPart = (fract == 0) ? BigRational.Zero : new BigRational(fract);
				NormalizeSign();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		/// A double-precision floating-point value.
		/// </summary>
		/// <param name="value">A double-precision floating-point value.</param>
		public MixedFraction(double value)
		{
			Tuple<BigInteger, BigRational> result = CheckForWholeValues(value);
			if (result != null)
			{
				WholePart = result.Item1;
				FractionalPart = result.Item2;
			}
			else
			{
				WholePart = (BigInteger)Math.Truncate(value);
				double fract = Math.Abs(value) % 1;
				FractionalPart = (fract == 0) ? BigRational.Zero : new BigRational(fract);
				NormalizeSign();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ExtendedNumerics.MixedFraction"/> class using
		/// a 128-bit base-10 floating point decimal number.
		/// </summary>
		/// <param name="value">A 128-bit base-10 floating point decimal number.</param>
		public MixedFraction(decimal value)
		{
			Tuple<BigInteger, BigRational> result = CheckForWholeValues((double)value);
			if (result != null)
			{
				WholePart = result.Item1;
				FractionalPart = result.Item2;
			}
			else
			{
				WholePart = (BigInteger)Math.Truncate(value);
				decimal fract = Math.Abs(value) % 1;
				FractionalPart = (fract == 0) ? BigRational.Zero : new BigRational(fract);
				NormalizeSign();
			}
		}

		/// <summary>
		/// Checks the value of a <see cref="Double"/> for 0, 1 or -1,
		/// setting the internal state and returning true if it is,
		/// throws an exception if it is NaN or +- Infinity,
		/// and returns false otherwise.
		/// </summary>
		/// <exception cref="System.ArgumentException">Value is not a number - value</exception>
		/// <exception cref="System.ArgumentException">Cannot represent infinity - value</exception>
		private static Tuple<BigInteger, BigRational> CheckForWholeValues(double value)
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
				return new Tuple<BigInteger, BigRational>(BigInteger.Zero, BigRational.Zero);
			}
			else if (value == 1)
			{
				return new Tuple<BigInteger, BigRational>(BigInteger.One, BigRational.Zero);
			}
			else if (value == -1)
			{
				return new Tuple<BigInteger, BigRational>(BigInteger.MinusOne, BigRational.Zero);
			}
			return null;
		}

		#endregion

		#region Arithmetic Methods

		/// <summary>
		/// Adds two <see cref="ExtendedNumerics.MixedFraction"/> values and returns the sum.
		/// </summary>
		/// <param name="augend">The augend.</param>
		/// <param name="addend">The addend.</param>
		/// <returns>The sum.</returns>
		public static MixedFraction Add(MixedFraction augend, MixedFraction addend)
		{
			BigRational fracAugend = augend.GetImproperFraction();
			BigRational fracAddend = addend.GetImproperFraction();

			MixedFraction result = Add(fracAugend, fracAddend);
			MixedFraction reduced = MixedFraction.Reduce(result);
			return reduced;
		}

		/// <summary>
		/// Subtracts two <see cref="ExtendedNumerics.MixedFraction"/> values and returns the difference.
		/// </summary>
		/// <param name="minuend">The minuend.</param>
		/// <param name="subtrahend">The subtrahend.</param>
		/// <returns>The difference.</returns>
		public static MixedFraction Subtract(MixedFraction minuend, MixedFraction subtrahend)
		{
			BigRational fracMinuend = minuend.GetImproperFraction();
			BigRational fracSubtrahend = subtrahend.GetImproperFraction();

			MixedFraction result = Subtract(fracMinuend, fracSubtrahend);
			MixedFraction reduced = MixedFraction.Reduce(result);
			return reduced;
		}

		/// <summary>
		/// Multiplies two <see cref="ExtendedNumerics.MixedFraction"/> values and returns the product.
		/// </summary>
		/// <param name="multiplicand">The multiplicand.</param>
		/// <param name="multiplier">The multiplier.</param>
		/// <returns>The product.</returns>
		public static MixedFraction Multiply(MixedFraction multiplicand, MixedFraction multiplier)
		{
			BigRational fracMultiplicand = multiplicand.GetImproperFraction();
			BigRational fracMultiplier = multiplier.GetImproperFraction();

			MixedFraction result = BigRational.ReduceToProperFraction(BigRational.Multiply(fracMultiplicand, fracMultiplier));
			MixedFraction reduced = MixedFraction.Reduce(result);
			return reduced;
		}

		/// <summary>
		/// Divides two <see cref="BigInteger"/> values and returns the quotient.
		/// </summary>
		/// <param name="dividend">The dividend.</param>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The quotient.</returns>
		public static MixedFraction Divide(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = new BigInteger(-1);
			BigInteger quotient = BigInteger.DivRem(dividend, divisor, out remainder);

			MixedFraction result = new MixedFraction(
					quotient,
					new BigRational(remainder, divisor)
				);

			return result;
		}

		/// <summary>
		/// Divides two <see cref="ExtendedNumerics.MixedFraction"/> values and returns the quotient.
		/// </summary>
		/// <param name="dividend">The dividend.</param>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The quotient.</returns>
		public static MixedFraction Divide(MixedFraction dividend, MixedFraction divisor)
		{
			// a/b / c/d  == (ad)/(bc)			
			BigRational l = dividend.GetImproperFraction();
			BigRational r = divisor.GetImproperFraction();

			BigInteger ad = BigInteger.Multiply(l.Numerator, r.Denominator);
			BigInteger bc = BigInteger.Multiply(l.Denominator, r.Numerator);

			BigRational newFraction = new BigRational(ad, bc);
			MixedFraction result = BigRational.ReduceToProperFraction(newFraction);
			return result;
		}

		/// <summary>
		/// Divides two <see cref="ExtendedNumerics.MixedFraction"/> values and returns the remainder.
		/// </summary>
		/// <param name="dividend">The dividend.</param>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The remainder.</returns>
		public static MixedFraction Remainder(BigInteger dividend, BigInteger divisor)
		{
			BigInteger remainder = (dividend % divisor);
			return new MixedFraction(BigInteger.Zero, new BigRational(remainder, divisor));
		}

		/// <summary>
		/// Divides two <see cref="ExtendedNumerics.MixedFraction"/> values and returns the remainder (modulus).
		/// </summary>
		/// <param name="number">The dividend.</param>
		/// <param name="mod">The divisor.</param>
		/// <returns>The remainder (modulus).</returns>
		public static MixedFraction Mod(MixedFraction number, MixedFraction mod)
		{
			BigRational num = number.GetImproperFraction();
			BigRational modulus = mod.GetImproperFraction();

			return new MixedFraction(BigRational.Remainder(num, modulus));
		}

		/// <summary>
		/// Raises the specified <see cref="ExtendedNumerics.MixedFraction"/> base value to the specified exponent.
		/// </summary>
		/// <param name="baseValue">The base value.</param>
		/// <param name="exponent">The exponent.</param>
		/// <returns>The result of raising the base value to the exponent power.</returns>
		public static MixedFraction Pow(MixedFraction baseValue, BigInteger exponent)
		{
			BigRational fractPow = BigRational.Pow(baseValue.GetImproperFraction(), exponent);
			return new MixedFraction(fractPow);
		}

		/// <summary>
		/// Returns the square root of the specified value.
		/// </summary>
		/// <param name="value">The base value to square root.</param>
		/// <returns>The square root of the specified value.</returns>
		public static MixedFraction Sqrt(MixedFraction value)
		{
			BigRational input = value.GetImproperFraction();
			BigRational result = BigRational.Sqrt(input);
			return BigRational.ReduceToProperFraction(result);
		}

		/// <summary>
		/// Returns the Nth root of a number up to a desired precision.
		/// The precision parameter is given in terms of the minimum number of correct decimal places.
		/// </summary>
		/// <param name="value">The value to take the Nth root of.</param>
		/// <param name="root">The Nth root to find of value. Also called the index.</param>
		/// <param name="precision">The minimum number of correct decimal places to return if the answer is not a rational number.</param>
		/// <returns>The Nth root of the specified value.</returns>
		/// <exception cref="System.Exception">Root must be greater than or equal to 1</exception>
		/// <exception cref="System.Exception">Value must be a positive integer</exception>
		public static MixedFraction NthRoot(MixedFraction value, int root, int precision = 30)
		{
			BigRational input = value.GetImproperFraction();
			BigRational result = BigRational.NthRoot(input, root, precision);
			return BigRational.ReduceToProperFraction(result);
		}

		/// <summary>
		/// Returns the natural (base e) logarithm of a specified number.
		/// </summary>
		/// <param name="rational">The number whose logarithm is to be found.</param>
		/// <returns>The natural (base e) logarithm of the specifed value.</returns>
		public static double Log(MixedFraction rational)
		{
			return BigRational.Log(rational.GetImproperFraction());
		}

		/// <summary>
		/// Returns the absolute value of a <see cref="ExtendedNumerics.MixedFraction"/> value.
		/// </summary>
		/// <param name="rational">A value to get the absolute value of.</param>
		/// <returns>The absolute value of value of the specified number.</returns>
		public static MixedFraction Abs(MixedFraction rational)
		{
			MixedFraction input = MixedFraction.Reduce(rational);
			return new MixedFraction(BigInteger.Abs(input.WholePart), input.FractionalPart);
		}

		/// <summary>
		/// Negates the specified value.
		/// </summary>
		/// <param name="rational">The number to negate the value of.</param>
		/// <returns>The result of the specified value multiplied by negative one (-1).</returns>
		public static MixedFraction Negate(MixedFraction rational)
		{
			MixedFraction input = MixedFraction.Reduce(rational);
			if (input.WholePart == 0)
			{
				return new MixedFraction(input.WholePart, BigRational.Negate(input.FractionalPart));
			}
			return new MixedFraction(BigInteger.Negate(input.WholePart), input.FractionalPart);
		}

		/// <summary>
		/// Adds two <see cref="ExtendedNumerics.BigRational"/> numbers and returns the sum as a <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="augend">The augend.</param>
		/// <param name="addend">The addend.</param>
		/// <returns>The sum.</returns>
		public static MixedFraction Add(BigRational augend, BigRational addend)
		{
			return new MixedFraction(BigInteger.Zero, BigRational.Add(augend, addend));
		}

		/// <summary>
		/// Subtracts two <see cref="ExtendedNumerics.BigRational"/> numbers and returns the difference as a <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="minuend">The minuend.</param>
		/// <param name="subtrahend">The subtrahend.</param>
		/// <returns>The difference.</returns>
		public static MixedFraction Subtract(BigRational minuend, BigRational subtrahend)
		{
			return new MixedFraction(BigInteger.Zero, BigRational.Subtract(minuend, subtrahend));
		}

		/// <summary>
		/// Multiplies two <see cref="ExtendedNumerics.BigRational"/> numbers and returns the product as a <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="multiplicand">The multiplicand.</param>
		/// <param name="multiplier">The multiplier.</param>
		/// <returns>The product.</returns>
		public static MixedFraction Multiply(BigRational multiplicand, BigRational multiplier)
		{
			return new MixedFraction(BigInteger.Zero, BigRational.Multiply(multiplicand, multiplier));
		}

		/// <summary>
		/// Divides two <see cref="ExtendedNumerics.BigRational"/> numbers and returns the quotient as a <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="dividend">The dividend.</param>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The quotient.</returns>
		public static MixedFraction Divide(BigRational dividend, BigRational divisor)
		{
			return new MixedFraction(BigInteger.Zero, BigRational.Divide(dividend, divisor));
		}

		#region GCD & LCM

		/// <summary>
		/// Finds the least common denominator of two <see cref="ExtendedNumerics.MixedFraction"/> values.
		/// </summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>The least common denominator of left and right.</returns>
		public static MixedFraction LeastCommonDenominator(MixedFraction left, MixedFraction right)
		{
			BigRational leftFrac = left.GetImproperFraction();
			BigRational rightFrac = right.GetImproperFraction();

			return MixedFraction.Reduce(new MixedFraction(BigRational.LeastCommonDenominator(leftFrac, rightFrac)));
		}

		/// <summary>
		/// Finds the greatest common divisor of two <see cref="ExtendedNumerics.MixedFraction"/> values.
		/// </summary>
		/// <param name="left">The first value.</param>
		/// <param name="right">The second value.</param>
		/// <returns>The greatest common divisor of left and right.</returns>
		public static MixedFraction GreatestCommonDivisor(MixedFraction left, MixedFraction right)
		{
			BigRational leftFrac = left.GetImproperFraction();
			BigRational rightFrac = right.GetImproperFraction();

			return MixedFraction.Reduce(new MixedFraction(BigRational.GreatestCommonDivisor(leftFrac, rightFrac)));
		}

		#endregion

		#endregion

		#region Arithmetic Operators

		#region Binary Operator Overloads

		/// <summary>
		/// Adds two <see cref="MixedFraction"/> values and returns the sum.
		/// </summary>
		/// <param name="augend">The augend.</param>
		/// <param name="addend">The addend.</param>
		/// <returns>The sum.</returns>
		public static MixedFraction operator +(MixedFraction augend, MixedFraction addend) => Add(augend, addend);

		/// <summary>
		/// Subtracts two <see cref="MixedFraction"/> values and returns the difference.
		/// </summary>
		/// <param name="minuend">The minuend.</param>
		/// <param name="subtrahend">The subtrahend.</param>
		/// <returns>The difference.</returns>
		public static MixedFraction operator -(MixedFraction minuend, MixedFraction subtrahend) => Subtract(minuend, subtrahend);

		/// <summary>
		/// Multiplies two <see cref="MixedFraction"/> values and returns the product.
		/// </summary>
		/// <param name="multiplicand">The multiplicand.</param>
		/// <param name="multiplier">The multiplier.</param>
		/// <returns>The product.</returns>
		public static MixedFraction operator *(MixedFraction multiplicand, MixedFraction multiplier) => Multiply(multiplicand, multiplier);

		/// <summary>
		/// Divides two <see cref="MixedFraction"/> values and returns the quotient.
		/// </summary>
		/// <param name="dividend">The dividend.</param>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The quotient.</returns>
		public static MixedFraction operator /(MixedFraction dividend, MixedFraction divisor) => Divide(dividend, divisor);

		/// <summary>
		/// Divides two <see cref="MixedFraction"/> values and returns the remainder/modulus.
		/// </summary>
		/// <param name="dividend">The dividend.</param>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The remainder that results from the division.</returns>
		public static MixedFraction operator %(MixedFraction dividend, MixedFraction divisor) => Mod(dividend, divisor);

		#endregion

		#region Unitary Operator Overloads

		/// <summary>
		/// Returns the value of the <see cref="ExtendedNumerics.MixedFraction"/> operand. (The sign of the operand is unchanged.)
		/// </summary>
		/// <param name="value">The value to return.</param>
		/// <returns>The value of the value operand.</returns>
		public static MixedFraction operator +(MixedFraction value) => value;

		/// <summary>
		/// Negates a specified <see cref="ExtendedNumerics.MixedFraction"/> value.
		/// </summary>
		/// <param name="value">The value to negate.</param>
		/// <returns>The result of the value parameter multiplied by negative one (-1).</returns>
		public static MixedFraction operator -(MixedFraction value) => Negate(value);

		/// <summary>
		/// Increments a <see cref="ExtendedNumerics.MixedFraction"/> value by 1.
		/// </summary>
		/// <param name="value">The value to increment.</param>
		/// <returns>The value of the value parameter incremented by 1.</returns>
		public static MixedFraction operator ++(MixedFraction value) => Add(value, MixedFraction.One);

		/// <summary>
		/// Decrements a <see cref="ExtendedNumerics.MixedFraction"/> value by 1.
		/// </summary>
		/// <param name="value">The value to decrement.</param>
		/// <returns>The value of the value parameter decremented by 1.</returns>
		public static MixedFraction operator --(MixedFraction value) => Subtract(value, MixedFraction.One);

		#endregion

		#endregion

		#region Comparison Operators

		/// <summary>
		/// Returns a value that indicates whether the values of two
		/// <see cref="ExtendedNumerics.MixedFraction"/> objects are equal.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c>  if the left and right parameters have the same value; otherwise, <c>false</c>.</returns>
		public static bool operator ==(MixedFraction left, MixedFraction right) { return Compare(left, right) == 0; }

		/// <summary>
		/// Returns a value that indicates whether two <see cref="ExtendedNumerics.MixedFraction"/> 
		/// objects have different values.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns><c>true</c>  if left and right are not equal; otherwise, <c>false</c>.</returns>
		public static bool operator !=(MixedFraction left, MixedFraction right) { return Compare(left, right) != 0; }

		/// <summary>
		/// Returns a value that indicates whether a <see cref="ExtendedNumerics.MixedFraction"/> value is
		/// less than another <see cref="ExtendedNumerics.MixedFraction"/> value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <value><c>true</c> if left is less than right; otherwise, <c>false</c>.</value>
		public static bool operator <(MixedFraction left, MixedFraction right) { return Compare(left, right) < 0; }

		/// <summary>
		/// Returns a value that indicates whether a <see cref="ExtendedNumerics.MixedFraction"/> value is
		/// less than or equal to another <see cref="ExtendedNumerics.MixedFraction"/> value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <value><c>true</c> if left is less than or equal to right; otherwise, <c>false</c>.</value>
		public static bool operator <=(MixedFraction left, MixedFraction right) { return Compare(left, right) <= 0; }

		/// <summary>
		/// Returns a value that indicates whether a <see cref="ExtendedNumerics.MixedFraction"/> value is
		/// greater than another <see cref="ExtendedNumerics.MixedFraction"/> value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <value><c>true</c> if left is greater than right; otherwise, <c>false</c>.</value>
		public static bool operator >(MixedFraction left, MixedFraction right) { return Compare(left, right) > 0; }

		/// <summary>
		/// Returns a value that indicates whether a<see cref="ExtendedNumerics.MixedFraction"/> value is
		/// greater than or equal to another <see cref="ExtendedNumerics.MixedFraction"/> value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <value><c>true</c> if left is greater than or equal to right; otherwise, <c>false</c>.</value>
		public static bool operator >=(MixedFraction left, MixedFraction right) { return Compare(left, right) >= 0; }

		#endregion

		#region Compare

		/// <summary>
		/// Compares two <see cref="ExtendedNumerics.MixedFraction"/> values and
		/// returns an integer that indicates whether the first value is
		/// less than, equal to, or greater than the second value.
		/// </summary>
		/// <param name="left">The first value to compare.</param>
		/// <param name="right">The second value to compare.</param>
		/// <returns>
		/// A signed integer that indicates the relative values of left and right.
		/// The return value has these meanings:
		/// Less than zero: left is less than right.
		/// Zero: left equals right.
		/// Greater than zero: left is greater than right.
		/// </returns>
		public static int Compare(MixedFraction left, MixedFraction right)
		{
			MixedFraction leftRed = MixedFraction.Reduce(left);
			MixedFraction rightRed = MixedFraction.Reduce(right);

			if (leftRed.WholePart == rightRed.WholePart)
			{
				BigRational leftFrac = leftRed.GetImproperFraction();
				BigRational rightFrac = right.GetImproperFraction();
				return BigRational.Compare(leftFrac, rightFrac);
			}
			else
			{
				return BigInteger.Compare(leftRed.WholePart, rightRed.WholePart);
			}
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and 
		/// returns an integer that indicates whether the current instance
		/// precedes, follows, or occurs in the same position in the sort order as the other object.
		/// Satisfies the <see cref="IComparable" /> interface implementation.
		/// </summary>
		/// <param name="obj">An object to compare with this instance.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared.
		/// The return value has these meanings:
		/// Less than zero: This instance precedes <paramref name="obj" /> in the sort order.
		/// Zero: This instance occurs in the same position in the sort order as <paramref name="obj" />.
		/// Greater than zero: This instance follows <paramref name="obj" /> in the sort order.
		/// </returns>
		/// <exception cref="System.ArgumentException">Argument must be of type BigRational</exception>
		int IComparable.CompareTo(Object obj)
		{
			if (obj == null) { return 1; }
			if (!(obj is MixedFraction)) { throw new ArgumentException($"Argument must be of type {nameof(MixedFraction)}", nameof(obj)); }
			return Compare(this, (MixedFraction)obj);
		}

		/// <summary>
		/// Compares the current instance with another object of the same type and
		/// returns an integer that indicates whether the current instance
		/// precedes, follows, or occurs in the same position in the sort order as the other object.
		/// Satisfies the <see cref="IComparable{BigRational}" /> interface implementation.
		/// </summary>
		/// <param name="other">An object to compare with this instance.</param>
		/// <returns>
		/// A value that indicates the relative order of the objects being compared. The return value has these meanings:
		/// Less than zero: This instance precedes <paramref name="other" /> in the sort order.
		/// Zero: This instance occurs in the same position in the sort order as <paramref name="other" />.
		/// Greater than zero: This instance follows <paramref name="other" /> in the sort order.
		/// </returns>
		public int CompareTo(MixedFraction other)
		{
			return Compare(this, other);
		}

		#endregion

		#region Conversion

		/// <summary>
		/// Performs an implicit conversion from <see cref="System.Byte"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(byte value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="SByte"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(SByte value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Int16"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(Int16 value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="UInt16"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(UInt16 value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Int32"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(Int32 value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="UInt32"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(UInt32 value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="Int64"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(Int64 value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="UInt64"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(UInt64 value)
		{
			return new MixedFraction((BigInteger)value);
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="BigInteger"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator MixedFraction(BigInteger value)
		{
			return new MixedFraction(value);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Single"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator MixedFraction(float value)
		{
			return new MixedFraction(value);
		}


		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Double"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator MixedFraction(double value)
		{
			return new MixedFraction(value);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="System.Decimal"/> to <see cref="ExtendedNumerics.MixedFraction"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.MixedFraction"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator MixedFraction(decimal value)
		{
			return new MixedFraction(value);
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="ExtendedNumerics.MixedFraction"/> to <see cref="System.Double"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="System.Double"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator double(MixedFraction value)
		{
			double fract = (double)value.FractionalPart;
			double whole = (double)value.WholePart;
			double result = whole + (fract * (value.Sign == 0 ? 1 : value.Sign));
			if (value.WholePart == 0)
			{
				result = fract;
			}
			return result;
		}

		/// <summary>
		/// Performs an explicit conversion from <see cref="ExtendedNumerics.MixedFraction"/> to <see cref="System.Decimal"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="System.Decimal"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static explicit operator decimal(MixedFraction value)
		{
			decimal fract = (decimal)value.FractionalPart;
			decimal whole = (decimal)value.WholePart;
			decimal result = whole + (fract * (value.Sign == 0 ? 1 : value.Sign));
			if (value.WholePart == 0)
			{
				result = fract;
			}
			return result;
		}

		/// <summary>
		/// Performs an implicit conversion from <see cref="ExtendedNumerics.MixedFraction"/> to <see cref="ExtendedNumerics.BigRational"/>.
		/// </summary>
		/// <param name="value">The value to convert to a <see cref="ExtendedNumerics.BigRational"/>.</param>
		/// <returns>The result of the conversion.</returns>
		public static implicit operator BigRational(MixedFraction value)
		{
			return BigRational.Simplify(new BigRational(
					BigInteger.Add(value.FractionalPart.Numerator, BigInteger.Multiply(value.WholePart, value.FractionalPart.Denominator)),
					value.FractionalPart.Denominator
				));
		}

		/// <summary>
		/// Converts the string representation of a number to its <see cref="ExtendedNumerics.MixedFraction"/> equivalent.
		/// </summary>
		/// <param name="value">A string that contains the number to convert.</param>
		/// <returns> A value that is equivalent to the number specified in the value parameter.</returns>
		/// <exception cref="System.ArgumentException">Argument cannot be null, empty or whitespace.</exception>
		/// <exception cref="System.ArgumentException">Invalid string given for number.</exception>
		/// <exception cref="System.ArgumentException">Invalid string given for numerator.</exception>
		/// <exception cref="System.ArgumentException">Invalid string given for whole number.</exception>
		/// <exception cref="System.ArgumentException">Invalid fraction given as string to parse.</exception>
		/// <exception cref="System.ArgumentException">Invalid string given for denominator.</exception>
		public static MixedFraction Parse(string value)
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
				return new MixedFraction(whole);
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
				return new MixedFraction(whole, numerator, denominator);
			}
			else
			{
				throw new ArgumentException("Invalid fraction given as string to parse.");
			}
		}

		#endregion

		#region Equality Methods

		/// <summary>
		/// Indicates whether the current object is equal to another object of the same type.
		/// Satisfies the <see cref="IEquatable{BigRational}" /> interface implementation.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns><see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		public bool Equals(MixedFraction other)
		{
			MixedFraction reducedThis = MixedFraction.Reduce(this);
			MixedFraction reducedOther = MixedFraction.Reduce(other);

			bool result = true;

			result &= reducedThis.WholePart.Equals(reducedOther.WholePart);
			result &= reducedThis.FractionalPart.Numerator.Equals(reducedOther.FractionalPart.Numerator);
			result &= reducedThis.FractionalPart.Denominator.Equals(reducedOther.FractionalPart.Denominator);

			return result;
		}

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><see langword="true" /> if the specified object  is equal to the current object; otherwise, <see langword="false" />.</returns>
		public override bool Equals(Object obj)
		{
			if (obj == null) { return false; }
			if (!(obj is MixedFraction)) { return false; }
			return Equals((MixedFraction)obj);
		}

		/// <summary>
		/// Returns a hash code for this instance.
		/// </summary>
		/// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
		public override int GetHashCode()
		{
			return CombineHashCodes(WholePart.GetHashCode(), FractionalPart.GetHashCode());
		}

		/// <summary>
		/// Combines two hash codes into one.
		/// </summary>
		/// <param name="h1">The first hash.</param>
		/// <param name="h2">The second hash.</param>
		/// <returns>A new hashcode that represents the combination of the two specified hash codes.</returns>
		internal static int CombineHashCodes(int h1, int h2)
		{
			return (((h1 << 5) + h1) ^ h2);
		}

		#endregion

		#region Transform Methods

		/// <summary>
		/// Returns the value of this instance as a <see cref="ExtendedNumerics.BigRational"/>
		/// who's numerator may be larger than its denominator, called an improper fraction.
		/// </summary>
		/// <returns>A Fraction <see cref="ExtendedNumerics.BigRational"/> representation of this instance value.</returns>
		public BigRational GetImproperFraction()
		{
			MixedFraction input = NormalizeSign(this);

			if (input.WholePart == 0 && input.FractionalPart.Sign == 0)
			{
				return BigRational.Zero;
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
					BigRational newFractional = new BigRational(total, input.FractionalPart.Denominator);
					return newFractional;
				}
				else
				{
					return input.FractionalPart;
				}
			}
			else
			{
				return new BigRational(input.WholePart, BigInteger.One);
			}
		}

		/// <summary>
		/// Divides out any common divisors between the numerator and the denominator
		/// and then normalizes the sign.
		/// </summary>
		public static MixedFraction Reduce(MixedFraction value)
		{
			MixedFraction input = NormalizeSign(value);
			MixedFraction reduced = BigRational.ReduceToProperFraction(input.FractionalPart);
			MixedFraction result = new MixedFraction(value.WholePart + reduced.WholePart, reduced.FractionalPart);
			return result;
		}

		/// <summary>
		/// Normalizes the sign of the specified value.
		/// That is, it examines all parts of the number
		/// (WholePart, FractionalPart Numerator and Denominator)
		/// and accounts for any negative values found and removes them.
		/// The resulting parity of the number is reflected in the
		/// sign of the WholePart property.
		/// </summary>
		public static MixedFraction NormalizeSign(MixedFraction value)
		{
			return value.NormalizeSign();
		}

		/// <summary>
		/// Internal method that normalizes the sign of the current instance.
		/// </summary>
		internal MixedFraction NormalizeSign()
		{
			FractionalPart = BigRational.NormalizeSign(FractionalPart);
			if (WholePart > 0 && WholePart.Sign == 1 && FractionalPart.Sign == -1)
			{
				WholePart = BigInteger.Negate(WholePart);
				FractionalPart = BigRational.Negate(FractionalPart);
			}
			return this;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Converts the numeric value of the current <see cref="ExtendedNumerics.MixedFraction"/>
		/// instance into its equivalent string representation.
		/// </summary>
		/// <returns>The string representation of the current <see cref="ExtendedNumerics.MixedFraction"/> value.</returns>
		public override string ToString()
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Converts the numeric value of the current <see cref="ExtendedNumerics.MixedFraction"/>
		/// instance into its equivalent string representation by using the specified format.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <returns>
		/// The string representation of the current <see cref="ExtendedNumerics.MixedFraction"/> value
		/// in the format specified by the format parameter.
		/// </returns>
		public String ToString(String format)
		{
			return ToString(CultureInfo.CurrentCulture);
		}

		/// <summary>
		/// Converts the numeric value of the current <see cref="ExtendedNumerics.MixedFraction"/>
		/// instance into its equivalent string representation by using the specified
		/// culture-specific formatting information.
		/// </summary>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>
		/// The string representation of the current <see cref="ExtendedNumerics.MixedFraction"/> value in
		///	the format specified by the provider parameter.
		/// </returns>
		public String ToString(IFormatProvider provider)
		{
			return ToString("R", provider);
		}

		/// <summary>
		/// Converts the numeric value of the current <see cref="ExtendedNumerics.MixedFraction"/>
		/// instance into its equivalent string representation by using the specified 
		/// format and culture-specific format information.
		/// </summary>
		/// <param name="format">A standard or custom numeric format string.</param>
		/// <param name="provider">An object that supplies culture-specific formatting information.</param>
		/// <returns>
		/// The string representation of the current <see cref="ExtendedNumerics.MixedFraction"/> value as
		/// specified by the format and provider parameters.
		/// </returns>
		public String ToString(String format, IFormatProvider provider)
		{
			NumberFormatInfo numberFormatProvider = (NumberFormatInfo)provider.GetFormat(typeof(NumberFormatInfo));
			if (numberFormatProvider == null)
			{
				numberFormatProvider = CultureInfo.CurrentCulture.NumberFormat;
			}

			string zeroString = numberFormatProvider.NativeDigits[0];

			MixedFraction input = MixedFraction.Reduce(this);

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
