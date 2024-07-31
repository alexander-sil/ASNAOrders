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

        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(AgentService.Instance.LogicInst.Go);
        }
    }
}
