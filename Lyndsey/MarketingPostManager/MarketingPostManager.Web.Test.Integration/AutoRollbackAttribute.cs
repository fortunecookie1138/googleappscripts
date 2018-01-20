using System;
using System.Reflection;
using System.Transactions;
using Xunit.Sdk;

namespace MarketingPostManager.Web.Test.Integration
{
    /// <summary>
    ///     Apply this attribute to your test method to automatically create a <see cref="TransactionScope" />
    ///     that is rolled back when the test is finished.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class AutoRollbackAttribute : BeforeAfterTestAttribute
    {
        private TransactionScopeAsyncFlowOption _asyncFlowOption = TransactionScopeAsyncFlowOption.Enabled;

        private IsolationLevel _isolationLevel = IsolationLevel.Unspecified;
        private TransactionScope _scope;

        private TransactionScopeOption _scopeOption = TransactionScopeOption.Required;

        private long _timeoutInMS = -1;

        /// <summary>
        ///     Gets or sets whether transaction flow across thread continuations is enabled for TransactionScope.
        ///     By default transaction flow across thread continuations is enabled.
        /// </summary>
        public TransactionScopeAsyncFlowOption AsyncFlowOption
        {
            get { return _asyncFlowOption; }
            set { _asyncFlowOption = value; }
        }

        /// <summary>
        ///     Gets or sets the isolation level of the transaction.
        ///     Default value is <see cref="IsolationLevel" />.Unspecified.
        /// </summary>
        public IsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
            set { _isolationLevel = value; }
        }

        /// <summary>
        ///     Gets or sets the _scope option for the transaction.
        ///     Default value is <see cref="TransactionScopeOption" />.Required.
        /// </summary>
        public TransactionScopeOption ScopeOption
        {
            get { return _scopeOption; }
            set { _scopeOption = value; }
        }

        /// <summary>
        ///     Gets or sets the timeout of the transaction, in milliseconds.
        ///     By default, the transaction will not timeout.
        /// </summary>
        public long TimeoutInMS
        {
            get { return _timeoutInMS; }
            set { _timeoutInMS = value; }
        }

        /// <summary>
        ///     Rolls back the transaction.
        /// </summary>
        public override void After(MethodInfo methodUnderTest)
        {
            _scope.Dispose();
        }

        /// <summary>
        ///     Creates the transaction.
        /// </summary>
        public override void Before(MethodInfo methodUnderTest)
        {
            var options = new TransactionOptions { IsolationLevel = IsolationLevel };
            if (TimeoutInMS > 0)
                options.Timeout = TimeSpan.FromMilliseconds(TimeoutInMS);

            _scope = new TransactionScope(ScopeOption, options, AsyncFlowOption);
        }
    }
}