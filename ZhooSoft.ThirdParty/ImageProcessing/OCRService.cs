using Plugin.Maui.OCR;
using System.Text.RegularExpressions;

namespace ZhooSoft.ThirdParty.ImageProcessing
{
    public class OcrService : IOcrService
    {

        private bool isInitiated;

        public async Task<string?> ExtractTextAsync(FileResult pickResult)
        {
            try
            {
                if (!isInitiated)
                {
                    isInitiated = true;
                    await OcrPlugin.Default.InitAsync();
                }

                if (pickResult != null)
                {
                    using var imageAsStream = await pickResult.OpenReadAsync();
                    if (imageAsStream == null) return null;
                    var imageAsBytes = new byte[imageAsStream.Length];
                    await imageAsStream.ReadExactlyAsync(imageAsBytes);
                    var ocrResult = await OcrPlugin.Default.RecognizeTextAsync(imageAsBytes);

                    if (!ocrResult.Success)
                    {
                        return null;
                    }

                    // Aadhaar number pattern (XXXX XXXX XXXX)
                    var aadhaarPattern = @"\b\d{4}\s\d{4}\s\d{4}\b";
                    var match = Regex.Match(ocrResult.AllText, aadhaarPattern);

                    return match.Success ? match.Value : "No Aadhaar number found.";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return null;
        }
    }
}
