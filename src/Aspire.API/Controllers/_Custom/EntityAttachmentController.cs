//using Aspire.Utilities;
//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Aspire.Context;
//using System.Net.Http.Headers;
//using Microsoft.EntityFrameworkCore;
//using Aspire.Data;

//namespace Aspire.APIs;

//[Route("api/[controller]")]
//[ApiController]
//public class EntityAttachmentController(ApplicationDbContext context,
//        ILogHandlingService _logger,
//        IGenericRepository<EntityAttachment, EntityAttachmentDTO, ApplicationDbContext> _genericRepository)
//                : ControllerBase
//{
//    [HttpPost("GetAll/{entityName}/{entityId}")]
//    public async Task<IActionResult> GetAll([FromBody] FilteringModel? filteringModel, string entityName, string entityId)
//    {
//        try
//        {
//            filteringModel.FilterFields.Add("EntityName", new FieldInfo("EntityName", "text", entityName));
//            filteringModel.FilterFields.Add("ObjectId", new FieldInfo("ObjectId", "text", entityId));
//            PaginationModel<EntityAttachmentDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
//            return Ok(Objs);
//        }
//        catch (Exception ex)
//        {
//            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
//        }
//    }

//    [HttpGet("GetById")]
//    public async Task<IActionResult> GetById([FromQuery] string Id)
//    {
//        try
//        {
//            EntityAttachmentDTO? Obj = await _genericRepository.GetByIdEntityDTO(Id);
//            return Ok(Obj);
//        }
//        catch (Exception ex)
//        {
//            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
//        }
//    }


//    [HttpGet("GetPhoto")]
//    public async Task<IActionResult> GetPhoto([FromQuery] string ObjectId)
//    {
//        try
//        {
//            FilteringModel? filteringModel = new FilteringModel();
//            filteringModel.FilterFields.Add("ObjectId", new FieldInfo("ObjectId", "text", ObjectId));
//            filteringModel.FilterFields.Add("FileName", new FieldInfo("FileName", "text", "Photo"));
//            PaginationModel<EntityAttachmentDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
//            if (Objs.Items != null)
//                return Ok(Objs.Items);

//            return Ok(null);
//        }
//        catch (Exception ex)
//        {
//            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
//        }
//    }

//    [HttpPost("Save")]
//    public async Task<IActionResult> Save([FromForm] SaveEntityAttachmentDTO Model)
//    {
//        try
//        {
//            string EnvPath = @"C:\Aspire\website\public\Aspire-Attachments\";
//            EntityAttachmentDTO dTO = new EntityAttachmentDTO();

//            // Save the file using minio
//            var file = Model.File;
//            if (file != null && file.Length > 0)
//            {
//                FilteringModel? filteringModel = new FilteringModel();
//                filteringModel.FilterFields.Add("ObjectId", new FieldInfo("ObjectId", "text", Model.ObjectId.ToString()));
//                filteringModel.FilterFields.Add("FileName", new FieldInfo("FileName", "text", Model.FileName));
//                PaginationModel<EntityAttachmentDTO> Objs = await _genericRepository.GetAllJoins(filteringModel);
//                if (Objs.Items.Count() > 0)
//                    return Ok(new { severity = "info", summary = "File", message = "This file name already exist for the same record!" });

//                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
//                fileName = Guid.NewGuid().ToString() + "." + fileName.Split('.')[1];
//                var filePath = EnvPath + fileName;

//                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
//                    await file.CopyToAsync(fileStream);

//                dTO.Name = Model.FileName;
//                dTO.FileName = Model.FileName;
//                dTO.FileExtension = Model.FileExtension;
//                dTO.ObjectId = Model.ObjectId;
//                dTO.EntityName = Model.EntityName;
//                dTO.DownloadLink = "/Documents/" + fileName;
//                dTO.FileSize = file.Length;
//                dTO.ContentType = file.ContentType;
//            }

//            string result = await _genericRepository.Save(dTO, this.HttpContext);

//            if (Model.EntityName == "AuditItem")
//            {
//                var AudItm = await context.AuditItems.FirstOrDefaultAsync(x => x.Id == Model.ObjectId);
//                int Evids = context.EntityAttachments.Where(x => x.ObjectId == Model.ObjectId).Count();

//                if (AudItm is not null)
//                {
//                    AudItm.Evids = Evids;
//                    context.SaveChanges();
//                }
//            }

//            return Ok(new { severity = result.Split(":")[0], summary = result.Split(":")[1], message = result.Split(":")[2] });
//        }
//        catch (Exception ex)
//        {
//            _logger.LogException(ex, nameof(Save));
//            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
//        }
//    }

//    [HttpPost("SavePhoto")]
//    public async Task<IActionResult> SavePhoto([FromForm] SaveEntityAttachmentDTO Model)
//    {
//        try
//        {
//            string EnvPath = @"C:\Aspire\website\public\Aspire-Attachments\";
//            EntityAttachmentDTO dTO = new EntityAttachmentDTO();

//            // Save the file using minio
//            var file = Model.File;
//            string fileName = "";
//            if (file != null && file.Length > 0)
//            {
//                fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
//                fileName = Guid.NewGuid().ToString() + "." + fileName.Split('.')[1];
//                var filePath = EnvPath + fileName;

//                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
//                    await file.CopyToAsync(fileStream);
//            }

//            return Ok(new { filename = fileName, severity = "info", summary = "File Upload", message = "Photo Uploaded Successfully." });
//        }
//        catch (Exception ex)
//        {
//            _logger.LogException(ex, nameof(Save));
//            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
//        }
//    }

//    [HttpPost("Edit")]
//    public async Task<IActionResult> Edit([FromBody] EntityAttachmentDTO Model)
//    {
//        try
//        {
//            string result = await _genericRepository.Save(Model, this.HttpContext);
//            return Ok(new { severity = result.Split(":")[0], summary = result.Split(":")[1], message = result.Split(":")[2] });
//        }
//        catch (Exception ex)
//        {
//            _logger.LogException(ex, nameof(Edit));
//            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
//        }
//    }


//    [HttpDelete("Delete")]
//    public async Task<IActionResult> Delete([FromQuery] string Id)
//    {
//        try
//        {
//            string result = await _genericRepository.Delete(Id, this.HttpContext);
//            return Ok(new { severity = result.Split(":")[0], summary = result.Split(":")[1], message = result.Split(":")[2] });
//        }
//        catch (Exception ex)
//        {
//            return Ok(new { severity = "error", summary = "Server Exception", message = ex.Message });
//        }
//    }
//}
