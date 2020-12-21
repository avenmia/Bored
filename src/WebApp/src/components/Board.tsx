import React, { useState } from "react";
import Cell from "./Cell";
import "./styles/Board.css";

const Board = () => 
{

  const [player, setPlayer] = useState('X')


  function updateBoard()
  {
    let currentPlayer = player;
    console.log("Current player is : %s", currentPlayer);
    console.log("Nextplayer is : %s", player);
    player === 'X' ? setPlayer('O') : setPlayer('X')
    console.log("Nextplayer is : %s", player);
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