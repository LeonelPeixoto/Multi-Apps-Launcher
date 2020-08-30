﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAppsLauncher
{
    /// <summary>
    /// Describe an application with the path of the executable and the arguments.
    /// </summary>
    class ApplicationInfo
    {
        /// <summary>
        /// Path of the application executable.
        /// </summary>
        public string applicationPath { get; set; }
        /// <summary>
        /// Arguments of the application.
        /// </summary>
        public string applicationArguments { get; set; }

        /// <summary>
        /// Application descriptor constructor.
        /// The path of the application executable and the arguments will be empty.
        /// </summary>
        public ApplicationInfo()
        {
            applicationPath = "";
            applicationArguments = "";
        }

        /// <summary>
        /// Application descriptor constructor.
        /// </summary>
        /// <param name="applicationPath">Path of the application executable.</param>
        /// <param name="applicationArguments">Arguments of the application.</param>
        public ApplicationInfo(string applicationPath, string applicationArguments = "")
        {
            this.applicationPath = applicationPath;
            this.applicationArguments = applicationArguments;
        }

        override
        public string ToString()
        {
            return applicationPath + "|" + applicationArguments;
        }
        
        public ApplicationInfo Clone()
        {
            return new ApplicationInfo(this.applicationPath, this.applicationArguments);
        }
    }
}