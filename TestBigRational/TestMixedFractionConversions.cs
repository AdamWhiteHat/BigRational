using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Conversions")]
	public class TestMixedFractionConversions
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
		public void TestConvertFromDecimalPointFloat()
		{
			float negativeOneAndOneHalf = -3f / 2f;
			float oneThird = 1f / 3f;

			MixedFraction expectedValue1 = new MixedFraction(-1, new BigRational(1, 2));
			MixedFraction result1 = (MixedFraction)negativeOneAndOneHalf;

			MixedFraction expectedValue2 = new MixedFraction(0, new BigRational(1, 3));
			MixedFraction result2 = (MixedFraction)oneThird;

			Assert.AreEqual(expectedValue1, result1);
			Assert.AreEqual(expectedValue2, result2);
		}

		[Test]
		public void TestConvertFromDecimalPointDouble()
		{
			double fifteenSixteenths = 0.9375d;

			MixedFraction expectedValue = new MixedFraction(BigInteger.Zero, new BigRational(15, 16));
			MixedFraction result = (MixedFraction)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestConvertFromWholeNumberDouble()
		{
			double seven = 7.0d;

			MixedFraction expectedValue = new MixedFraction(7, new BigRational(0, 1));

			MixedFraction result1 = new MixedFraction(seven);
			MixedFraction result2 = (MixedFraction)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[Test]
		public void TestConvertFromNegativeMixedDouble()
		{
			double negativeOneAndOneThird = -4d / 3d;

			MixedFraction expectedValue = new MixedFraction(-1, new BigRational(1, 3));
			MixedFraction result = (MixedFraction)negativeOneAndOneThird;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestConvertFromDecimalPointDecimal()
		{
			decimal fifteenSixteenths = 0.9375m;

			MixedFraction expectedValue = new MixedFraction(BigInteger.Zero, new BigRational(15, 16));
			MixedFraction result = (MixedFraction)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestConvertFromWholeNumberDecimal()
		{
			decimal seven = 7.0m;

			MixedFraction expectedValue = new MixedFraction(7, new BigRational(0, 1));

			MixedFraction result1 = new MixedFraction(seven);
			MixedFraction result2 = (MixedFraction)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[Test]
		public void TestConvertFromNegativeMixedDecimal()
		{
			// Note that decimal works best with a fixed number of decimal points
			// This will fail if we use the same numbers as used with the double test
			decimal negativeOneAndOneHundredAndTwentyEigth = -129m / 128m;

			MixedFraction expectedValue = new MixedFraction(-1, new BigRational(1, 128));
			MixedFraction result = (MixedFraction)negativeOneAndOneHundredAndTwentyEigth;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestConvertToDouble()
		{
			MixedFraction oneSixteenth = new MixedFraction(1, 16);
			MixedFraction negativeOneThird = new MixedFraction(-1, 3);
			MixedFraction improperThirteenFourths = new MixedFraction(13, 4);
			MixedFraction improperNegativeNineFifths = new MixedFraction(-9, 5);
			MixedFraction largeRational = new MixedFraction(
				BigInteger.Parse("36979593578080793436251350559911534471745439536857510606701399088382738"),
				BigInteger.Parse("2226645766662654219495625670010961737131540370393163559325615188894568125"));

			double expectedValueOneSixteenth = 0.0625d;
			double expectedValueNegativeOneThird = -1d / 3d;
			double expectedValueImproperThirteenFourths = 3.25d;
			double expectedValueImproperNegativeNineFifths = -1.8d;
			double expectedValueLargeRational = 0.016607757790547271d;

			double resultOneSixteenth = (double)oneSixteenth;
			double resultNegativeOneThird = (double)negativeOneThird;
			double resultImproperThirteenFourths = (double)improperThirteenFourths;
			double resultImproperNegativeNineFifths = (double)improperNegativeNineFifths;
			double resultLargeRational = (double)largeRational;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth, "1/16");
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird, "-1/3");
			Assert.AreEqual(expectedValueImproperThirteenFourths, resultImproperThirteenFourths, "13/4");
			Assert.AreEqual(expectedValueImproperNegativeNineFifths, resultImproperNegativeNineFifths, "-9/5");
			Assert.AreEqual(expectedValueLargeRational, resultLargeRational, "(a large fraction)");
		}

		[Test]
		public void TestConvertToDecimal()
		{
			MixedFraction oneSixteenth = new MixedFraction(1, 16);
			MixedFraction negativeOneThird = new MixedFraction(-1, 3);
			MixedFraction improperThirteenFourths = new MixedFraction(13, 4);
			MixedFraction improperNegativeNineFifths = new MixedFraction(-9, 5);
			MixedFraction largeRational = new MixedFraction(
							BigInteger.Parse("36979593578080793436251350559911534471745439536857510606701399088382738"),
							BigInteger.Parse("2226645766662654219495625670010961737131540370393163559325615188894568125"));

			decimal expectedValueOneSixteenth = 0.0625m;
			decimal expectedValueNegativeOneThird = -1m / 3m;
			decimal expectedValueImproperThirteenFourths = 3.25m;
			decimal expectedValueImproperNegativeNineFifths = -1.8m;
			decimal expectedValueLargeFraction = 0.0166077577905472695920433411m;

			decimal resultOneSixteenth = (decimal)oneSixteenth;
			decimal resultNegativeOneThird = (decimal)negativeOneThird;
			decimal resultImproperThirteenFourths = (decimal)improperThirteenFourths;
			decimal resultImproperNegativeNineFifths = (decimal)improperNegativeNineFifths;
			decimal resultLargeFraction = (decimal)largeRational;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth, "1/16");
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird, "-1/3");
			Assert.AreEqual(expectedValueImproperThirteenFourths, resultImproperThirteenFourths, "13/4");
			Assert.AreEqual(expectedValueImproperNegativeNineFifths, resultImproperNegativeNineFifths, "-9/5");
			Assert.AreEqual(expectedValueLargeFraction, resultLargeFraction, "(a large fraction)");
		}

		[Test]
		public void TestCastZeroFromFloat()
		{
			float zero = 0;
			MixedFraction result = (MixedFraction)zero;
			MixedFraction expectedValue = MixedFraction.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestCastZeroFromDouble()
		{
			double zero = 0;
			MixedFraction result = (MixedFraction)zero;
			MixedFraction expectedValue = MixedFraction.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestCastZeroFromDecimal()
		{
			decimal zero = 0;
			MixedFraction result = (MixedFraction)zero;
			MixedFraction expectedValue = MixedFraction.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestParseWholeNumber()
		{
			string toParse = "3";

			MixedFraction result = MixedFraction.Parse(toParse);
			MixedFraction expectedValue = new MixedFraction(3, 0, 1);

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestParseFraction()
		{
			string toParse = "1/3";

			MixedFraction result = MixedFraction.Parse(toParse);
			MixedFraction expectedValue = new MixedFraction(0, 1, 3);

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestParseMixedNumber()
		{
			string toParse = "-1 + 1/3";

			MixedFraction result = MixedFraction.Parse(toParse);
			MixedFraction expectedValue = new MixedFraction(-1, 1, 3);

			Assert.AreEqual(expectedValue, result);
		}


		[Test]
		public void TestUnaryPlusOperator()
		{
			MixedFraction before = -5;
			MixedFraction after = +(before);

			TestContext.WriteLine($"+({before}) = {after}");

			string result = after.ToString();
			string expectedValue = "-5";

			Assert.AreEqual(expectedValue, result, "The unary + operator is expected to return the value of its operand.");
		}

		[Test]
		public void TestUnaryMinusOperator()
		{
			MixedFraction before = 5;
			MixedFraction after = -(before);

			TestContext.WriteLine($"-({before}) = {after}");

			string result = after.ToString();
			string expectedValue = "-5";

			Assert.AreEqual(expectedValue, result, "The unary - operator is expected to compute the numeric negation of its operand.");
		}
	}
}
