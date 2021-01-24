import React, { useEffect, useState } from "react";
import Board from "./Board";
import { Guid } from "guid-typescript";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import type { TicTacToeState, TicTacToeMove, GameMessage } from "./types";


const TicTacToe = () => 
{
  const [ connection, setConnection ] = useState<HubConnection | null>(null);
  const [gameId, setGameId] = useState('');
  const [gameState, setGameState] = useState<TicTacToeState>();

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
            .then(async (result) => {
                console.log('Connected!');
                // TODO: Check for existing game in case of disconnect?
                await connection.send('GetNewGameID')
                connection.on('ReceiveMessage', (message: TicTacToeState | string) => {
                    Guid.isGuid(message) ? setGameId(message as string) : setGameState(message as TicTacToeState)
                });
            })
            .catch(e => console.log('Connection failed: ', e));
    }
  }, [connection]);

  const sendState = async (gameId: string, move: TicTacToeMove) => {

    const gameMessage: GameMessage = {
      Game: "TicTacToe",
      GameID: gameId,
      Move: JSON.stringify(move),
    }

    if (connection?.state === "Connected") {
        try {
            console.log("Game Message is: %o", gameMessage);
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
            <Board gameId={gameId} sendState={sendState} gameState={gameState}/>
          </div>
        )
}

export default TicTacToe;