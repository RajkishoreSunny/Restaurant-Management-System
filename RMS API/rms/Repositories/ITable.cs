using System;
using Models.TableModel;
namespace Repositories.TableRepo
{
    public interface ITableRepository
    {
        public List<Table> ListOfTables();
        public Table GetTableById(int id);
        public List<Table> AvailableTables();
        public Table UpdateTable(int Id, Table table);
        public Table DeleteTable(int Id);
        public bool UpdateStatus(int Id);
        public bool UpdateStatus(int Id, string Status);
    }
}