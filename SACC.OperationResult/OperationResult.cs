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

namespace SACC.OperationResult
{
	public class OperationResult<T>
	{
		public bool IsSuccessful => Errors.Count == 0;
		public bool IsUnsuccessful => !IsSuccessful;
		public List<Error> Errors { get; set; }
		public T ResultData { get; set; }

		public OperationResult()
		{
			Errors = new List<Error>();
		}

		public OperationResult(T resultData)
		{
			Errors = new List<Error>();
			ResultData = resultData;
		}

		public OperationResult(T resultData, Error error)
		{
			Errors = new List<Error>();
			Errors.Add(error);
			ResultData = resultData;
		}

		public OperationResult(T resultData, List<Error> errors)
		{
			Errors = new List<Error>();
			Errors.AddRange(errors);
			ResultData = resultData;
		}

		public OperationResult<T> AddResultError(Error error)
		{
			Errors.Add(error);
			return this;
		}

		public OperationResult<T> AddResultErrors(List<Error> errors)
		{
			Errors.AddRange(errors);
			return this;
		}
	}
}
