using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BankAccountNS;

namespace BankTest
{
    [TestClass]
    public class UnitTest1
    {
        // unit test code  
        [TestMethod]
        public void Debit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 4.55;
            double expected = 7.44;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Debit(debitAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
        //unit test method  
        [TestMethod]
        public void Debit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            try
            {
                account.Debit(debitAmount);
            }
            catch (ArgumentOutOfRangeException e){
                StringAssert.Contains(e.Message, BankAccount.DebitAmountLessThanZeroMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }
        //unit test method  
        [TestMethod]
        public void Debit_WhenAmountMoreThanBalance_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double debitAmount = 15;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            try
            {
                account.Debit(debitAmount);
            }
            catch(ArgumentOutOfRangeException e) {
                StringAssert.Contains(e.Message, BankAccount.DebitAmountExceedsBalanceMessage);
                return;
            }
            Assert.Fail("No exception was thrown.");
        }
        [TestMethod]
        public void Credit_WithValidAmount_UpdatesBalance()
        {
            // arrange  
            double beginningBalance = 11.99;
            double creditAmount = 4.55;
            double expected = 16.54;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Credit(creditAmount);

            // assert  
            double actual = account.Balance;
            Assert.AreEqual(expected, actual, 0.001, "Account not credited correctly");
        }
        [TestMethod]
        public void Credit_AccountFrozen_ShouldThrowException()
        {
            // arrange  
            double beginningBalance = 11.99;
            double creditAmount = 5;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);
            account.ToggleFreeze();
            // act  
            try
            {
                account.Credit(creditAmount);
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Account frozen");
                return;
            }
            Assert.Fail("No exception was thrown.");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Credit_WhenAmountIsLessThanZero_ShouldThrowArgumentOutOfRange()
        {
            // arrange  
            double beginningBalance = 11.99;
            double creditAmount = -100.00;
            BankAccount account = new BankAccount("Mr. Bryan Walton", beginningBalance);

            // act  
            account.Credit(creditAmount);

        }
    }
}
