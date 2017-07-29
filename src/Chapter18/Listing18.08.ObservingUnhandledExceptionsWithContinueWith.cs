﻿namespace AddisonWesley.Michaelis.EssentialCSharp.Chapter18.Listing18_08
{
    using System;
    using System.Threading.Tasks;

    public class ProgramObservingUnhandledExceptionsWithContinueWith
    {
        public static void Main()
        {
            bool parentTaskFaulted = false;
            Task task = new Task(() =>
                {
                    throw new InvalidOperationException();
                });
            Task continuationTask = task.ContinueWith(
                (antecedentTask) =>
                {
                    parentTaskFaulted =
                        antecedentTask.IsFaulted;
                }, TaskContinuationOptions.OnlyOnFaulted);
            task.Start();
            continuationTask.Wait();
            if (!parentTaskFaulted)
            {
                throw new Exception("Parent task should be faulted");
            }
            if (!task.IsFaulted)
            {
                throw new Exception("Task should be faulted");
            }

            task.Exception.Handle(eachException =>
            {
                Console.WriteLine(
                    $"ERROR: { eachException.Message }");
                return true;
            });
        }
    }
}
