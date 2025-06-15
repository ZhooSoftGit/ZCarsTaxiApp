using ZTaxiApp.Model.DTOs;
using ZTaxiApp.Model.Request;
using ZTaxiApp.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZTaxiApp.Services.Contracts
{
    #region Interfaces

    public interface IDocumentService
    {
        #region Methods

        Task<ApiResponse<bool>> ApproveDocumentAsync(int documentId, DocumentApprovalRequest request);

        Task<ApiResponse<SignedUrlResponse>> GenerateSignedUrlAsync(SignedUrlRequest signedUrlRequest);

        Task<ApiResponse<List<DocumentDto>>> GetUserDocumentsAsync(string? phoneNumber = null);

        Task<ApiResponse<bool>> UploadFileAsync(Stream fileStream, string fileName);

        Task<ApiResponse<DocumentDto>> UpsertDocumentAsync(UpsertDocumentDto dto, string? phoneNumber = null);

        #endregion
    }

    #endregion
}
