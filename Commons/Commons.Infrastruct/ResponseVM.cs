using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Infrastruct
{
    public class BaseResponseVM
    {
        public int StatusCode { get; set; }
        public List<string> ErrorInfos { get; set; }
    }

    public class HasBodyResponseVM<T> : BaseResponseVM
    {
        public HasBodyResponseVM()
        {

        }
        public HasBodyResponseVM(T body)
        {
            Body = body;
        }
        public T Body { get; set; }
    }
}
