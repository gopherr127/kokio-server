using Kokio.Api.App.TestCaseSteps;
using System.Collections.Generic;

namespace Kokio.Api.App.TestCases
{
    public class TestCase : Entity
    {
        public string Name { get; set; }
        public TestCaseTypes Type { get; set; }
        public List<TestCaseStep> Steps { get; set; }
    }
}
