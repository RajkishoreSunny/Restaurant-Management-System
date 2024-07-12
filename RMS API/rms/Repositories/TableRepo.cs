using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Models.ReservationModel;
using Models.TableModel;
namespace Repositories.TableRepo
{
    public class TableRepository : ITableRepository
    {
        private readonly RMSDbContext _dbContext;
        private readonly IServiceProvider _serviceProvider;
        private Timer _timer;
        private static readonly TimeSpan ReservationDuration = TimeSpan.FromMinutes(1);
        public TableRepository(RMSDbContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        #region TableCrud
        public Table GetTableById(int id)
        {
            try
            {
                var table = _dbContext.Tables.FirstOrDefault(t => t.TableId == id);
                if(table != null)
                {
                    return table;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public List<Table> AvailableTables()
        {
            var tableList = _dbContext.Tables.Where(t => t.Status == "Available").ToList();
            if(tableList != null)
            {
                return tableList;
            }
            return null;
        }

        public List<Table> ListOfTables()
        {
            try
            {
                var tableList = _dbContext.Tables.ToList();
                if(tableList != null)
                {
                    return tableList;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Table UpdateTable(int Id, Table table)
        {
            try
            {
                var availableTable = _dbContext.Tables.FirstOrDefault(t => t.TableId == Id);
                if(availableTable != null)
                {
                    availableTable.SeatingCapacity = table.SeatingCapacity;
                    availableTable.Status = table.Status;
                    _dbContext.SaveChanges();
                    return availableTable;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Table DeleteTable(int Id)
        {
            try
            {
                var table = _dbContext.Tables.FirstOrDefault(x => x.TableId == Id);
                if(table != null)
                {
                    _dbContext.Tables.Remove(table);
                    _dbContext.SaveChanges();
                    return table;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region TableStatus
        public bool UpdateStatus(int Id)   //Table UpdateStatus Method for the booking time
        {
            var tableData = _dbContext.Tables.FirstOrDefault(x => x.TableId == Id);
            if(tableData != null)
            {
                tableData.Status = "Booked";
                return true;
            }
            return false;
        }

        public bool UpdateStatus(int Id, string Status)   //Table Update Status Method for the real time operation Controlled by Manager
        {
            var table = _dbContext.Tables.FirstOrDefault(t => t.TableId == Id);
            if(table != null)
            {
                table.Status = Status;
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
        #endregion

    }
}