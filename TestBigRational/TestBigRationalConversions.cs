using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Conversions")]
	public class TestBigRationalConversions
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestConvertFromDecimalPointFloat()
		{
			float negativeOneAndOneHalf = -3f / 2f;
			float oneThird = 1f / 3f;

			BigRational expectedValue1 = new BigRational(-1, new Fraction(1, 2));
			BigRational result1 = (BigRational)negativeOneAndOneHalf;

			BigRational expectedValue2 = new BigRational(0, new Fraction(1, 3));
			BigRational result2 = (BigRational)oneThird;

			Assert.AreEqual(expectedValue1, result1);
			Assert.AreEqual(expectedValue2, result2);
		}

		[Test]
		public void TestConvertFromDecimalPointDouble()
		{
			double fifteenSixteenths = 0.9375d;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestConvertFromWholeNumberDouble()
		{
			double seven = 7.0d;

			BigRational expectedValue = new BigRational(7, new Fraction(0, 1));

			BigRational result1 = new BigRational(seven);
			BigRational result2 = (BigRational)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[Test]
		public void TestConvertFromNegativeMixedDouble()
		{
			double negativeOneAndOneThird = -4d / 3d;

			BigRational expectedValue = new BigRational(-1, new Fraction(1, 3));
			BigRational result = (BigRational)negativeOneAndOneThird;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestConvertFromDecimalPointDecimal()
		{
			decimal fifteenSixteenths = 0.9375m;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestConvertFromWholeNumberDecimal()
		{
			decimal seven = 7.0m;

			BigRational expectedValue = new BigRational(7, new Fraction(0, 1));

			BigRational result1 = new BigRational(seven);
			BigRational result2 = (BigRational)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[Test]
		public void TestConvertFromNegativeMixedDecimal()
		{
			// Note that decimal works best with a fixed number of decimal points
			// This will fail if we use the same numbers as used with the double test
			decimal negativeOneAndOneHundredAndTwentyEigth = -129m / 128m;

			BigRational expectedValue = new BigRational(-1, new Fraction(1, 128));
			BigRational result = (BigRational)negativeOneAndOneHundredAndTwentyEigth;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestCastZeroFromFloat()
		{
			float zero = 0;
			BigRational result = (BigRational)zero;
			BigRational expectedValue = BigRational.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestCastZeroFromDouble()
		{
			double zero = 0;
			BigRational result = (BigRational)zero;
			BigRational expectedValue = BigRational.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestCastZeroFromDecimal()
		{
			decimal zero = 0;
			BigRational result = (BigRational)zero;
			BigRational expectedValue = BigRational.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestParseWholeNumber()
		{
			string toParse = "3";

			BigRational result = BigRational.Parse(toParse);
			BigRational expectedValue = new BigRational(3, 0, 1);

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestParseFraction()
		{
			string toParse = "1/3";

			BigRational result = BigRational.Parse(toParse);
			BigRational expectedValue = new BigRational(0, 1, 3);

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestParseMixedNumber()
		{
			string toParse = "-1 + 1/3";

			BigRational result = BigRational.Parse(toParse);
			BigRational expectedValue = new BigRational(-1, 1, 3);

			Assert.AreEqual(expectedValue, result);
		}
	}
}
