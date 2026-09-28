using Aspire.Data;
using Aspire.Services;
using Aspire.Utilities;
using Microsoft.AspNetCore.Mvc;
using Aspire.Context;
using Microsoft.EntityFrameworkCore;

namespace Aspire.APIs;

/// <summary>
/// Raising a document's revision number.
///
/// This cannot go through Document/Edit. DocumentService.Update refuses any document that is
/// not in Draft, and a revision is uploaded precisely while the document sits Under Review -
/// that lock is there to stop the contractor rewriting the record mid-review, and it should
/// stay. So the version moves through its own narrow endpoint that touches two columns and
/// nothing else, rather than by widening the lock.
///
/// Lives in _Custom, which the low-code generator never rewrites.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DocumentVersionController(ApplicationDbContext context, ITenantProviderService _tenantService)
    : ControllerBase
{
    public class BumpVersionRequest
    {
        public long Id { get; set; }
        /// <summary>Optional. Left out, the next whole revision is taken.</summary>
        public decimal? Version { get; set; }
    }

    /// <summary>
    /// Records that a new file has become the current revision of this document.
    /// The attachment itself is already stored by the time this is called; only the number moves.
    /// </summary>
    [HttpPost("Bump")]
    public async Task<IActionResult> Bump([FromBody] BumpVersionRequest model)
    {
        try
        {
            long? tenantId = _tenantService.GetTenantId();

            Document? doc = await context.Documents
                .FirstOrDefaultAsync(d => d.Id == model.Id && d.TenantId == tenantId);

            if (doc is null)
                return Ok(new { severity = "error", summary = "Not found", msg = $"This Id ({model.Id}) is not exist in the database" });

            // Versioning is a document-type setting, so the server decides it too - a client
            // that stops sending the flag must not become a way around it.
            DocumentType? type = doc.DocumentTypeId is null
                ? null
                : await context.DocumentTypes.FirstOrDefaultAsync(t => t.Id == doc.DocumentTypeId);

            if (type?.IsAllowVersioning != true)
                return Ok(new { severity = "error", summary = "Business Validation", msg = "Versioning is not enabled for this document type." });

            // A revision only counts once its file is in, so refuse to number an empty document.
            bool hasFile = await context.EntityAttachments
                .AnyAsync(a => a.TenantId == tenantId && a.EntityName == "Document" && a.ObjectId == doc.Id);

            if (!hasFile)
                return Ok(new { severity = "error", summary = "Business Validation", msg = "At least one attachment is required before a new version can be recorded." });

            decimal next = model.Version ?? Math.Floor(doc.CurrentVersion ?? 0m) + 1m;

            // Monotonic: a late or repeated call must never walk the revision backwards.
            if (doc.CurrentVersion.HasValue && next <= doc.CurrentVersion.Value)
                return Ok(new { severity = "error", summary = "Business Validation", msg = $"Version must be greater than the current version ({doc.CurrentVersion.Value})." });

            doc.CurrentVersion = next;
            doc.UpdatedByUsrId = _tenantService.GetUserId();
            doc.UpdateDate = DateTime.UtcNow;

            await context.SaveChangesAsync();
            await Utilities.DBLog.Action(doc.Id, "Document", "NewVersion", $"Document version raised to {next}", context, _tenantService);

            return Ok(new { severity = "success", msg = "OK", id = doc.Id, version = doc.CurrentVersion });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
