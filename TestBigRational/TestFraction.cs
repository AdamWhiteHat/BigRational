using System;
using System.Numerics;
using ExtendedNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBigRational
{
	[TestClass]
	public class TestFraction
	{
		[TestMethod]
		public void TestAddition()
		{
			Fraction oneThird = new Fraction(1, 3);
			Fraction oneFifth = new Fraction(1, 5);

			Fraction expectedValue = new Fraction(8, 15);

			Fraction result = Fraction.Add(oneThird, oneFifth);
			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestSubtraction()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValue = new Fraction(1, 3);

			Fraction result = Fraction.Subtract(oneHalf, oneSixth);
			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestImproperSubtraction()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValue = new Fraction(1, 3);

			Fraction result = Fraction.Subtract(oneHalf, oneSixth);
			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestMultiplication()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction twoFifths = new Fraction(2, 5);

			Fraction expectedValue = new Fraction(1, 5);

			Fraction result = Fraction.Multiply(oneHalf, twoFifths);
			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestDivision()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValue = new Fraction(3, 1);

			Fraction result = Fraction.Divide(oneHalf, oneSixth);
			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConvertToDouble()
		{
			Fraction oneSixteenth = new Fraction(1, 16);
			Double expectedValue = 0.0625d;

			Double result = (Double)oneSixteenth;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConvertFromDouble()
		{
			Double fifteenSixteenths = 0.9375d;
			Fraction expectedValue = new Fraction(15, 16);

			Fraction result = (Fraction)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestSimplify()
		{
			Fraction eighteenTwos = new Fraction(18, 2);
			Fraction noChange = new Fraction(1, 8);
			Fraction reduced = new Fraction(2, 6);
			Fraction reducedNegative = new Fraction(-2, 8);

			Fraction expectedValueEighteenTwos = new Fraction(9, 1);
			Fraction expectedValueNoChange = new Fraction(1, 8);
			Fraction expectedValueReduced = new Fraction(1, 3);
			Fraction expectedValueReducedNegative = new Fraction(-1, 4);

			Assert.AreEqual(expectedValueEighteenTwos, eighteenTwos);
			Assert.AreEqual(expectedValueNoChange, noChange);
			Assert.AreEqual(expectedValueReduced, reduced);
			Assert.AreEqual(expectedValueReducedNegative, reducedNegative);
		}

		[TestMethod]
		public void TestNormalizeSign()
		{
			Fraction noChange1 = new Fraction(3, 11);
			Fraction noChange2 = new Fraction(-3, 13);
			Fraction normalized = new Fraction(3, -17);

			Fraction expectedValueNoChange1 = new Fraction(3, 11);
			Fraction expectedValueNoChange2 = new Fraction(-3, 13);
			Fraction expectedValueNormalized = new Fraction(-3, 17);

			Assert.AreEqual(expectedValueNoChange1, noChange1);
			Assert.AreEqual(expectedValueNoChange2, noChange2);
			Assert.AreEqual(expectedValueNormalized, normalized);
		}

		[TestMethod]
		public void TestReduceToProperFraction()
		{
			BigRational noChange = Fraction.ReduceToProperFraction(new Fraction(7, 16));
			BigRational reduced = Fraction.ReduceToProperFraction(new Fraction(16, 7));

			BigRational expectedValueNoChange = new BigRational(0, 7, 16);
			BigRational expectedValueReduced = new BigRational(2, 2, 7);

			Assert.AreEqual(expectedValueNoChange, noChange);
			Assert.AreEqual(expectedValueReduced, reduced);
		}
	}
}
