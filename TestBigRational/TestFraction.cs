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

		[TestMethod]
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

		[TestMethod]
		public void TestImproperSubtraction()
		{
			Fraction oneHalf = new Fraction(1, 2);
			Fraction oneSixth = new Fraction(1, 6);

			Fraction expectedValueOneThird = new Fraction(1, 3);

			Fraction resultOneThird = Fraction.Subtract(oneHalf, oneSixth);

			Assert.AreEqual(expectedValueOneThird, resultOneThird);
		}

		[TestMethod]
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

		[TestMethod]
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

		[TestMethod]
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

		[TestMethod]
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
			BigRational reducedNegative = Fraction.ReduceToProperFraction(new Fraction(-3, 2));
			BigRational reduced = Fraction.ReduceToProperFraction(new Fraction(16, 7));
			BigRational noChange = Fraction.ReduceToProperFraction(new Fraction(7, 16));

			BigRational expectedValueReducedNegative = new BigRational(-1, 1, 2);
			BigRational expectedValueReduced = new BigRational(2, 2, 7);
			BigRational expectedValueNoChange = new BigRational(0, 7, 16);

			Assert.AreEqual(expectedValueReducedNegative, reducedNegative);
			Assert.AreEqual(expectedValueReduced, reduced);
			Assert.AreEqual(expectedValueNoChange, noChange);
		}
	}
}
