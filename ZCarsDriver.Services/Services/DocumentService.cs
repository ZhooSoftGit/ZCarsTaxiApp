using ZCarsDriver.Services.Contracts;
using ZhooCars.Model.DTOs;
using ZhooCars.Model.Request;
using ZhooCars.Model.Response;
using ZhooSoft.ServiceBase;

namespace ZCarsDriver.Services.Services
{
    public class DocumentService : IDocumentService
    {
        #region Fields

        private readonly IApiService _apiService;

        #endregion

        #region Constructors

        public DocumentService(IApiService apiService)
        {
            _apiService = apiService;
        }

        #endregion

        #region Methods

        public async Task<ApiResponse<bool>> ApproveDocumentAsync(int documentId, DocumentApprovalRequest request)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.DocumentApprove}/{documentId}", request);
        }

        public async Task<ApiResponse<SignedUrlResponse>> GenerateSignedUrlAsync(SignedUrlRequest signedUrlRequest)
        {
            return await _apiService.PostAsync<SignedUrlResponse>($"{ApiConstants.BaseUrl}{ApiConstants.DocumentGetSignedUrl}", signedUrlRequest);
        }

        public async Task<ApiResponse<List<DocumentDto>>> GetUserDocumentsAsync(string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? $"{ApiConstants.BaseUrl}{ApiConstants.DocumentGetDocuments}" : $"{ApiConstants.BaseUrl}{ApiConstants.DocumentGetDocuments}?phoneNumber={phoneNumber}";
            return await _apiService.GetAsync<List<DocumentDto>>(url);
        }

        public async Task<ApiResponse<bool>> UploadFileAsync(Stream fileStream, string fileName)
        {
            return await _apiService.PostAsync<bool>($"{ApiConstants.BaseUrl}{ApiConstants.DocumentUpload}", fileStream);
        }

        public async Task<ApiResponse<DocumentDto>> UpsertDocumentAsync(UpsertDocumentDto dto, string? phoneNumber = null)
        {
            var url = string.IsNullOrEmpty(phoneNumber) ? $"{ApiConstants.BaseUrl}{ApiConstants.DocumentUpsert}" : $"{ApiConstants.BaseUrl}{ApiConstants.DocumentUpsert}?phoneNumber={phoneNumber}";
            return await _apiService.PostAsync<DocumentDto>(url, dto);
        }

        #endregion
    }
}
