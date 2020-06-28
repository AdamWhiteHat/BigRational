using System;
using System.Numerics;
using ExtendedNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBigRational
{
	[TestClass]
	public class TestFractionConversions
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertToDouble()
		{
			Fraction oneSixteenth = new Fraction(1, 16);
			Fraction negativeOneThird = new Fraction(-1, 3);

			Double expectedValueOneSixteenth = 0.0625d;
			Double expectedValueNegativeOneThird = -1d / 3d;

			Double resultOneSixteenth = (Double)oneSixteenth;
			Double resultNegativeOneThird = (Double)negativeOneThird;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromDouble()
		{
			Double fifteenSixteenths = 0.9375d;
			Double negativeOneThird = -1d / 3d;

			Fraction expectedValueFifteenSixteenths = new Fraction(15, 16);
			Fraction expectedValueNegativeOneThird = new Fraction(-1, 3);

			Fraction result1516 = (Fraction)fifteenSixteenths;
			Fraction resultNeg13 = (Fraction)negativeOneThird;

			Assert.AreEqual(expectedValueFifteenSixteenths, result1516);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNeg13);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertToDecimal()
		{
			Fraction oneSixteenth = new Fraction(1, 16);
			Fraction negativeOneThird = new Fraction(-1, 3);

			Decimal expectedValueOneSixteenth = 0.0625m;
			Decimal expectedValueNegativeOneThird = -1m / 3m;

			Decimal resultOneSixteenth = (Decimal)oneSixteenth;
			Decimal resultNegativeOneThird = (Decimal)negativeOneThird;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromDecimal()
		{
			// Decimal converts best with a fixed number of decimal points
			Decimal fifteenSixteenths = 0.9375m;
			Decimal negativeOneOneHundredAndTwentyEight = -1m / 128m;

			Fraction expectedValueFifteenSixteenths = new Fraction(15, 16);
			Fraction expectedValueNegativeOneThird = new Fraction(-1, 128);

			Fraction result1516 = (Fraction)fifteenSixteenths;
			Fraction resultNeg1128 = (Fraction)negativeOneOneHundredAndTwentyEight;

			Assert.AreEqual(expectedValueFifteenSixteenths, result1516);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNeg1128);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestCastZeroFromDouble()
		{
			double zero = 0;
			Fraction result = (Fraction)zero;
			Fraction expectedValue = Fraction.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestCastZeroFromDecimal()
		{
			decimal zero = 0;
			Fraction result = (Fraction)zero;
			Fraction expectedValue = Fraction.Zero;

			Assert.AreEqual(expectedValue, result);
		}
	}
}
