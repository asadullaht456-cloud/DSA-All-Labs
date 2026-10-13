using Microsoft.AspNetCore.Mvc;
using DSALabsApi.Models;
using DSALabsApi.Services;

namespace DSALabsWeek03Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CircularLinkedListController : ControllerBase
    {
        private readonly CircularLinkedList _list;
        public CircularLinkedListController(CircularLinkedList list) => _list = list;

        [HttpGet]
        public IActionResult Traverse()
        {
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Circular list has {_list.Size} node(s)."));
        }

        [HttpPost("insert-beginning")]
        public IActionResult InsertBeginning([FromBody] ValueRequest req)
        {
            _list.InsertAtBeginning(req.Value);
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Inserted {req.Value} at beginning."));
        }

        [HttpPost("insert-end")]
        public IActionResult InsertEnd([FromBody] ValueRequest req)
        {
            _list.InsertAtEnd(req.Value);
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(),
                $"Inserted {req.Value} at end."));
        }

        [HttpDelete("delete-beginning")]
        public IActionResult DeleteBeginning()
        {
            var (ok, msg) = _list.DeleteFromBeginning();
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        [HttpDelete("delete-end")]
        public IActionResult DeleteEnd()
        {
            var (ok, msg) = _list.DeleteFromEnd();
            if (!ok) return BadRequest(ApiResponse<int[]>.Fail(msg));
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), msg));
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchRequest req)
        {
            int idx = _list.Search(req.Key);
            if (idx == -1)
                return NotFound(ApiResponse<int>.Fail($"{req.Key} not found."));
            return Ok(ApiResponse<int>.Ok(idx, $"{req.Key} found at index {idx}."));
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _list.Clear();
            return Ok(ApiResponse<int[]>.Ok(_list.Traverse(), "List cleared."));
        }
    }
}
