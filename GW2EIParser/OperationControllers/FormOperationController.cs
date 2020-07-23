﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using GW2EIParser.Exceptions;

namespace GW2EIParser
{
    public class FormOperationController : OperationController
    {

        private CancellationTokenSource _cancelTokenSource;

        private Task _task;

        private readonly DataGridView _dgv;

        public FormOperationController(string location, string status, DataGridView dgv) : base(location, status)
        {
            _dgv = dgv;
        }

        public void SetContext(CancellationTokenSource cancelTokenSource, Task task)
        {
            _cancelTokenSource = cancelTokenSource;
            _task = task;
        }

        public bool IsBusy()
        {
            if (_task != null)
            {
                return !_task.IsCompleted;
            }
            return false;
        }

        protected override void ThrowIfCanceled()
        {
            if (_task != null && _cancelTokenSource.IsCancellationRequested)
            {
                _cancelTokenSource.Token.ThrowIfCancellationRequested();
            }
        }

        private void InvalidateDataView()
        {
            if (_dgv.InvokeRequired)
            {
                _dgv.Invoke(new Action(() => _dgv.Invalidate()));
            }
            else
            {
                _dgv.Invalidate();
            }
        }

        public void ToRunState()
        {
            ButtonText = "取消";
            State = OperationState.Parsing;
            Status = "分析中...";
            InvalidateDataView();
        }

        public void ToCancelState()
        {
            if (_task == null)
            {
                return;
            }
            State = OperationState.Cancelling;
            ButtonText = "取消中";
            _cancelTokenSource.Cancel();
            InvalidateDataView();
        }
        public void ToRemovalFromQueueState()
        {
            ToCancelState();
            Status = "Awaiting Removal from Queue";
            InvalidateDataView();
        }
        public void ToCancelAndClearState()
        {
            ToCancelState();
            State = OperationState.ClearOnCancel;
        }
        public void ToReadyState()
        {
            State = OperationState.Ready;
            ButtonText = "分析";
            Status = "Ready To Parse";
            InvalidateDataView();
        }

        public void ToCompleteState()
        {
            State = OperationState.Complete;
            ButtonText = "打开";
            FinalizeStatus("Parsing Successful - ");
            InvalidateDataView();
        }

        public void ToUnCompleteState()
        {
            State = OperationState.Ready;
            ButtonText = "分析";
            FinalizeStatus("Parsing Failure - ");
            InvalidateDataView();
        }

        public void ToPendingState()
        {
            State = OperationState.Pending;
            ButtonText = "取消";
            Status = "Pending";
            InvalidateDataView();
        }

        public void ToQueuedState()
        {
            State = OperationState.Queued;
            ButtonText = "取消";
            Status = "Queued";
            InvalidateDataView();
        }
    }
}
