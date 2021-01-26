import React, { MouseEvent, useState } from "react";
import "./styles/Cell.css"

const Cell = ({value, updateBoard, position, player} : any) =>
{
  const [cell, setCell] = useState('');

  function setCellValue(v: MouseEvent<HTMLButtonElement>)
  {
    if (v.currentTarget.value === '')
    {
      setCell(player);
      const move = {
        Player: player,
        Cell: {
          row: position[0],
          col: position[1]
        }
      }
      updateBoard(move);
    }
  }

  return (
    <button className="square" onClick={setCellValue}>
      {cell}
    </button>
  )
}

export default Cell;