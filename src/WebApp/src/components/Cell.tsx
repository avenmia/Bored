import React, { SyntheticEvent, useState } from "react";
import "./styles/Cell.css"

const Cell = ({value, updateBoard} : any) =>
{
  const [player, setPlayer] = useState(' ');

  function setCellValue(v: SyntheticEvent)
  {
    updateBoard();
    setPlayer(value);
  }

  return (
    <button className="square" onClick={setCellValue}>
      {player}
    </button>
  )
}

export default Cell;