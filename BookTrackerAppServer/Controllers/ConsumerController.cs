using BookTrackerAppServer.Database;
using BookTrackerAppServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTrackerAppServer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumerController : Controller
    {
        private readonly BookDbContext _bookDbContext;
        public ConsumerController(BookDbContext bookDbContext)
        {
            _bookDbContext = bookDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllConsumers()
        {
            var consumers = await _bookDbContext.consumers.ToListAsync();
            return Ok(consumers);
        }
        [HttpPost]
        public async Task<IActionResult> AddConsumer([FromBody] Consumer consumerAddRequest)
        {
            await _bookDbContext.consumers.AddAsync(consumerAddRequest);
            await _bookDbContext.SaveChangesAsync();
            return Ok(consumerAddRequest);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetConsumer([FromRoute] int id)
        {
            var consumer = await _bookDbContext.consumers.FirstOrDefaultAsync(c => c.Id == id);
            if (consumer == null)
            {
                return NotFound();
            }
            return Ok(consumer);
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateConsumer([FromRoute] int id, Consumer updateConsumerRequest)
        {
            var consumer = await _bookDbContext.consumers.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }
            consumer.Sn = updateConsumerRequest.Sn;
            consumer.Name = updateConsumerRequest.Name;
            consumer.Address = updateConsumerRequest.Address;
            consumer.Mobile = updateConsumerRequest.Mobile;
            await _bookDbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteConsumer([FromRoute] int id)
        {
            var consumer = await _bookDbContext.consumers.FindAsync(id);
            if (consumer == null)
            {
                return NotFound();
            }
            _bookDbContext.consumers.Remove(consumer);
            await _bookDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
