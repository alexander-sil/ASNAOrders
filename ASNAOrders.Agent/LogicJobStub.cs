using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASNAOrders.Agent
{
    internal class LogicJobStub : IJob
    {
        public LogicJobStub() 
        {

        }

        public void Execute(IJobExecutionContext context)
        {
            AgentService.Instance.LogicInst.Go();
        }
    }
}
