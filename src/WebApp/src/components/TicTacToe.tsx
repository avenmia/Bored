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

interface IGameState
{
  GameType: String
}

interface GameMessage
{
  GameState: string
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

    const chatMessage: IGameState = {
      GameType: "TestState"
    }

    const gameMessage: GameMessage = {
      GameState: JSON.stringify(chatMessage)
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