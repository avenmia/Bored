import React, { useState } from "react";
import Cell from "./Cell";
import "./styles/Board.css";

const Board = () => 
{

  const [player, setPlayer] = useState('X')


  function updateBoard()
  {
    player === 'X' ? setPlayer('O') : setPlayer('X')
    return player;
  }


  return (
    <div className="grid-container">
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
      <Cell className="grid-item" value={player} updateBoard={() => updateBoard()} />
    </div>
  )
}

export default Board;