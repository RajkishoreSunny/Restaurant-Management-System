using System;
using Repositories.TableRepo;
using Models.TableModel;
namespace Services.TableSvc
{
    public class TableService
    {
        public readonly ITableRepository _tableRepository;
        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }
        public Table GetTableById(int Id)
        {
            return _tableRepository.GetTableById(Id);
        }
        public List<Table> ListOfTables()
        {
            return _tableRepository.ListOfTables();
        }
        public List<Table> AvailableTables()
        {
            return _tableRepository.AvailableTables();
        }
        public Table UpdateTable(int Id, Table table)
        {
            return _tableRepository.UpdateTable(Id, table);
        }
        public Table DeleteTable(int Id)
        {
            return _tableRepository.DeleteTable(Id);
        }
        public bool UpdateStatus(int Id)
        {
            return _tableRepository.UpdateStatus(Id);
        }
        public bool UpdateStatus(int Id, string Status)
        {
            return _tableRepository.UpdateStatus(Id, Status);
        }
    }
}