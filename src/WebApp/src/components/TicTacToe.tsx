import React, { useEffect, useState } from "react";
import Board from "./Board";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

interface TicTacToeState
{
  GameID: string,
  GameType: string,
  Turn: string,
  Winner: string,
  TotalWins: Number
}

interface TicTacToeMove
{
  Player: string,
  Cell: {
    row: number,
    col: number
  }
}

interface IGameState<T>
{
}


interface GameMessage
{
  Game: string,
  GameID: string,
  Move: string,
}


const TicTacToe = () => 
{
  const [ connection, setConnection ] = useState<HubConnection | null>(null);

  useEffect(() => {
    const newConnection = new HubConnectionBuilder()
        .withUrl('https://localhost:44350/gameservicehub')
        .withAutomaticReconnect()
        .build();

    setConnection(newConnection);
  }, []);

  useEffect(() => {
    if (connection) {
        connection.start()
            .then(result => {
                console.log('Connected!');

                connection.on('ReceiveMessage', (message: any) => {
                    console.log("Receiving state");
                    // setBoard(message);
                });
            })
            .catch(e => console.log('Connection failed: ', e));
    }
  }, [connection]);

  const sendState = async () => {

    const move: TicTacToeMove = {
      Player: "O",
      Cell: {
        row: 0,
        col: 1
      }
    }

    const gameMessage: GameMessage = {
      Game: "TicTacToe",
      GameID: "1",
      Move: JSON.stringify(move),
    }

    if (connection?.state === "Connected") {
        try {
            await connection.send('SendMessage', gameMessage);
        }
        catch(e) {
            console.log(e);
        }
    }
    else {
        alert('No connection to server yet.');
    }
  }

  return (
    <div>
      <Board sendState={async () => await sendState()} setBoard={() => console.log("Setting board")}/>
    </div>
  )
}

export default TicTacToe;