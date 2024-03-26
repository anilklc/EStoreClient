using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EStore.Services
{
    public class UploadImageResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

        public static UploadImageResult Fail(string errorMessage)
        {
            return new UploadImageResult { IsSuccess = false, ErrorMessage = errorMessage };
        }

        public static UploadImageResult Success()
        {
            return new UploadImageResult { IsSuccess = true };
        }
    }
}
