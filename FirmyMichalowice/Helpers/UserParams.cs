using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Helpers
{
    public class UserParams
    {
		public const int MaxPageSize = 24;
		public int PageNumber { get; set; } = 1;

		private int pageSize = 10;

		public int PageSize
		{
			get { return pageSize; }
			set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
		}


	}
}
