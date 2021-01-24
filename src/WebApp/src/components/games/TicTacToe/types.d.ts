export interface TicTacToeMove
{
  Player: string,
  Cell: {
    row: number,
    col: number
  }
}

export interface GameMessage
{
  Game: string,
  GameID: string,
  Move: string,
}

interface TicTacToeState
{
  Winner: string,
  Cells: [][],
  GameType: string,
  Turn: string,
  Status: string
}