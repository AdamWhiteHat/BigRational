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
			BigRational threeHalfs = new BigRational(BigInteger.Zero, new Fraction(3, 2));
			BigRational tenEighths = new BigRational(BigInteger.Zero, new Fraction(10, 8));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(11, 4)));
			BigRational result = BigRational.Add(threeHalfs, tenEighths);

			BigRational reducedResult = BigRational.Reduce(result);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestSubtraction()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFourths = new BigRational(BigInteger.Zero, new Fraction(7, 4));

			BigRational expected = new BigRational(BigInteger.Zero, 7, 4);
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
			BigRational oneSixteenth = new BigRational(BigInteger.Zero, new Fraction(1, 16));

			Double expectedValue = 0.0625d;
			Double result = (Double)oneSixteenth;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConvertFromDouble()
		{
			Double fifteenSixteenths = 0.9375d;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}
	}
}
