using System;
using System.Numerics;
using ExtendedNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBigRational
{
	[TestClass]
	public class TestBigRationalConversions
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromDecimalPointDouble()
		{
			Double fifteenSixteenths = 0.9375d;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromWholeNumberDouble()
		{
			Double seven = 7.0d;

			BigRational expectedValue = new BigRational(7, new Fraction(0, 1));

			BigRational result1 = new BigRational(seven);
			BigRational result2 = (BigRational)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromNegativeMixedDouble()
		{
			Double negativeOneAndOneThird = -4d / 3d;

			BigRational expectedValue = new BigRational(-1, new Fraction(1, 3));
			BigRational result = (BigRational)negativeOneAndOneThird;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromDecimalPointDecimal()
		{
			Decimal fifteenSixteenths = 0.9375m;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromWholeNumberDecimal()
		{
			Decimal seven = 7.0m;

			BigRational expectedValue = new BigRational(7, new Fraction(0, 1));

			BigRational result1 = new BigRational(seven);
			BigRational result2 = (BigRational)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestConvertFromNegativeMixedDecimal()
		{
			// Note that Decimal works best with a fixed number of decimal points
			// This will fail if we use the same numbers as used with the double test
			Decimal negativeOneAndOneHundredAndTwentyEigth = -129m / 128m;

			BigRational expectedValue = new BigRational(-1, new Fraction(1, 128));
			BigRational result = (BigRational)negativeOneAndOneHundredAndTwentyEigth;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestCastZeroFromDouble()
		{
			double zero = 0;
			BigRational result = (BigRational)zero;
			BigRational expectedValue = BigRational.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod, TestCategory("Conversions")]
		public void TestCastZeroFromDecimal()
		{
			decimal zero = 0;
			BigRational result = (BigRational)zero;
			BigRational expectedValue = BigRational.Zero;

			Assert.AreEqual(expectedValue, result);
		}
	}
}
