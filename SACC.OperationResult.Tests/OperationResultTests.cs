/*
Copyright 2017 Steven Archibald Consulting Corporation

Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

	 http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SACC.OperationResult.Tests
{
	[TestClass]
	public class OperationResultTests
	{
		[TestMethod]
		public void NoDataNoErrorsSucceeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnNullDataNoErrors().IsSuccessful);
		}

		[TestMethod]
		public void NoDataSingleErrorsSucceeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnNullDataSingleError().IsUnsuccessful);
		}

		[TestMethod]
		public void NoDataMultipleErrors1Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnNullDataMultipleErrors(1).IsUnsuccessful);
		}

		[TestMethod]
		public void NoDataMultipleErrors4Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnNullDataMultipleErrors(4).IsUnsuccessful);
		}

		[TestMethod]
		public void NoDataErrorRange1Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnNullDataErrorRange(1).IsUnsuccessful);
		}

		[TestMethod]
		public void NoDataErrorRange4Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnNullDataErrorRange(4).IsUnsuccessful);
		}

		[TestMethod]
		public void DataNoErrorsSucceeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnDataNoErrors().IsSuccessful);
			Assert.IsTrue(OperationProcessor.ReturnDataNoErrors().ResultData != null);
		}

		[TestMethod]
		public void DataSingleErrorsSucceeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnDataSingleError().IsUnsuccessful);
		}

		[TestMethod]
		public void DataMultipleErrors1Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnDataMultipleErrors(1).IsUnsuccessful);
		}

		[TestMethod]
		public void DataMultipleErrors4Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnDataMultipleErrors(4).IsUnsuccessful);
		}

		[TestMethod]
		public void DataErrorRange1Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnDataErrorRange(1).IsUnsuccessful);
		}

		[TestMethod]
		public void DataErrorRange4Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnDataErrorRange(4).IsUnsuccessful);
		}

		[TestMethod]
		public void TypeNoErrorsSucceeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnTypeNoErrors().IsSuccessful);
		}

		[TestMethod]
		public void TypeSingleErrorsSucceeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnTypeSingleError().IsUnsuccessful);
		}

		[TestMethod]
		public void TypeMultipleErrors1Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnTypeMultipleErrors(1).IsUnsuccessful);
		}

		[TestMethod]
		public void TypeMultipleErrors4Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnTypeMultipleErrors(4).IsUnsuccessful);
		}

		[TestMethod]
		public void TypeErrorRange1Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnTypeErrorRange(1).IsUnsuccessful);
		}

		[TestMethod]
		public void TypeErrorRange4Succeeds()
		{
			Assert.IsTrue(OperationProcessor.ReturnTypeErrorRange(4).IsUnsuccessful);
		}

		[TestMethod]
		public void FluentOperationsWork()
		{
			var result = new OperationResult<NullResultData>();
			if (result.AddResultErrors((OperationProcessor.ReturnNullDataMultipleErrors(2).Errors)).IsUnsuccessful) { Assert.IsTrue(true); }
		}
	}
}
