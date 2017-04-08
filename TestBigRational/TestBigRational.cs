using System;
using System.Numerics;
using ExtendedNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBigRational
{
	[TestClass]
	public class TestBigRational
	{
		[TestMethod]
		public void TestAddition()
		{
			BigRational sevenFourths = new BigRational(BigInteger.Zero, new Fraction(7, 4));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 63, 20));
			BigRational result = BigRational.Add(sevenFourths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestSubtraction()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFourths = new BigRational(BigInteger.Zero, new Fraction(7, 4));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 7, 4));
			BigRational result = BigRational.Subtract(sevenTwoths, sevenFourths);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestMultiplication()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 49, 10));
			BigRational result = BigRational.Multiply(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestDivision()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 5, 2));
			BigRational result = BigRational.Divide(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestConvertToDouble()
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void TestConvertFromDouble()
		{
			throw new NotImplementedException();
		}
	}
}
