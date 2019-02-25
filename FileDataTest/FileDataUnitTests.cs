using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileData;
using Microsoft.QualityTools.Testing.Fakes;

namespace FileDataTest
{
    [TestClass]
    public class FileDataUnitTests
    {
        private readonly FileDataUtility _fileUtility;
        string[] args;

        public FileDataUnitTests()
        {
            _fileUtility = new FileDataUtility();
        }
        [TestMethod]
        public void TestValidate()
        {
            using (ShimsContext.Create())
            {
                args =new string[] { "-v", "c:/test.txt" };
                FileData.Fakes.ShimFileDataUtility.AllInstances.GetArgumentGroupString = (a, b) => { return new string[] { "-v" }; };
                FileDataResult result = _fileUtility.Validate(args);
                Assert.IsTrue(result.Status);
            }
        }

        [TestMethod]
        public void TestValidateForNoArgs()
        {
            using (ShimsContext.Create())
            {
                args=new string[] { };
                FileData.Fakes.ShimFileDataUtility.AllInstances.GetArgumentGroupString = (a, b) => { return new string[] { "string" }; };
                FileDataResult result = _fileUtility.Validate(args);
                Assert.IsFalse(result.Status);
            }
        }

        [TestMethod]
        public void TestValidateForInvalidArgs()
        {
            using (ShimsContext.Create())
            {
                args = new string[] {"a"};
                FileData.Fakes.ShimFileDataUtility.AllInstances.GetArgumentGroupString = (a, b) => { return new string[] { "string" }; };
                FileDataResult result = _fileUtility.Validate(args);
                Assert.IsFalse(result.Status);
            }
        }

        [TestMethod]
        public void TestExecuteForVersion()
        {
            using (ShimsContext.Create())
            {
                args = new string[] { "-v","c:\test.txt"};
                FileData.Fakes.ShimFileDataUtility.AllInstances.GetArgumentGroupString = (a, b) => { return new string[] { "string" }; };
                FileDataResult result = _fileUtility.Execute(args);
                Assert.IsFalse(result.Status);
            }
        }

        [TestMethod]
        public void TestExecuteForSize()
        {
            using (ShimsContext.Create())
            {
                args = new string[] { "-s","c:\test.txt" };
                FileData.Fakes.ShimFileDataUtility.AllInstances.GetArgumentGroupString = (a, b) => { return new string[] { "string" }; };
                FileDataResult result = _fileUtility.Execute(args);
                Assert.IsFalse(result.Status);
            }
        }
    }
}
