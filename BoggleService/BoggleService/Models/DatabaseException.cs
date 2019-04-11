using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoggleService.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class DatabaseException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public DatabaseException(string message) : base(message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public DatabaseException() : base()
        {

        }
    }
}