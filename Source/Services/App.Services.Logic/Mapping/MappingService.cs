﻿namespace App.Services.Logic.Mapping
{
    using AutoMapper;

    public class MappingService : IMappingService
    {
        public T Map<T>(object source)
        {
            return Mapper.Map<T>(source);
        }
    }
}
