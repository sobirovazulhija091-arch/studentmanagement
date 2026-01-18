 using System.Numerics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
 public class EnrollmentController(IEnrollmentService enrollmentService):ControllerBase
 {
    [HttpPost]
    public async Task<Response<string>> AddAsync(EnrollmentDto enrollmentDto)
    {
         return await enrollmentService.AddAsync(enrollmentDto);
    }
    [HttpGet]
    public async Task<List<Enrollment>> GetAsync()
    {
           return await enrollmentService.GetAsync();
    }//stdent whit subject
    [HttpPatch]
   public async Task<Response<string>> UpdateActiveAsync(int enrollmentid,bool active)
    {
         return await enrollmentService.UpdateActiveAsync(enrollmentid,active);
    }
 }
 