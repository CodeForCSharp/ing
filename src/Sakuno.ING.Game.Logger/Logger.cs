﻿using System.Linq;
using Sakuno.ING.Game.Logger.Entities;
using Sakuno.ING.Game.Models;

namespace Sakuno.ING.Game.Logger
{
    internal class Logger
    {
        private readonly LoggerContext context;
        private readonly NavalBase navalBase;

        private ShipCreation shipCreation;
        private BuildingDockId lastBuildingDock;

        public Logger(LoggerContext context, IGameProvider provider, NavalBase navalBase)
        {
            this.context = context;
            this.navalBase = navalBase;

            provider.EquipmentCreated += (t, m) =>
            {
                this.context.EquipmentCreationTable.Add(new EquipmentCreation
                {
                    TimeStamp = t,
                    Consumption = m.Consumption,
                    EquipmentCreated = m.SelectedEquipentInfoId,
                    IsSuccess = m.IsSuccess,
                    AdmiralLevel = this.navalBase.Admiral.Leveling.Level,
                    Secretary = this.navalBase.Secretary.Info.Id,
                    SecretaryLevel = this.navalBase.Secretary.Leveling.Level
                });
                this.context.SaveChanges();
            };

            provider.ShipCreated += (t, m) =>
            {
                shipCreation = new ShipCreation
                {
                    TimeStamp = t,
                    Consumption = m.Consumption,
                    IsLSC = m.IsLSC,
                    AdmiralLevel = this.navalBase.Admiral.Leveling.Level,
                    Secretary = this.navalBase.Secretary.Info.Id,
                    SecretaryLevel = this.navalBase.Secretary.Leveling.Level,
                    EmptyDockCount = navalBase.BuildingDocks.Count(x => x.State == BuildingDockState.Empty)
                };
                lastBuildingDock = m.BuildingDockId;
            };

            provider.BuildingDockUpdated += (t, m) =>
            {
                if (shipCreation != null)
                {
                    shipCreation.ShipBuilt = m.Single(x => x.Id == lastBuildingDock).BuiltShipId;
                    this.context.ShipCreationTable.Add(shipCreation);
                    shipCreation = null;
                    lastBuildingDock = default;
                    this.context.SaveChanges();
                }
            };
        }
    }
}
