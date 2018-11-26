using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Dtos
{
    public class FileModel
    {
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}
