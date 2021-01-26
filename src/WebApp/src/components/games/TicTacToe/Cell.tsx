import React, { MouseEvent, useState } from "react";
import "./styles/Cell.css"

const Cell = ({value, updateBoard, position, player} : any) =>
{
  const [cell, setCell] = useState('');

  async function setCellValue(v: MouseEvent<HTMLButtonElement>)
  {
    if (v.currentTarget.value === '')
    {
      const move = {
        Player: player,
        Cell: {
          row: position[0],
          col: position[1]
        }
      }
      await updateBoard(move);
      // TODO: Cell should be set based on result of updating the game board since the move could be invalid.
      setCell(player);
    }
  }

  return (
    <button className="square" onClick={setCellValue}>
      {cell}
    </button>
  )
}

export default Cell;