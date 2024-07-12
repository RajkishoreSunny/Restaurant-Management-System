using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models.TableModel;
using Services.TableSvc;
namespace Controller.TableCntrl
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class TableController : ControllerBase
    {
        private readonly TableService _tableService;

        public TableController(TableService tableService)
        {
            _tableService = tableService;
        }
        [HttpGet("GetTableById")]
        public ActionResult<Table> GetTableById(int Id)
        {
            try
            {
                var table = _tableService.GetTableById(Id);
                if(table != null)
                {
                    return Ok(table);
                }
                return BadRequest();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("ListOfTables")]
        public ActionResult<List<Table>> ListOfTables()
        {
            try
            {
                var listOfTables = _tableService.ListOfTables();
                return Ok(listOfTables);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("AvailableTables")]
        public ActionResult<Table> AvailableTables()
        {
            try
            {
                var listOfTables = _tableService.AvailableTables();
                return Ok(listOfTables);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("UpdateTable")]
        public ActionResult<Table> UpdateTable(int id, [FromBody]Table table)
        {
            try
            {
                var availableTable = _tableService.UpdateTable(id, table);
                if(availableTable != null)
                {
                    return Ok(availableTable);
                }
                return NotFound();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpDelete("DeleteTable")]
        public ActionResult<Table> DeleteTable(int id)
        {
            try
            {
                var deleteTable = _tableService.DeleteTable(id);
                if(deleteTable != null)
                {
                    return Ok(deleteTable);
                }
                return NotFound();
            }
            catch
            {
                return Unauthorized();
            }
        }

        [HttpPost("UpdateStatus")]
        public ActionResult<bool> UpdateStatus(int Id)
        {
            try
            {
                var table = _tableService.UpdateStatus(Id);
                if(table)
                {
                    return Ok(true);
                }
                return false;
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("UpdateStatus/{Id}/{Status}")]
        public ActionResult<bool> UpdateStatus(int Id, string Status)
        {

            try
            {
                var table = _tableService.UpdateStatus(Id, Status);
                if(table)
                {
                    return Ok(true);
                }
                return false;
            }
            catch
            {
                return BadRequest();
            }
        }
    }
    
}