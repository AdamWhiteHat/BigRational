# BigRational

Arbitrary precision rational number class

Here is the class signature, so you can see what it supports, but suffice it to say it supports all the typical arithmetic operations one would expect from an arithmetic class library.

public class BigRational : IComparable, IComparable<BigRational>, IEquatable<BigRational>
{
	// 
	// Static Members
	// 

	public static BigRational One;
	public static BigRational Zero;
	public static BigRational MinusOne;
	
	//
	// Instance Properties
	//

	public BigInteger WholePart { get; }
	public Fraction FractionalPart { get; }

	public int Sign { get; }
	public bool IsZero { get; }

	//
	// Constructors
	//
	
	public BigRational();
	public BigRational(int value);
	public BigRational(BigInteger value);
	public BigRational(Fraction fraction);
	public BigRational(BigInteger whole, Fraction fraction);
	public BigRational(BigInteger numerator, BigInteger denominator);
	public BigRational(BigInteger whole, BigInteger numerator, BigInteger denominator);
	public BigRational(float value);
	public BigRational(double value);
	public BigRational(decimal value);
	
	// Parse from a string //
	public static BigRational Parse(string value);

    //
	// Arithmetic Methods
	//

	public static BigRational Add(BigRational augend, BigRational addend);
	public static BigRational Subtract(BigRational minuend, BigRational subtrahend);
	public static BigRational Multiply(BigRational multiplicand, BigRational multiplier);
	public static BigRational Divide(BigInteger dividend, BigInteger divisor);
	public static BigRational Divide(BigRational dividend, BigRational divisor);
	public static BigRational Remainder(BigInteger dividend, BigInteger divisor);
	public static BigRational Mod(BigRational number, BigRational mod);
	public static BigRational Pow(BigRational baseValue, BigInteger exponent);
	public static BigRational Sqrt(BigRational value);
	public static BigRational NthRoot(BigRational value, int root);

	public static BigRational Abs(BigRational rational);
	public static BigRational Negate(BigRational rational);	
	
	public static double Log(BigRational rational);
	
	public static BigRational Add(Fraction augend, Fraction addend);
	public static BigRational Subtract(Fraction minuend, Fraction subtrahend);
	public static BigRational Multiply(Fraction multiplicand, Fraction multiplier);
	public static BigRational Divide(Fraction dividend, Fraction divisor);

	//
	// GCD & LCM
	//

	public static BigRational GreatestCommonDivisor(BigRational left, BigRational right);
	public static BigRational LeastCommonDenominator(BigRational left, BigRational right);

	//
	// Arithmetic Operators
	//

	// Binary //
	public static BigRational operator +(BigRational augend, BigRational addend);
	public static BigRational operator -(BigRational minuend, BigRational subtrahend);
	public static BigRational operator *(BigRational multiplicand, BigRational multiplier) ;
	public static BigRational operator /(BigRational dividend, BigRational divisor);
	public static BigRational operator %(BigRational dividend, BigRational divisor);

	// Unitary //
	public static BigRational operator +(BigRational rational) => Abs(rational);
	public static BigRational operator -(BigRational rational) => Negate(rational);
	public static BigRational operator ++(BigRational rational) => Add(rational, BigRational.One);
	public static BigRational operator --(BigRational rational) => Subtract(rational, BigRational.One);

	//
	// Comparison Operators
	//

	public static bool operator ==(BigRational left, BigRational right);
	public static bool operator !=(BigRational left, BigRational right);
	public static bool operator <(BigRational left, BigRational right) ;
	public static bool operator <=(BigRational left, BigRational right);
	public static bool operator >(BigRational left, BigRational right) ;
	public static bool operator >=(BigRational left, BigRational right);

	//
	// Compare To
	//

	public static int Compare(BigRational left, BigRational right);	
	public int CompareTo(BigRational other); // IComparable<Fraction>
	int IComparable.CompareTo(Object obj); // IComparable

	//
	// Conversion
	//

	// To BigRational //
	public static implicit operator BigRational(byte value);
	public static implicit operator BigRational(SByte value);
	public static implicit operator BigRational(Int16 value);
	public static implicit operator BigRational(UInt16 value);
	public static implicit operator BigRational(Int32 value);
	public static implicit operator BigRational(UInt32 value);
	public static implicit operator BigRational(Int64 value);
	public static implicit operator BigRational(UInt64 value);
	public static implicit operator BigRational(BigInteger value);
	public static explicit operator BigRational(float value);
	public static explicit operator BigRational(double value);
	public static explicit operator BigRational(decimal value);
	
	// From BigRational //
	public static explicit operator double(BigRational value);
	public static explicit operator decimal(BigRational value);
	public static implicit operator Fraction(BigRational value);

