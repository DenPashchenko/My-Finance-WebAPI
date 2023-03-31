using Microsoft.AspNetCore.Mvc;
using MyFinance.Application.Reports.Queries.ReportForDate;
using MyFinance.Application.Reports.Queries.ReportForPeriod;
using MyFinanceWebApi.Controllers.Abstractions;

namespace MyFinanceWebApi.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : BaseController
    {
        /// <summary>
        /// Gets the report for the time period
        /// </summary>
        [HttpGet("period_report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReportForPeriodVm>> GetReportForPeriodAsync([FromQuery(Name = "start_date")] string startDate, 
                                                                                   [FromQuery(Name = "end_date")] string endDate)
        {
            var query = new GetReportForPeriodQuery(startDate, endDate);
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }

        /// <summary>
        /// Gets the report for the specific date
        /// </summary>
        [HttpGet("date_report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ReportForDateVm>> GetReportForDateAsync([FromQuery(Name = "date")] string date)
        {
            var query = new GetReportForDateQuery(date);
            var viewModel = await Mediator.Send(query);

            return Ok(viewModel);
        }
    }
}
