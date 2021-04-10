using System;

namespace C4Sharp.FileSystem
{
    public class C4FileException: Exception
    {
        public C4FileException(string message):base(message)
        {
            
        }
        
        public C4FileException(string message, Exception innerException):base(message, innerException)
        {
            
        }
    }
}