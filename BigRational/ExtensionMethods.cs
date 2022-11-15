using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExtendedNumerics.Internal
{
	public static class ExtensionMethods
	{
		/// <summary>Returns the square root of a BigInteger.</summary>
		public static BigInteger SquareRoot(this BigInteger input)
		{
			if (input.IsZero) return new BigInteger(0);
			if (input.IsOne) return new BigInteger(1);

			BigInteger n = new BigInteger(0);
			BigInteger p = new BigInteger(0);
			BigInteger low = new BigInteger(0);
			BigInteger high = BigInteger.Abs(input);

			while (high > low + 1)
			{
				n = (high + low) >> 1;
				p = n * n;

				if (input < p)
					high = n;
				else if (input > p)
					low = n;
				else
					break;
			}
			return input == p ? n : low;
		}

		/// <summary> Returns the Nth root of a BigInteger. The the value must be a positive integer and the parameter root must be greater than or equal to 1.</summary>
		public static BigInteger NthRoot(this BigInteger source, int root)
		{
			BigInteger remainder = new BigInteger();
			return source.NthRoot(root, out remainder);
		}

		/// <summary> Returns the Nth root of a BigInteger with remainder. The the value must be a positive integer and the parameter root must be greater than or equal to 1.</summary>
		public static BigInteger NthRoot(this BigInteger source, int root, out BigInteger remainder)
		{
			if (root < 1) throw new Exception("Root must be greater than or equal to 1");
			if (source.Sign == -1) throw new Exception("Value must be a positive integer");

			remainder = 0;
			if (source == BigInteger.One) { return BigInteger.One; }
			if (source == BigInteger.Zero) { return BigInteger.Zero; }
			if (root == 1) { return source; }

			BigInteger upperbound = source;
			BigInteger lowerbound = BigInteger.Zero;

			while (true)
			{
				BigInteger nval = (upperbound + lowerbound) >> 1;
				BigInteger testPow = BigInteger.Pow(nval, root);

				if (testPow > source) upperbound = nval;
				if (testPow < source) lowerbound = nval;
				if (testPow == source)
				{
					lowerbound = nval;
					break;
				}
				if (lowerbound == upperbound - 1) break;
			}
			remainder = source - BigInteger.Pow(lowerbound, root);
			return lowerbound;
		}
	}
}
