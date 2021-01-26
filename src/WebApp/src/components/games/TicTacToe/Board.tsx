import React, { useState } from "react";
import Cell from "./Cell";
import "./styles/Board.css";
import type { TicTacToeMove, TicTacToeState } from "./types";

const ROW_SIZE = 3;
const COL_SIZE = 3;

// When the board gets a new game state it needs to update using that
const Board = ({gameId, sendState, gameState} : any) => 
{

  const [player, setPlayer] = useState('X')
  
  async function updateBoard(move: TicTacToeMove)
  {
    player === 'X' ? setPlayer('O') : setPlayer('X')
    await sendState(gameId, move);
    return player;
  }

  // TODO: make TicTacToe Value enum
  function getCellValue(row: number, col: number)
  {
    try
    {
      if (gameState !== undefined)
      {
        const currentGameState = JSON.parse(gameState) as TicTacToeState
        const cellValue = currentGameState.Cells[row][col]; 
        return cellValue === null ? '' : cellValue === 0 ? 'X' : 'O'; 
      }
      return '';
    }
    catch(e)
    {
      console.log("Error: %o", e);
      return '';
    }
  }

  let cells = [];
  for(let row = 0; row < ROW_SIZE; row++)
  {
    for(let col = 0; col < COL_SIZE; col++)
    {
      // TODO: Make key for cell less of a hack.
      cells.push(<Cell key={row.toString() + col.toString()} className="grid-item" value={getCellValue(row,col)} updateBoard={updateBoard} position={[row,col]} player={player}/>)
    }
  }

  return (
    <div className="grid-container">
      {
        cells
      }
    </div>
  )
}

export default Board;