	//
	// Equality Methods
	//

	public bool Equals(BigRational other);
	public override bool Equals(Object obj);
	public override int GetHashCode();

	//
	// Transform Methods
	//

	public static BigRational Reduce(BigRational value);
	public static BigRational NormalizeSign(BigRational value);
	public Fraction GetImproperFraction();
	
	//
	// Overrides
	//

	public override string ToString();
	public String ToString(String format);
	public String ToString(IFormatProvider provider);
	public String ToString(String format, IFormatProvider provider);
}



  This library is also available on nuget: [https://www.nuget.org/packages/ExtendedNumerics.BigRational](https://www.nuget.org/packages/ExtendedNumerics.BigRational)



#

# Other mathy projects & numeric types

I've written a number of other polynomial implementations and numeric types catering to various specific scenarios. Depending on what you're trying to do, another implementation of this same library might be more appropriate. All of my polynomial projects should have feature parity, where appropriate[^1].

[^1]: For example, the ComplexPolynomial implementation may be missing certain operations (namely: Irreducibility), because such a notion does not make sense or is ill defined in the context of complex numbers).

* [GenericArithmetic](https://github.com/AdamWhiteHat/GenericArithmetic) - A core math library. Its a class of static methods that allows you to perform arithmetic on an arbitrary numeric type, represented by the generic type T, who's concrete type is decided by the caller. This is implemented using System.Linq.Expressions and reflection to resolve the type's static overloadable operator methods at runtime, so it works on all the .NET numeric types automagically, as well as any custom numeric type, provided it overloads the numeric operators and standard method names for other common functions (Min, Max, Abs, Sqrt, Parse, Sign, Log,  Round, etc.). Every generic arithmetic class listed below takes a dependency on this class.

* [Polynomial](https://github.com/AdamWhiteHat/Polynomial) - The original. A univariate polynomial that uses System.Numerics.BigInteger as the indeterminate type.
* [GenericPolynomial](https://github.com/AdamWhiteHat/GenericPolynomial) -  A univariate polynomial library that allows the indeterminate to be of an arbitrary type, as long as said type implements operator overloading. This is implemented dynamically, at run time, calling the operator overload methods using Linq.Expressions and reflection.
* [CSharp11Preview.GenericMath.Polynomial](https://github.com/AdamWhiteHat/CSharp11Preview.GenericMath.Polynomial) -  A univariate polynomial library that allows the indeterminate to be of an arbitrary type, but this version is implemented using C# 11's new Generic Math via static virtual members in interfaces.
>
* [MultivariatePolynomial](https://github.com/AdamWhiteHat/MultivariatePolynomial) - A multivariate polynomial (meaning more than one indeterminate, e.g. 2*X*Y^2) which uses BigInteger as the type for the indeterminates.
* [GenericMultivariatePolynomial](https://github.com/AdamWhiteHat/GenericMultivariatePolynomial) - A multivariate polynomial that allows the indeterminates to be of [the same] arbitrary type. GenericMultivariatePolynomial is to MultivariatePolynomial what GenericPolynomial is to Polynomial, and indeed is implemented using the same strategy as GenericPolynomial (i.e. dynamic calling of the operator overload methods at runtime using Linq.Expressions and reflection).
>
* [ComplexPolynomial](https://github.com/AdamWhiteHat/ComplexPolynomial) - A univariate polynomial library that has System.Numerics.Complex type indeterminates.
* [ComplexMultivariatePolynomial](https://github.com/AdamWhiteHat/ComplexMultivariatePolynomial) -  A multivariate polynomial library that has System.Numerics.Complex indeterminates.
>
* [BigDecimal](https://github.com/AdamWhiteHat/BigDecimal) - An arbitrary precision, base-10 floating point number class.
* [BigRational](https://github.com/AdamWhiteHat/BigRational) - Encodes a numeric value as an Integer + Fraction
* [BigComplex](https://github.com/AdamWhiteHat/BigComplex) - Essentially the same thing as System.Numerics.Complex but uses a System.Numerics.BigInteger type for the real and imaginary parts instead of a double.
>
* [IntervalArithmetic](https://github.com/AdamWhiteHat/IntervalArithmetic). Instead of representing a value as a single number, interval arithmetic represents each value as a mathematical interval, or range of possibilities, [a,b], and allows the standard arithmetic operations to be performed upon them too, adjusting or scaling the underlying interval range as appropriate. See [Wikipedia's article on Interval Arithmetic](https://en.wikipedia.org/wiki/Interval_arithmetic) for further information.
* [GNFS](https://github.com/AdamWhiteHat/GNFS) - A C# reference implementation of the General Number Field Sieve algorithm for the purpose of better understanding the General Number Field Sieve algorithm.
