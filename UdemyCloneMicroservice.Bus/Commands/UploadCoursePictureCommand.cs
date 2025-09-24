using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyCloneMicroservice.Bus.Commands
{
    public record UploadCoursePictureCommand(Guid courseId, Byte[] picture, string FileName);
}