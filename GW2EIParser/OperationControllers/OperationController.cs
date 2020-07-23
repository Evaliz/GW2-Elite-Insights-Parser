﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using GW2EIParser.Exceptions;

namespace GW2EIParser
{
    public enum OperationState
    {
        Ready = 0,
        Parsing = 1,
        Cancelling = 2,
        Complete = 3,
        Pending = 4,
        ClearOnCancel = 5,
        Queued = 6,
    }

    public abstract class OperationController
    {
        /// <summary>
        /// Location of the file being parsed
        /// </summary>
        public string Location { get; }
        /// <summary>
        /// Location of the generated files
        /// </summary>
        public List<string> GeneratedFiles { get; }
        /// <summary>
        /// Files/directories to open when open button is clicked
        /// </summary>
        public HashSet<string> PathsToOpen { get; }
        /// <summary>
        /// Status of the parse operation
        /// </summary>
        public string Status { get; protected set; }
        /// <summary>
        /// State of the button
        /// </summary>
        public string ButtonText { get; protected set; }
        /// <summary>
        /// Operation state
        /// </summary>
        public OperationState State { get; protected set; }

        protected List<string> StatusList { get; }

        public OperationController(string location, string status)
        {
            Location = location;
            Status = status;
            ButtonText = "分析";
            State = OperationState.Ready;
            StatusList = new List<string>();
            GeneratedFiles = new List<string>();
            PathsToOpen = new HashSet<string>();
        }

        protected virtual void ThrowIfCanceled()
        {

        }

        public void WriteLogMessages(StreamWriter sw)
        {
            foreach (string str in StatusList)
            {
                sw.WriteLine(str);
            }
        }

        public virtual void UpdateProgressWithCancellationCheck(string status)
        {
            UpdateProgress(status);
            ThrowIfCanceled();
        }
        public void UpdateProgress(string status)
        {
            StatusList.Add(status);
        }

        public void FinalizeStatus(string prefix)
        {
            Status = StatusList.LastOrDefault() ?? "";
            foreach (string generatedFile in GeneratedFiles)
            {
                Console.WriteLine("Generated" +$": {generatedFile}" + Environment.NewLine);
            }
            Console.WriteLine(prefix + $"{Location}: {Status}" + Environment.NewLine);
        }
    }
}
