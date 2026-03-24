using System;

namespace EZXception.Business
{
    /// <summary>
    /// Thrown when a workflow step cannot be executed due to unmet preconditions or a failed process.
    /// </summary>
    public class WorkflowException : BusinessRuleException
    {
        public string? WorkflowName { get; }
        public string? StepName { get; }

        public WorkflowException(string message, string? workflowName = null, string? stepName = null)
            : base(message)
        {
            WorkflowName = workflowName;
            StepName = stepName;
        }

        public WorkflowException(string message, Exception innerException, string? workflowName = null, string? stepName = null)
            : base(message, innerException)
        {
            WorkflowName = workflowName;
            StepName = stepName;
        }
    }
}
