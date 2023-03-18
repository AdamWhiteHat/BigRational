# BigRational

Arbitrary precision rational number class

E.g.
  3 + 3/5



  This library is also available on nuget: [https://www.nuget.org/packages/ExtendedNumerics.BigRational](https://www.nuget.org/packages/ExtendedNumerics.BigRational)



#

# Other mathy projects & numeric types

I've written a number of other polynomial implementations and numeric types catering to various specific scenarios. Depending on what you're trying to do, another implementation of this same library might be more appropriate. All of my polynomial projects should have feature parity, where appropriate[^1].

[^1]: For example, the ComplexPolynomial implementation may be missing certain operations (namely: Irreducibility), because such a notion does not make sense or is ill defined in the context of complex numbers).

* [Polynomial](https://github.com/AdamWhiteHat/Polynomial). The original. A univariate polynomial that uses System.Numerics.BigInteger as the indeterminate type.
* [GenericPolynomial](https://github.com/AdamWhiteHat/GenericPolynomial) allows the indeterminate to be of an arbitrary type, as long as said type implements operator overloading. This is implemented by dynamically, at run time, calling the operator overload methods using Linq.Expressions and reflection.
* [CSharp11Preview.GenericMath.Polynomial](https://github.com/AdamWhiteHat/CSharp11Preview.GenericMath.Polynomial) allows the indeterminate to be of an arbitrary type, but this version is implemented using C# 11's new Generic Math via static virtual members in interfaces.
>
* [MultivariatePolynomial](https://github.com/AdamWhiteHat/MultivariatePolynomial). A multivariate polynomial (meaning more than one indeterminate, e.g. 2*X*Y^2) which uses BigInteger as the type for the indeterminates.
* [GenericMultivariatePolynomial](https://github.com/AdamWhiteHat/GenericMultivariatePolynomial). As above, but allows the indeterminates to be of [the same] arbitrary type. GenericMultivariatePolynomial is to MultivariatePolynomial what GenericPolynomial is to Polynomial, and indeed is implemented using the same strategy as GenericPolynomial (i.e. dynamic calling of the operator overload methods at runtime using Linq.Expressions and reflection).
>
* [ComplexPolynomial](https://github.com/AdamWhiteHat/ComplexPolynomial). Same idea as Polynomial, but using the System.Numerics.Complex class instead of System.Numerics.BigInteger.
* [ComplexMultivariatePolynomial](https://github.com/AdamWhiteHat/ComplexMultivariatePolynomial). Same idea as MultivariatePolynomial, but using the System.Numerics.Complex class instead of System.Numerics.BigInteger.
>
* [BigDecimal](https://github.com/AdamWhiteHat/BigDecimal) - An arbitrary precision, base-10 floating point number class.
* [BigRational](https://github.com/AdamWhiteHat/BigRational) encodes a numeric value as an Integer + Fraction
* [BigComplex](https://github.com/AdamWhiteHat/BigComplex) - Essentially the same thing as System.Numerics.Complex but uses a System.Numerics.BigInteger type for the real and imaginary parts instead of a double.
>
* [IntervalArithmetic](https://github.com/AdamWhiteHat/IntervalArithmetic). Instead of representing a value as a single number, interval arithmetic represents each value as a mathematical interval, or range of possibilities, [a,b], and allows the standard arithmetic operations to be performed upon them too, adjusting or scaling the underlying interval range as appropriate. See [Wikipedia's article on Interval Arithmetic](https://en.wikipedia.org/wiki/Interval_arithmetic) for further information.
* [GNFS](https://github.com/AdamWhiteHat/GNFS) - A C# reference implementation of the General Number Field Sieve algorithm for the purpose of better understanding the General Number Field Sieve algorithm.

