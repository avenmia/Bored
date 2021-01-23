import React, { SyntheticEvent, useState } from "react";
import "./styles/Cell.css"

const Cell = ({value, updateBoard, position} : any) =>
{
  const [player, setPlayer] = useState(' ');

  function setCellValue(v: SyntheticEvent)
  {
    if (player === ' ')
    {
      setPlayer(value);
      const move = {
        Player: value,
        Cell: {
          row: position[0],
          col: position[1]
        }
      }
      console.log("Move is: %o", move)
      updateBoard(move);
    }
  }

  return (
    <button className="square" onClick={setCellValue}>
      {player}
    </button>
  )
}

export default Cell;