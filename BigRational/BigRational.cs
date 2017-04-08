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
		public BigInteger WholePart { get; private set; }
		public Fraction FractionalPart { get; private set; }

		public BigRational(BigInteger value)
		{
			WholePart = value;
		}

		public BigRational(BigInteger whole, Fraction fraction)
			: this(whole)
		{
			FractionalPart = fraction;
		}


	}
}
