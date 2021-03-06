﻿using System;
using Newtonsoft.Json;

namespace Sakuno.ING.Game.Json.Converters
{
    internal class IdConverter<TId, TUnderlying> : JsonConverter
        where TId : struct, IComparable<TId>
    {
        private readonly Func<TUnderlying, TId> cast;
        private readonly Type nullableType = typeof(TId?);
        public IdConverter()
        {
            var op = typeof(TId).GetMethod("op_Explicit", new Type[] { typeof(TUnderlying) });
            cast = (Func<TUnderlying, TId>)op.CreateDelegate(typeof(Func<TUnderlying, TId>));
        }

        public override bool CanConvert(Type objectType) => true;
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = cast((TUnderlying)Convert.ChangeType(reader.Value, typeof(TUnderlying)));
            if (value.CompareTo(default) <= 0)
                if (objectType == nullableType)
                    return null;
                else
                    throw new ArgumentOutOfRangeException("The id must be valid.");
            return value;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotSupportedException();
    }
}
