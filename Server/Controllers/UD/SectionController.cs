using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectionController : BaseController, GenericRestController<SectionDTO>
    {
        public SectionController(OCTOBEROracleContext context,
                                IHttpContextAccessor httpContextAccessor,
                                IMemoryCache memoryCache)
                : base(context, httpContextAccessor)
        {
        }

        [HttpDelete]
        [Route("Delete/{SectionID}")]
        public async Task<IActionResult> Delete(int SectionID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == SectionID).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.Sections.Remove(itm);
                }
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.Sections.Select(sp => new SectionDTO
                {
                    Capacity = sp.Capacity,
                    CourseNo = sp.CourseNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    InstructorId = sp.InstructorId,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime
                })
                .ToListAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpGet]
        [Route("Get/{SectionID}")]
        public async Task<IActionResult> Get(int SectionID)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                SectionDTO? result = await _context.Sections
                    .Where(x=>x.SectionId == SectionID)
                    .Select(sp => new SectionDTO
                {
                    Capacity = sp.Capacity,
                    CourseNo = sp.CourseNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    InstructorId = sp.InstructorId,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    SectionNo = sp.SectionNo,
                    StartDateTime = sp.StartDateTime
                })
                .SingleAsync();
                await _context.Database.RollbackTransactionAsync();
                return Ok(result);
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId).FirstOrDefaultAsync();

                if (itm == null)
                {
                    Section s = new Section
                    {

                        SectionId = _SectionDTO.SectionId,
                        SectionNo = _SectionDTO.SectionNo
                    };
                    _context.Sections.Add(s);
                    await _context.SaveChangesAsync();
                    await _context.Database.CommitTransactionAsync();
                }
                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }

        [HttpPut]
        [Route("Put")]
        public async Task<IActionResult> Put([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId).FirstOrDefaultAsync();

                itm.SectionNo = _SectionDTO.SectionNo;

                _context.Sections.Update(itm);
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();

                return Ok();
            }
            catch (Exception Dex)
            {
                await _context.Database.RollbackTransactionAsync();
                //List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, "An Error has occurred");
            }
        }
    }
}

