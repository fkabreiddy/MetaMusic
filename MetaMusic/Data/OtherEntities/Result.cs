﻿namespace MetaMusic.Data.OtherEntities
{
    public class Result<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }
    }

    public class Result
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        
    }
}
