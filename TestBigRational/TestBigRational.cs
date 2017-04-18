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
			BigRational sevenTwoths = new BigRational(3, 1, 2);
			BigRational sevenFifths = new BigRational(1, 2, 5);

			BigRational expected = new BigRational(2, 1, 10);
			BigRational result = BigRational.Subtract(sevenTwoths, sevenFifths);

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
		public void TestConvertFromDecimalPointDouble()
		{
			Double fifteenSixteenths = 0.9375d;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConvertFromWholeNumberDouble()
		{
			Double seven = 7.0d;

			BigRational expectedValue = new BigRational(7, new Fraction(0, 1));

			BigRational result1 = new BigRational(seven);
			BigRational result2 = (BigRational)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[TestMethod]
		public void TestConvertFromNegativeMixedDouble()
		{
			Double negativeOneAndOneThird = -((double)4 / (double)3);

			BigRational expectedValue = new BigRational(-1, new Fraction(1, 3));
			BigRational result = (BigRational)negativeOneAndOneThird;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConstruction()
		{
			BigRational result1 = new BigRational(BigInteger.Zero, new Fraction(182, 26));
			BigRational result1_2 = new BigRational(new Fraction(182, 26));
			BigRational result2 = new BigRational(BigInteger.Zero, new Fraction(-7, 5));

			BigRational expected1 = new BigRational(7);
			BigRational expected2 = new BigRational(-1, 2, 5);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected1, result1_2);
			Assert.AreEqual(expected2, result2);
		}

		[TestMethod]
		public void TestExpandImproperFraction()
		{
			BigRational threeAndOneThird = new BigRational(3, 1, 3);
			BigRational oneEightyTwoTwentySixths = new BigRational(new Fraction(182, 26));
			BigRational negativeThreeAndOneSeventh = new BigRational(-3, 1, 7);
			BigRational seven = new BigRational(7);

			Fraction expected313 = new Fraction(10, 3);
			Fraction expected18226 = new Fraction(91, 13);
			Fraction expectedNeg317 = new Fraction(-20, 7);
			Fraction expected7over1 = new Fraction(7, 1);

			Fraction result1 = threeAndOneThird.GetImproperFraction();
			Fraction result2 = oneEightyTwoTwentySixths.GetImproperFraction();
			Fraction result3 = negativeThreeAndOneSeventh.GetImproperFraction();
			Fraction result7 = seven.GetImproperFraction();


			Assert.AreEqual(expected313, result1);
			Assert.AreEqual(expected18226, result2);
			Assert.AreEqual(expectedNeg317, result3);
			Assert.AreEqual(expected7over1, result7);
		}
	}
}
