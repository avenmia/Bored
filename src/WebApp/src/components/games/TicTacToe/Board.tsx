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

const Board = ({sendState, setBoard} : any) => 
{
  const ROW_SIZE = 3;
  const COL_SIZE = 3;

  const [player, setPlayer] = useState('X')

  async function updateBoard(move: TicTacToeMove)
  {
    player === 'X' ? setPlayer('O') : setPlayer('X')
    console.log("Incoming move is: %o", move);
    // TODO: Remove hard coded game ID
    await sendState("1", move);
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