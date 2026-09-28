using Aspire.Context;
using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Aspire.APIs;

[Route("api/[controller]")]
[ApiController]
public class Attendance(ApplicationDbContext context, ITenantProviderService _tenantService) : ControllerBase
{
    Dictionary<string, List<AuditItemsTreeDTO>> lookups = [];
    string filteringOperation = string.Empty;
    List<string> allWorkActivityIds = new();

    [HttpPost("CheckInOut")]
    public async Task<IActionResult> CheckInOut()
    {
        try
        {
            long? tenantId = _tenantService.GetTenantId();
            long? userId = _tenantService.GetUserId();
            context.TimeAttendances.Add(new TimeAttendance(tenantId, userId));
            context.SaveChanges();
            return Ok();
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            long? tenantId = _tenantService.GetTenantId();
            long? userId = _tenantService.GetUserId();
            string Sql = "Select \"TimeAttendances\".\"AttendanceDate\"::date AS AttendanceDate, CAST(Min(\"TimeAttendances\".\"CheckInOutMinutes\") AS TEXT) AS \"CheckIn\", CAST(Max(\"TimeAttendances\".\"CheckInOutMinutes\") AS TEXT) AS \"CheckOut\", CAST(ROUND(((Max(\"TimeAttendances\".\"CheckInOutMinutes\") - Min(\"TimeAttendances\".\"CheckInOutMinutes\")) / 60)::numeric, 2) AS TEXT) AS \"Hours\" From \"TimeAttendances\" Where \"TimeAttendances\".\"AssignedToId\" = '" + userId + "' AND \"TimeAttendances\".\"TenantId\" = '" + tenantId + "' Group By \"TimeAttendances\".\"AttendanceDate\"::date";
            var Objs = context.Database.SqlQueryRaw<TimeAttendanceDTO>(Sql).ToList();
            return Ok(Objs);
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }


    [HttpGet("CheckStatus")]
    public async Task<IActionResult> CheckStatus()
    {
        try
        {
            long? userId = _tenantService.GetUserId();
            TimeAttendance? firstCheckInOut; TimeAttendance? lastCheckInOut;
            List<TimeAttendance> lst = context.TimeAttendances.Where(a => a.AttendanceDate.Value.Date == DateTime.Now.Date && a.AssignedToId == userId).ToList();
            if (lst.Count == 0)
                return Ok(new { checkIn = "---", checkOut = "---", totalHours = "---", status = "Check In"});
            else
            {
                firstCheckInOut = lst.MinBy(a => a.Id);
                lastCheckInOut = lst.MaxBy(a => a.Id);
                if (lst.Count % 2 == 1)
                    return Ok(new { checkIn = firstCheckInOut.CheckInOut, checkOut = "---", totalHours = "---", status = "Check Out" });
                else
                {
                    double? difference = (lastCheckInOut.CheckInOutMinutes - firstCheckInOut.CheckInOutMinutes) / 60;
                    return Ok(new { checkIn = firstCheckInOut.CheckInOut, checkOut = lastCheckInOut.CheckInOut, totalHours = difference.ToString(), status = "Check In" });
                }
            }
        }
        catch (Exception ex)
        {
            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
        }
    }

}

