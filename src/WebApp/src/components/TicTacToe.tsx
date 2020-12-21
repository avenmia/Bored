import React, { useEffect, useState } from "react";
import Board from "./Board";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";

interface TicTacToeState
{
  GameID: string,
  GameType: string,
  Turn: String,
  Winner: String,
  TotalWins: Number
}

interface IGameState<T>
{
}

// interface GameMessage
// {
//   GameState: IGameState<unknown>,
//   GameType: string
// }

interface GameMessage
{
  GameState: any,
  GameType: string
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
    // const chatMessage: TicTacToeState = {
    //     GameID: "game1",
    //     GameType: "TestState",
    //     Turn: "X",
    //     Winner: "None",
    //     TotalWins: 5
    // };

    const chatMessage: IGameState<any> = {
      GameID: "game1",
      Winner: "Charlie Kelly",
      Turn: "Over",
      TotalWins: 5
    }

    const gameMessage: GameMessage = {
      GameState: JSON.stringify(chatMessage),
      GameType: "TestState"
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