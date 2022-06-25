using FinalProject.Jobs;
using Quartz;
using Quartz.Spi;

namespace FinalProject.Services;

public class QuartzHostedService : IHostedService
{
    private readonly IJobFactory _jobFactory;
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IEnumerable<JobSchedule> _jobSchedules;

    public QuartzHostedService(
        IJobFactory jobFactory, 
        ISchedulerFactory schedulerFactory,
        IEnumerable<JobSchedule> jobSchedules)
    {
        _jobFactory = jobFactory;
        _schedulerFactory = schedulerFactory;
        _jobSchedules = jobSchedules;
    }
    public IScheduler Scheduler { get; set; } = null!;
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
        Scheduler.JobFactory = _jobFactory;
        foreach (var jobSchedule in _jobSchedules)
        {
            var job = CreateJobDetail(jobSchedule);
            var trigger = CreateTrigger(jobSchedule);
            await Scheduler.ScheduleJob(job, trigger, cancellationToken);
        }
        await Scheduler.Start(cancellationToken);
    }

    private static ITrigger CreateTrigger(JobSchedule jobSchedule)
    {
        return TriggerBuilder
            .Create()
            .WithIdentity($"{jobSchedule.JobType.FullName}.trigger")
            .WithCronSchedule(jobSchedule.CronExpression)
            .WithDescription(jobSchedule.CronExpression)
            .Build();
    }

    private static IJobDetail CreateJobDetail(JobSchedule jobSchedule)
    {
        var jobType = jobSchedule.JobType;
        if (jobType.FullName is null)
        {
            throw new NullReferenceException("Job Type Name is null.");
        }
        return JobBuilder
            .Create(jobType)
            .WithIdentity(jobType.FullName)
            .WithDescription(jobType.Name)
            .Build();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Scheduler.Shutdown(cancellationToken);
    }
}
