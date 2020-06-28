using System;
using System.Numerics;
using ExtendedNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBigRational
{
	[TestClass]
	public class TestFractionArithmetic
	{
		public TestContext TestContext { get { return m_testContext; } set { m_testContext = value; } }
		private TestContext m_testContext;

		[TestMethod, TestCategory("Arithmetic")]
		public void TestAddition()
		{
			Fraction oneThird = new Fraction(1, 3);
			Fraction oneFifth = new Fraction(1, 5);

			Fraction ninety = new Fraction(90 / 1);

			Fraction expectedValueEightFifteenths = new Fraction(8, 15);
			Fraction expectedValueThirtysixTwentyfifths = new Fraction(2, 15);

			Fraction expected271Thirds = new Fraction(271, 3);

			Fraction resultEightFifteenths = Fraction.Add(oneThird, oneFifth);
			Fraction resultThirtysixTwentyfifths = Fraction.Add(oneThird, Fraction.Negate(oneFifth));

			Fraction result271Thirds = Fraction.Add(ninety, oneThird);

			Assert.AreEqual(expectedValueEightFifteenths, resultEightFifteenths);
			Assert.AreEqual(expectedValueThirtysixTwentyfifths, resultThirtysixTwentyfifths);
			Assert.AreEqual(expected271Thirds, result271Thirds);
		}

		[TestMethod, TestCategory("Arithmetic")]
		public void TestSubtraction()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValueOneThird = new Fraction(1, 3);
			Fraction expectedValueNegativeTwoThirds = new Fraction(2, 3);

			Fraction resultOneThird = Fraction.Subtract(oneHalf, oneSixth);
			Fraction resultNegativeTwoThirds = Fraction.Subtract(oneHalf, Fraction.Negate(oneSixth));

			Assert.AreEqual(expectedValueOneThird, resultOneThird);
			Assert.AreEqual(expectedValueNegativeTwoThirds, resultNegativeTwoThirds);
		}

		[TestMethod, TestCategory("Arithmetic")]
		public void TestImproperSubtraction()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValueOneThird = new Fraction(1, 3);

			Fraction resultOneThird = Fraction.Subtract(oneHalf, oneSixth);

			Assert.AreEqual(expectedValueOneThird, resultOneThird);
		}

		[TestMethod, TestCategory("Arithmetic")]
		public void TestMultiplication()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction twoFifths = new Fraction(2, 5);

			Fraction expectedValueOneFifth = new Fraction(1, 5);
			Fraction expectedValueNegativeFourTwentyFifths = new Fraction(-4, 25);

			Fraction resultOneFifth = Fraction.Multiply(oneHalf, twoFifths);
			Fraction resultNegativeFourTwentyFifths = Fraction.Multiply(twoFifths, Fraction.Negate(twoFifths));

			Assert.AreEqual(expectedValueOneFifth, resultOneFifth);
			Assert.AreEqual(expectedValueNegativeFourTwentyFifths, resultNegativeFourTwentyFifths);
		}

		[TestMethod, TestCategory("Arithmetic")]
		public void TestDivision()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);
			Fraction negativeOneSixth = new Fraction(-1, 6);

			Fraction expectedValueThree = new Fraction(3, 1);
			Fraction expectedValueOneThird = new Fraction(1, 3);
			Fraction expectedValueNegativeThree = new Fraction(-3, 1);
			Fraction expectedValueNegativeOneThird = new Fraction(-1, 3);

			Fraction resultThree = Fraction.Divide(oneHalf, oneSixth);
			Fraction resultNegativeThree = Fraction.Divide(oneHalf, negativeOneSixth);
			Fraction resultOneThird = Fraction.Divide(oneSixth, oneHalf);
			Fraction resultNegativeOneThird = Fraction.Divide(oneSixth, Fraction.Negate(oneHalf));

			Assert.AreEqual(expectedValueThree, resultThree);
			Assert.AreEqual(expectedValueNegativeThree, resultNegativeThree);
			Assert.AreEqual(expectedValueOneThird, resultOneThird);
			Assert.AreEqual(expectedValueNegativeOneThird, resultNegativeOneThird);
		}

		[TestMethod, TestCategory("Arithmetic")]
		public void TestPow()
		{
			// (4/5)^2 == 16/25
			Fraction fourFifths = new Fraction(4, 5);

			Fraction expected = new Fraction(16, 25);
			Fraction result = Fraction.Pow(fourFifths, 2);

			Assert.AreEqual(expected, result);
		}
	}
}
