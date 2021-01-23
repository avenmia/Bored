import React, { useState } from "react";
import Cell from "./Cell";
import "./styles/Board.css";

interface TicTacToeMove
{
  Player: string,
  Cell: {
    row: number,
    col: number
  }
}

const Board = ({gameId, sendState, gameState} : any) => 
{
  const ROW_SIZE = 3;
  const COL_SIZE = 3;

  console.log("Current game state: %o", gameState);
  
  const [player, setPlayer] = useState('X')
  async function updateBoard(move: TicTacToeMove)
  {
    player === 'X' ? setPlayer('O') : setPlayer('X')
    await sendState(gameId, move);
    return player;
  }

  let cells = [];
  for(let i = 0; i < ROW_SIZE; i++)
  {
    for(let j = 0; j < COL_SIZE; j++)
    {
      cells.push(<Cell className="grid-item" value={player} updateBoard={updateBoard} position={[i,j]}/>)
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