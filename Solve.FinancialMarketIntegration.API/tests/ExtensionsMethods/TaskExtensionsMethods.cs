using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public static class TaskExtensions
{
    public static TResult WaitAndGetResult<TResult>(this Task<TResult> source)
    {
        source.Wait();
        return source.Result;
    }
}