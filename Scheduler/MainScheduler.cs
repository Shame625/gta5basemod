using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Database;
using MiddlewareNamespace;
using Quartz;
using Quartz.Impl;

namespace SchedulerNamespace
{
    public class MainScheduler
    {

        readonly ISchedulerFactory _schedFact = new Quartz.Impl.StdSchedulerFactory();
        public IScheduler _sched;
        public MainScheduler(string connectionString = "")
        {
            //Debug.WriteLine("Scheduling jobs");
            _sched = _schedFact.GetScheduler();
            LoadJobs(connectionString);
            _sched.Start();
            MessagesManager.Instance.DebugMessage("Scheduler started!");
        }

        private void LoadJobs(string cs)
        {
            var myAssembly = Assembly.GetExecutingAssembly();
            var jobsTypes = myAssembly.GetTypes().Where(m => m.IsClass && m.GetInterface("IJob") != null);

            var Triggers = new List<Database.Entity.Trigger>();

            using(var dbContext = new ApplicationDbContext(cs))
            {
                Triggers = dbContext.Triggers.ToList();
            }
            MessagesManager.Instance.DebugMessage("Loading jobs Started!");
            foreach (var JobType in jobsTypes)
            {
                var i = 0;
                foreach (var myTrigger in GetCronTriggers(JobType.Name, Triggers))
                {
                    var job = new JobDetail(JobType.Name, "Grupa" + i, JobType);
                    myTrigger.Group = job.Group;
                    _sched.ScheduleJob(job, myTrigger);
                    i++;

                    MessagesManager.Instance.DebugMessage($"Loaded {job.Name}");
                }
            }
            MessagesManager.Instance.DebugMessage("Loading jobs Finished!");
        }
        private static CronTrigger GetTrigger(string triggerName, string cronExpression)
        {
            return new CronTrigger(triggerName, "Group" + triggerName, cronExpression);
        }

        public static IEnumerable<Trigger> GetCronTriggers(string jobName, List<Database.Entity.Trigger> triggers)
        {
            var rtnTrigers = new List<Trigger>();

            foreach (var triger in triggers.Where(o => o.JobName == jobName))
            {
                var tr = GetTrigger(triger.TriggerName, triger.CronExpression);

                tr.Name = triger.TriggerName;
                rtnTrigers.Add(tr);
            }

            return rtnTrigers;
        }

    }
}
