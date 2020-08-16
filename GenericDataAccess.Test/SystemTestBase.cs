using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GenericDataAccess.Test
{
    public abstract class SystemTestBase
    {
        protected string Output { get; private set; }

        protected int ExitCode { get; private set; }

        [TestInitialize]
        public async Task InitAsync()
        {
            BeforeExecution();

            await using var sw = new StringWriter();
            Console.SetOut(sw);

            // string[] strings = CollectConsoleParameters().ToArray();
            ExitCode = await Program.Main();
            Output = sw.ToString();

            AfterExecution();
        }

        protected virtual void BeforeExecution()
        {
        }

        protected abstract IEnumerable<string> CollectConsoleParameters();

        protected virtual void AfterExecution()
        {
        }
    }
}