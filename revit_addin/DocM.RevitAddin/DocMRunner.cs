// In DocMRunner.cs
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Newtonsoft.Json; // Use the JSON library
using System;

namespace DocM.RevitAddin
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class DocMRunner : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Start logging
            Logger.Log("DocM Runner invoked.");

            try
            {
                // 1. Create some sample data to represent an object from Revit
                var roomData = new
                {
                    Name = "Office 101",
                    Area = 150.5,
                    Department = "Engineering"
                };

                // 2. Serialize the C# object into a JSON string
                string json = JsonConvert.SerializeObject(roomData, Formatting.Indented);
                Logger.Log("Serialized room data:");
                Logger.Log(json);

                // 3. Use the ApiClient to send the data
                // NOTE: In a real app with a UI, you would use ExternalEvent to run this
                // asynchronously. For this backend example, we'll wait for the result.
                Logger.Log("Sending data to API...");
                var apiClient = new ApiClient();
                var task = apiClient.PostDataAsync(json);
                string apiResponse = task.Result; // .Result waits for the async task to complete

                if (apiResponse != null)
                {
                    Logger.Log($"API Response: {apiResponse}");
                    TaskDialog.Show("API Success", "Successfully sent data and received a response. See log file for details.");
                }
                else
                {
                    TaskDialog.Show("API Error", "Failed to get a response from the API. See log file for details.");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur
                Logger.Log($"An error occurred: {ex.Message}");
                TaskDialog.Show("Error", $"An unexpected error occurred. See log file for details at C:\\Temp\\DocM\\logs");
                return Result.Failed;
            }

            return Result.Succeeded;
        }
    }
}