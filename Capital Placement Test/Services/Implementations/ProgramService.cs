using Capital_Placement_Test.DTOs.RequestDto;
using Capital_Placement_Test.Exceptions;
using Capital_Placement_Test.Models;
using Capital_Placement_Test.Response;
using Capital_Placement_Test.Services.Interfaces;
using Microsoft.Azure.Cosmos;

namespace Capital_Placement_Test.Services.Implementations
{
    public class ProgramService : IProgramService
    {
        private readonly IDatabaseConnection databaseConnection;
       /// <summary>
       /// Database container name alia Table
       /// </summary>
        protected internal string? Table { get; } = nameof(CandidateProgram);
        
        /// <summary>
        /// Unique prorty for getting container items.
        /// This is should not be changes by any means
        /// </summary>
        protected internal string? PartitionKey { get; } = "/id";
        public ProgramService(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;            
        }

        public async Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> CreateCandidateProgramAsync(CreateProgramDto dto)
        {
            var error = new ErrorResponse();
            var response = new ResponseHandler<CandidateProgram>();
            try
            {
                var container = await GetContainer();
                var program = new CandidateProgram
                {
                    Title = dto.Title,
                    Description = dto.Description
                };
                var data = await container.Item1.CreateItemAsync(program, new PartitionKey(PartitionKey));
                response.Status = data != null;
                response.Message = data != null ? "Program created successfully" : "Unable to create program";
                response.Data = data!.Resource;
            }
            catch (Exception ex)
            {
                error.Errors.Add(ex.Message);
                throw;
            }

            return new Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>(response, error);
        }       

        public async Task<ResponseHandler<List<CandidateProgram>>> GetAllCandidateProgramsAsync()
        {
            try
            {
                var container = await GetContainer();
                var sqlQuery = $"SELECT * FROM {container.Item2}";
                QueryDefinition queryDefinition = new(sqlQuery);
                FeedIterator<CandidateProgram> queryResultSetIterator = container.Item1.GetItemQueryIterator<CandidateProgram>(queryDefinition);
                var programs = new List<CandidateProgram>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<CandidateProgram> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (CandidateProgram employee in currentResultSet)
                    {
                        programs.Add(employee);
                    }
                }

                return new ResponseHandler<List<CandidateProgram>>
                {
                    Status = programs.Any(),
                    Message = programs.Any() ? "Programs fetched successfully" : "No record",
                    Data = programs
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> GetSingleCandidateProgramAsync(string id)
        {
            var error = new ErrorResponse();
            var response = new ResponseHandler<CandidateProgram>();
            try
            {
                var container = await GetContainer();
                ItemResponse<CandidateProgram> program = await container.Item1.ReadItemAsync<CandidateProgram>(id, new PartitionKey(PartitionKey));
                response.Status = program.Resource != null;
                response.Message = program.Resource != null? "Program fetched" : "No record found";
                response.Data = program.Resource;
            }
            catch (Exception ex)
            {
                error.Errors.Add(ex.Message);
                throw;
            }

            return new Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>(response, error);
        }
        public async Task<Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>> UpdateCandidateProgramAsync(string id, CreateProgramDto dto)
        {
            var error = new ErrorResponse();
            var response = new ResponseHandler<CandidateProgram>();
            try
            {
                var container = await GetContainer();
                ItemResponse<CandidateProgram> res = await container.Item1.ReadItemAsync<CandidateProgram>(id, new PartitionKey(PartitionKey));
                
                //Get Existing Item
                var existingItem = res.Resource;
                if (existingItem is null)
                    throw new CustomException("Invalid program");

                //Replace existing item values with new values
                existingItem.Title = dto.Title;
                existingItem.Description = dto.Description;
                existingItem.ModifiedAt = DateTime.Now;
                var updateRes = await container.Item1.ReplaceItemAsync(existingItem, id, new PartitionKey(PartitionKey));
                response.Status = updateRes.Resource != null;
                response.Message = updateRes.Resource != null ? "Program updated successfully" : "Unable to update record";
                response.Data = updateRes.Resource;
            }
            catch (CustomException ex)
            {

                error.Errors.Add(ex.Message);
            }
            catch (Exception ex)
            {
                error.Errors.Add(ex.Message);
                throw;
            }

            return new Tuple<ResponseHandler<CandidateProgram>, ErrorResponse>(response, error);
        }
        private async Task<Tuple<Container, string>> GetContainer()
            => await databaseConnection.DatabaseConnectAsync(Table!, PartitionKey!);
    }
}
