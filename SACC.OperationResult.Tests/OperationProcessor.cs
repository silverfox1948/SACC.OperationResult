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

using System.Collections.Generic;

namespace SACC.OperationResult.Tests
{
	public class OperationProcessor
	{
		#region NullResultData Object
		public static OperationResult<NullResultData> ReturnNullDataNoErrors()
		{
			return new OperationResult<NullResultData>();
		}

		private static Error MakeError(int number)
		{
			return new Error(string.Format("Message #{0}", number.ToString()));
		}

		public static OperationResult<NullResultData> ReturnNullDataSingleError()
		{
			var result = new OperationResult<NullResultData>();
			result.Errors.Add(MakeError(1));
			return result;
		}

		public static OperationResult<NullResultData> ReturnNullDataMultipleErrors(int errorCount)
		{
			var result = new OperationResult<NullResultData>();
			for (int i = 0; i < errorCount; i++)
			{
				result.Errors.Add(MakeError(i + 1));
			}
			return result;
		}

		public static OperationResult<NullResultData> ReturnNullDataErrorRange(int errorCount)
		{
			var result = new OperationResult<NullResultData>();
			var errors = new List<Error>();
			for (int i = 0; i < errorCount; i++)
			{
				errors.Add(MakeError(i + 1));
			}
			result.Errors.AddRange(errors);
			return result;
		}
		#endregion

		#region TestResultData Object
		public static OperationResult<TestResultData> ReturnDataNoErrors()
		{
			var result = new OperationResult<TestResultData>();
			result.ResultData = new TestResultData();
			return result;
		}

		public static OperationResult<TestResultData> ReturnDataSingleError()
		{
			var result = new OperationResult<TestResultData>();
			result.Errors.Add(MakeError(1));
			return result;
		}

		public static OperationResult<TestResultData> ReturnDataMultipleErrors(int errorCount)
		{
			var result = new OperationResult<TestResultData>();
			for (int i = 0; i < errorCount; i++)
			{
				result.Errors.Add(MakeError(i + 1));
			}
			return result;
		}

		public static OperationResult<TestResultData> ReturnDataErrorRange(int errorCount)
		{
			var result = new OperationResult<TestResultData>();
			var errors = new List<Error>();
			for (int i = 0; i < errorCount; i++)
			{
				errors.Add(MakeError(i + 1));
			}
			result.Errors.AddRange(errors);
			return result;
		}
		#endregion

		#region Value Type
		public static OperationResult<int> ReturnTypeNoErrors()
		{
			var result = new OperationResult<int>();
			return result;
		}

		public static OperationResult<int> ReturnTypeSingleError()
		{
			var result = new OperationResult<int>();
			result.Errors.Add(MakeError(1));
			return result;
		}

		public static OperationResult<int> ReturnTypeMultipleErrors(int errorCount)
		{
			var result = new OperationResult<int>();
			for (int i = 0; i < errorCount; i++)
			{
				result.Errors.Add(MakeError(i + 1));
			}
			return result;
		}

		public static OperationResult<int> ReturnTypeErrorRange(int errorCount)
		{
			var result = new OperationResult<int>();
			var errors = new List<Error>();
			for (int i = 0; i < errorCount; i++)
			{
				errors.Add(MakeError(i + 1));
			}
			result.Errors.AddRange(errors);
			return result;
		}
		#endregion
	}
}
