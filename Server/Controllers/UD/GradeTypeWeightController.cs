using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OCTOBER.EF.Data;
using OCTOBER.EF.Models;
using OCTOBER.Server.Controllers.Base;
using OCTOBER.Shared.DTO;
using System.Diagnostics;

namespace OCTOBER.Server.Controllers.UD
{
    [Route("api/[controller]")]
    [ApiController]

    public class GradeTypeWeightController : BaseController, GenericRestController<GradeTypeWeightDTO>
    {
        public GradeTypeWeightController(OCTOBEROracleContext context,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache memoryCache)
        : base(context, httpContextAccessor)
        {
        }


        [HttpGet]
        [Route("Get/{SchoolID}/{SectionID}/{GradeTypeCode}")]
        public async Task<IActionResult> Get(int SchoolID, int SectionID, string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                GradeTypeWeightDTO? result = await _context
                    .GradeTypeWeights
                    .Where(x => x.SectionId == SectionID)
                    .Where(x => x.SchoolId == SchoolID)
                    .Where(x => x.GradeTypeCode == GradeTypeCode)
                     .Select(sp => new GradeTypeWeightDTO
                     {
                         GradeTypeCode = sp.GradeTypeCode,
                         CreatedBy = sp.CreatedBy,
                         CreatedDate = sp.CreatedDate,
                         DropLowest = sp.DropLowest,
                         ModifiedBy = sp.ModifiedBy,
                         ModifiedDate = sp.ModifiedDate,
                         NumberPerSection = sp.NumberPerSection,
                         PercentOfFinalGrade = sp.PercentOfFinalGrade,
                         SchoolId = sp.SchoolId,
                         SectionId = sp.SectionId,
                     })
                .SingleOrDefaultAsync();

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
        [Route("Get")]
        public async Task<IActionResult> Get()
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var result = await _context.GradeTypeWeights.Select(sp => new GradeTypeWeightDTO
                {
                    GradeTypeCode = sp.GradeTypeCode,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    DropLowest = sp.DropLowest,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    NumberPerSection = sp.NumberPerSection,
                    PercentOfFinalGrade = sp.PercentOfFinalGrade,
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
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


        [HttpPost]
        [Route("Post")]
        public async Task<IActionResult> Post([FromBody]
                                                GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode).FirstOrDefaultAsync();

                if (itm == null)
                {
                    GradeTypeWeight gtw = new GradeTypeWeight
                    {
                        GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode,
                        SectionId = _GradeTypeWeightDTO.SectionId,
                        NumberPerSection = _GradeTypeWeightDTO.NumberPerSection,
                        PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade,
                        DropLowest = _GradeTypeWeightDTO.DropLowest,
                    };
                    _context.GradeTypeWeights.Add(gtw);
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
        public async Task<IActionResult> Put([FromBody]
                                                GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode).FirstOrDefaultAsync();

                itm.NumberPerSection = _GradeTypeWeightDTO.NumberPerSection;
                itm.SectionId = _GradeTypeWeightDTO.SectionId;
                itm.PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade;
                itm.DropLowest = _GradeTypeWeightDTO.DropLowest;

                _context.GradeTypeWeights.Update(itm);
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


        [HttpDelete]
        [Route("Delete/{GradeTypeCode}")]
        public async Task<IActionResult> Delete(string GradeTypeCode)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();

                var itm = await _context.GradeTypeWeights.Where(x => x.GradeTypeCode == GradeTypeCode).FirstOrDefaultAsync();

                if (itm != null)
                {
                    _context.GradeTypeWeights.Remove(itm);
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

        public Task<IActionResult> Delete(int KeyVal)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> Get(int KeyVal)
        {
            throw new NotImplementedException();
        }
    }
}
