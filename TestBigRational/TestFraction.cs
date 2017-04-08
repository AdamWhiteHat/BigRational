using System;
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
	}
}
