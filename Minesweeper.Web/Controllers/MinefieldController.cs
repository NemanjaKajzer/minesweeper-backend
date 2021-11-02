using Microsoft.AspNetCore.Mvc;
using Minesweeper.Model;
using MineSweeper.Business;
using System;
using System.Collections.Generic;

namespace Minesweeper.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MinefieldController : ControllerBase
    {
        private readonly IMinefieldService _minefieldService;
        private readonly IDifficultyService _difficultyService;

        public MinefieldController(IMinefieldService minefieldService, IDifficultyService difficultyService)
        {
            _minefieldService = minefieldService;
            _difficultyService = difficultyService;
        }

        [HttpPost]
        [Route("ChooseDifficulty")]
        public IActionResult ChooseDifficulty(int difficultyId)
        {
            var difficulty = _difficultyService.GetDifficultyById(difficultyId);
            _minefieldService.CreateMinefieldByDifficulty(difficulty);

            return Ok(difficulty);
        }

        [HttpPost]
        [Route("OpenCell")]
        public IActionResult OpenCell(Cell cell)
        {
            var openedCells = _minefieldService.OpenCell(cell);

            if (_minefieldService.GetMineSweeperField().IsFinished)
            {
                var bombCells = _minefieldService.GetMineSweeperField().GetBombCells();
                var openedAndBombCells = new Tuple<List<Cell>, List<Cell>>(openedCells, bombCells);
                // we'll return all the opened and all the bomb cells, so we can show them to user
                //return Ok("FINISHED");
                return Ok(openedAndBombCells);
            }
            return Ok(_minefieldService.GetMineSweeperField().Display());
        }

        [HttpPost]
        [Route("ToggleMarkAsBomb")]
        public IActionResult ToggleMarkAsBomb(Cell cell)
        {
            _minefieldService.ToggleMarkAsBomb(cell);
            return Ok(cell);
        }
    }
}
