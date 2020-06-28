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
		public void TestConvertFromFloat()
		{
			float negativeOneHalf = -1f / 2f;
			float oneThird = 1f / 3f;

			TestContext.WriteLine($"(float) -1/2 == {negativeOneHalf}");
			TestContext.WriteLine($"(float)  1/3 == {oneThird}");

			Fraction expectedValue1 = new Fraction(-1, 2);
			Fraction result1 = (Fraction)negativeOneHalf;

			BigRational expectedValue2 = new BigRational(0, new Fraction(1, 3));
			BigRational result2 = (BigRational)oneThird;

			Assert.AreEqual(expectedValue1, result1);
			Assert.AreEqual(expectedValue2, result2);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertToDouble()
		{
			Fraction oneSixteenth = new Fraction(1, 16);
			Fraction negativeOneThird = new Fraction(-1, 3);

			double expectedValueOneSixteenth = 0.0625d;
			double expectedValueNegativeOneThird = -1d / 3d;

			double resultOneSixteenth = (double)oneSixteenth;
			double resultNegativeOneThird = (double)negativeOneThird;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromDouble()
		{
			double fifteenSixteenths = 0.9375d;
			double negativeOneThird = -1d / 3d;

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

			decimal expectedValueOneSixteenth = 0.0625m;
			decimal expectedValueNegativeOneThird = -1m / 3m;

			decimal resultOneSixteenth = (decimal)oneSixteenth;
			decimal resultNegativeOneThird = (decimal)negativeOneThird;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromDecimal()
		{
			// decimal converts best with a fixed number of decimal points
			decimal fifteenSixteenths = 0.9375m;
			decimal negativeOneOneHundredAndTwentyEight = -1m / 128m;

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
