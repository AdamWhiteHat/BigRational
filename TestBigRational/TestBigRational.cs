using System;
using System.Diagnostics;
using System.Numerics;
using ExtendedNumerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestBigRational
{
	[TestClass]
	public class TestBigRational
	{
		[TestMethod]
		public void TestAddition()
		{
			BigRational threeHalfs = new BigRational(BigInteger.Zero, new Fraction(3, 2));
			BigRational tenEighths = new BigRational(BigInteger.Zero, new Fraction(10, 8));

			BigRational oneHundred = new BigRational(100);
			BigRational oneHalf = new BigRational(BigInteger.Zero, new Fraction(1, 2));

			BigRational expected1 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(11, 4)));
			BigRational expected2 = BigRational.Reduce(new BigRational(BigInteger.Zero, new Fraction(201, 2)));

			BigRational result1 = BigRational.Add(threeHalfs, tenEighths);
			BigRational result2 = BigRational.Add(oneHundred, oneHalf);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected2, result2);
		}

		[TestMethod]
		public void TestSubtraction()
		{
			BigRational sevenTwoths = new BigRational(3, 1, 2);
			BigRational sevenFifths = new BigRational(1, 2, 5);

			BigRational expected = new BigRational(2, 1, 10);
			BigRational result = BigRational.Subtract(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestMultiplication()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 49, 10));
			BigRational result = BigRational.Multiply(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}


		[TestMethod]
		public void TestDivisionNegative()
		{
			BigRational expected = BigRational.One;
			BigRational result = BigRational.Divide(BigRational.MinusOne, BigRational.MinusOne);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestDivision()
		{
			BigRational sevenTwoths = new BigRational(BigInteger.Zero, new Fraction(7, 2));
			BigRational sevenFifths = new BigRational(BigInteger.Zero, new Fraction(7, 5));

			BigRational expected = BigRational.Reduce(new BigRational(BigInteger.Zero, 5, 2));
			BigRational result = BigRational.Divide(sevenTwoths, sevenFifths);

			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void TestConvertFromDecimalPointDouble()
		{
			Double fifteenSixteenths = 0.9375d;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConvertFromWholeNumberDouble()
		{
			Double seven = 7.0d;

			BigRational expectedValue = new BigRational(7, new Fraction(0, 1));

			BigRational result1 = new BigRational(seven);
			BigRational result2 = (BigRational)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[TestMethod]
		public void TestConvertFromNegativeMixedDouble()
		{
			Double negativeOneAndOneThird = -4d / 3d;

			BigRational expectedValue = new BigRational(-1, new Fraction(1, 3));
			BigRational result = (BigRational)negativeOneAndOneThird;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConvertFromDecimalPointDecimal()
		{
			Decimal fifteenSixteenths = 0.9375m;

			BigRational expectedValue = new BigRational(BigInteger.Zero, new Fraction(15, 16));
			BigRational result = (BigRational)fifteenSixteenths;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConvertFromWholeNumberDecimal()
		{
			Decimal seven = 7.0m;

			BigRational expectedValue = new BigRational(7, new Fraction(0, 1));

			BigRational result1 = new BigRational(seven);
			BigRational result2 = (BigRational)seven;


			Assert.AreEqual(expectedValue, result1);
			Assert.AreEqual(expectedValue, result2);
		}

		[TestMethod]
		public void TestConvertFromNegativeMixedDecimal()
		{
			// Note that Decimal works best with a fixed number of decimal points
			// This will fail if we use the same numbers as used with the double test
			Decimal negativeOneAndOneHundredAndTwentyEigth = -129m / 128m;

			BigRational expectedValue = new BigRational(-1, new Fraction(1, 128));
			BigRational result = (BigRational)negativeOneAndOneHundredAndTwentyEigth;

			Assert.AreEqual(expectedValue, result);
		}

		[TestMethod]
		public void TestConstruction()
		{
			BigRational result1 = new BigRational(BigInteger.Zero, new Fraction(182, 26));
			BigRational result1_2 = new BigRational(new Fraction(182, 26));
			BigRational result2 = new BigRational(BigInteger.Zero, new Fraction(-7, 5));

			BigRational expected1 = new BigRational(7);
			BigRational expected2 = new BigRational(-1, 2, 5);

			Assert.AreEqual(expected1, result1);
			Assert.AreEqual(expected1, result1_2);
			Assert.AreEqual(expected2, result2);
		}

		[TestMethod]
		public void TestExpandImproperFraction()
		{
			BigRational threeAndOneThird = new BigRational(3, 1, 3);
			BigRational oneEightyTwoTwentySixths = new BigRational(new Fraction(182, 26));
			BigRational negativeThreeAndOneSeventh = new BigRational(-3, 1, 7);
			BigRational seven = new BigRational(7);

			Fraction expected313 = new Fraction(10, 3);
			Fraction expected18226 = new Fraction(91, 13);
			Fraction expectedNeg317 = new Fraction(-22, 7);
			Fraction expected7over1 = new Fraction(7, 1);

			Fraction result1 = threeAndOneThird.GetImproperFraction();
			Fraction result2 = oneEightyTwoTwentySixths.GetImproperFraction();
			Fraction result3 = negativeThreeAndOneSeventh.GetImproperFraction();
			Fraction result7 = seven.GetImproperFraction();


			Assert.AreEqual(expected313, result1);
			Assert.AreEqual(expected18226, result2);
			Assert.AreEqual(expectedNeg317, result3);
			Assert.AreEqual(expected7over1, result7);
		}

		[TestMethod]
		public void TestMullersRecurrenceConvergesOnFive()
		{
			// Set an upper limit to the number of iterations to be tried
			int n = 100;

			// Precreate some constants to use in the calculations
			BigRational c108 = new BigRational(108);
			BigRational c815 = new BigRational(815);
			BigRational c1500 = new BigRational(1500);
			BigRational convergencePoint = new BigRational(5);

			// Seed the initial values
			BigRational X0 = new BigRational(4);
			BigRational X1 = new BigRational(new Fraction(17, 4));
			BigRational Xprevious = X0;
			BigRational Xn = X1;

			// Get the current distance to the convergence point, this should be constantly
			// decreasing with each iteration
			BigRational distanceToConvergence = BigRational.Subtract(convergencePoint, X1);

			int count = 1;
			for (int i = 1; i < n; ++i)
			{
				BigRational Xnext = c108 - (c815 - c1500 / Xprevious) / Xn;
				BigRational nextDistanceToConvergence = BigRational.Subtract(convergencePoint, Xnext);
				Assert.IsTrue(nextDistanceToConvergence < distanceToConvergence);

				Xprevious = Xn;
				Xn = Xnext;
				distanceToConvergence = nextDistanceToConvergence;
				if ((Double)Xn == 5d)
					break;
				++count;
			}
			Assert.AreEqual((Double)Xn, 5d);
			Assert.IsTrue(count == 70);
		}

	}
}
