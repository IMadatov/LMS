using System.Data;
using System.Data.Common;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace WebAPI.DatabaseExtensions;

/// <summary>
/// Monitor SQL execution time and log warnings if it exceeds the threshold.
/// </summary>
/// <remarks>
/// <para>
/// Should have <b>EntityFrameworkCore : EnableLangSqlExecutionWarning</b> flag (<see langword="bool"/>) enabled in appsettings.json
/// </para>
/// <para>
/// Should include <b>EntityFrameworkCore : WarningThresholdMilliseconds</b> key value (<see langword="int"/>) in appsettings.json
/// </para>
/// </remarks>
public class LongExecutionWarningInterceptor : IDbCommandInterceptor
{
    private static int _sqlWarningThresholdMs;

    private readonly Stopwatch _stopwatch = new();

    private readonly bool _enabled;

    public LongExecutionWarningInterceptor(
        IConfiguration configuration
    )
    {
        _enabled = Convert.ToBoolean(configuration["EntityFrameworkCore:EnableLangSqlExecutionWarning"]);

        if (!_enabled)
            return;

        var threshold = configuration["EntityFrameworkCore:WarningThresholdMilliseconds"];

        if (string.IsNullOrEmpty(threshold))
            _enabled = false;

        var thresholdValue = Convert.ToInt32(threshold);

        _sqlWarningThresholdMs = thresholdValue is < 50 or > 1000 ? thresholdValue : 400;
    }



    #region CommandExecuting

    public InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> interceptionContext)
    {
        CommandExecuting();

        return interceptionContext;
    }

    public InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> interceptionContext)
    {
        CommandExecuting();

        return interceptionContext;
    }

    public InterceptionResult<object> ScalarExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<object> interceptionContext)
    {
        CommandExecuting();

        return interceptionContext;
    }

    public ValueTask<InterceptionResult<DbDataReader>> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result,
        CancellationToken cancellationToken = new())
    {
        CommandExecuting();

        return ValueTask.FromResult(result);
    }

    public ValueTask<InterceptionResult<object>> ScalarExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<object> result,
        CancellationToken cancellationToken = new())
    {
        CommandExecuting();

        return ValueTask.FromResult(result);
    }

    public ValueTask<InterceptionResult<int>> NonQueryExecutingAsync(DbCommand command, CommandEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        CommandExecuting();

        return ValueTask.FromResult(result);
    }

    #endregion



    #region CommandExecuted

    public DbDataReader ReaderExecuted(DbCommand command, CommandExecutedEventData eventData, DbDataReader result)
    {
        CommandExecuted(command);

        return result;
    }

    public object? ScalarExecuted(DbCommand command, CommandExecutedEventData eventData, object? result)
    {
        CommandExecuted(command);

        return result;
    }

    public int NonQueryExecuted(DbCommand command, CommandExecutedEventData eventData, int result)
    {
        CommandExecuted(command);

        return result;
    }

    public ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result,
        CancellationToken cancellationToken = new())
    {
        CommandExecuted(command);

        return ValueTask.FromResult(result);
    }

    public ValueTask<object?> ScalarExecutedAsync(DbCommand command, CommandExecutedEventData eventData, object? result,
        CancellationToken cancellationToken = new())
    {
        CommandExecuted(command);

        return ValueTask.FromResult(result);
    }

    public ValueTask<int> NonQueryExecutedAsync(DbCommand command, CommandExecutedEventData eventData, int result,
        CancellationToken cancellationToken = new())
    {
        CommandExecuted(command);

        return ValueTask.FromResult(result);
    }

    #endregion



    #region CommandFailed

    public void CommandFailed(DbCommand command, CommandErrorEventData eventData)
    {

    }

    public Task CommandFailedAsync(DbCommand command, CommandErrorEventData eventData,
        CancellationToken cancellationToken = new())
    {
        return Task.CompletedTask;
    }

    #endregion



    #region CreatingAndDispose

    public InterceptionResult<DbCommand> CommandCreating(CommandCorrelatedEventData eventData, InterceptionResult<DbCommand> result)
    {
        return result;
    }

    public DbCommand CommandCreated(CommandEndEventData eventData, DbCommand result)
    {
        return result;
    }

    public InterceptionResult DataReaderDisposing(DbCommand command, DataReaderDisposingEventData eventData,
        InterceptionResult result)
    {
        return result;
    }

    #endregion



    #region Private-Methods

    private void CommandExecuting()
    {
        _stopwatch.Restart();
    }

    private void CommandExecuted(IDbCommand command)
    {
        _stopwatch.Stop();

        if (_enabled)
            LogIfTooSlow(command, _stopwatch.Elapsed);
    }

    private static void LogIfTooSlow(IDbCommand command, TimeSpan completionTime)
    {
        if (completionTime.TotalMilliseconds > _sqlWarningThresholdMs)
        {
            Log.Warning("Query time ({0}ms) exceeded the threshold of {1}ms. Command: \"{2}\"",
                completionTime.TotalMilliseconds, _sqlWarningThresholdMs, command.CommandText);
        }
    }

    #endregion
}