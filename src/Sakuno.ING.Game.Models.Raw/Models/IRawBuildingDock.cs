﻿using System;
using Sakuno.ING.Game.Models.MasterData;

namespace Sakuno.ING.Game.Models
{
    public readonly struct BuildingDockId : IEquatable<BuildingDockId>, IComparable<BuildingDockId>
    {
        private readonly int value;
        public BuildingDockId(int value) => this.value = value;

        public int CompareTo(BuildingDockId other) => value - other.value;
        public bool Equals(BuildingDockId other) => value == other.value;

        public static implicit operator int(BuildingDockId id) => id.value;
        public static explicit operator BuildingDockId(long value) => new BuildingDockId((int)value);

        public override string ToString() => value.ToString();
    }

    public interface IRawBuildingDock : IIdentifiable<BuildingDockId>
    {
        DateTimeOffset CompletionTime { get; }
        BuildingDockState State { get; }
        Materials Consumption { get; }
        ShipInfoId? BuiltShipId { get; }
        bool IsLSC { get; }
    }

    public enum BuildingDockState
    {
        Locked = -1,
        Empty = 0,
        Building = 2,
        BuildCompleted = 3,
    }
}
