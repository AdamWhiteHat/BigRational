using System;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ExtendedNumerics
{
	public class Fraction
	{
		public BigInteger Numerator { get; private set; }
		public BigInteger Denominator { get; private set; }

		public Fraction(BigInteger value)
		{
			Numerator = value;
			Denominator = BigInteger.One;
		}

		public Fraction(BigInteger numerator, BigInteger denominator)
		{
			Numerator = numerator;
			Denominator = denominator;
		}
	}
}
