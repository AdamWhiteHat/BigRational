using System;
using System.Numerics;
using ExtendedNumerics;
using NUnit.Framework;

namespace TestBigRational
{
	[TestFixture(Category = "Conversions")]
	public class TestFractionConversions
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[Test]
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

		[Test]
		public void TestConvertToDouble()
		{
			Fraction oneSixteenth = new Fraction(1, 16);
			Fraction negativeOneThird = new Fraction(-1, 3);
			Fraction improperThirteenFourths = new Fraction(13, 4);
			Fraction improperNegativeNineFifths = new Fraction(-9, 5);
			Fraction largeFraction = new Fraction(
				BigInteger.Parse("36979593578080793436251350559911534471745439536857510606701399088382738"),
				BigInteger.Parse("2226645766662654219495625670010961737131540370393163559325615188894568125"));

			double expectedValueOneSixteenth = 0.0625d;
			double expectedValueNegativeOneThird = -1d / 3d;
			double expectedValueImproperThirteenFourths = 3.25d;
			double expectedValueImproperNegativeNineFifths = -1.8d;
			double expectedValueLargeFraction = 0.016607757790547271d;

			double resultOneSixteenth = (double)oneSixteenth;
			double resultNegativeOneThird = (double)negativeOneThird;
			double resultImproperThirteenFourths = (double)improperThirteenFourths;
			double resultImproperNegativeNineFifths = (double)improperNegativeNineFifths;
			double resultLargeFraction = (double)largeFraction;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
			Assert.AreEqual(expectedValueImproperThirteenFourths, resultImproperThirteenFourths);
			Assert.AreEqual(expectedValueImproperNegativeNineFifths, resultImproperNegativeNineFifths);
			Assert.AreEqual(expectedValueLargeFraction, resultLargeFraction);
		}

		[Test]
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

		[Test]
		public void TestConvertToDecimal()
		{
			Fraction oneSixteenth = new Fraction(1, 16);
			Fraction negativeOneThird = new Fraction(-1, 3);
			Fraction improperThirteenFourths = new Fraction(13, 4);
			Fraction improperNegativeNineFifths = new Fraction(-9, 5);
			Fraction largeFraction = new Fraction(
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
			decimal resultLargeFraction = (decimal)largeFraction;

			Assert.AreEqual(expectedValueOneSixteenth, resultOneSixteenth);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
			Assert.AreEqual(expectedValueImproperThirteenFourths, resultImproperThirteenFourths);
			Assert.AreEqual(expectedValueImproperNegativeNineFifths, resultImproperNegativeNineFifths);
			Assert.AreEqual(expectedValueLargeFraction, resultLargeFraction);
		}

		[Test]
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

		[Test]
		public void TestCastZeroFromDouble()
		{
			double zero = 0;
			Fraction result = (Fraction)zero;
			Fraction expectedValue = Fraction.Zero;

			Assert.AreEqual(expectedValue, result);
		}

		[Test]
		public void TestCastZeroFromDecimal()
		{
			decimal zero = 0;
			Fraction result = (Fraction)zero;
			Fraction expectedValue = Fraction.Zero;

			Assert.AreEqual(expectedValue, result);
		}
	}
}
