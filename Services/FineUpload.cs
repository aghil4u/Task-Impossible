using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FineUploader
{
    [ModelBinder(typeof(ModelBinder))]
    public class FineUpload
    {
        public string Filename { get; set; }
        public Stream InputStream { get; set; }

        public void SaveAs(string destination, bool overwrite = false, bool autoCreateDirectory = true)
        {
            if (autoCreateDirectory)
            {
                var directory = new FileInfo(destination).Directory;
                if (directory != null) directory.Create();
            }

            using (var file = new FileStream(destination, overwrite ? FileMode.Create : FileMode.CreateNew))
            {
                InputStream.CopyTo(file);
            }
        }

        public class ModelBinder : IModelBinder
        {
            
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                var request = bindingContext.HttpContext.Request;

                var formUpload = request.Form.Files.Count > 0;

                // find filename
                string xFileName = request.Headers["X-File-Name"];
                string qqFile = request.Form["qqfile"];
                var formFilename = formUpload ? request.Form.Files[0].FileName : null;

                var upload = new FineUpload
                {
                    Filename = xFileName ?? qqFile ?? formFilename,

                    InputStream = request.Form.Files[0].OpenReadStream()
                };
                bindingContext.Result = ModelBindingResult.Success(upload);
                
                return Task.CompletedTask;
            }
        }
    }
}