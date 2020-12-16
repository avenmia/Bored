import React, { useState, useEffect, useRef } from 'react';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import ChatWindow from './ChatWindow';
import ChatInput from './ChatInput';

interface GameMessage {
  user: string,
  message: string
}

const Chat = () => {
    const [ connection, setConnection ] = useState<HubConnection | null>(null);
    const [ chat, setChat ] = useState<GameMessage[]>([]);
    const latestChat = useRef<any | null>(null);

    latestChat.current = chat;

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
                        const updatedChat = [...latestChat.current];
                        updatedChat.push(message);
                    
                        setChat(updatedChat);
                    });
                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const sendMessage = async (user: string, message: string) => {
        const chatMessage: GameMessage = {
            user: user,
            message: message
        };

        if (connection?.state === "Connected") {
            try {
                await connection.send('SendMessage', chatMessage);
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
            <ChatInput sendMessage={sendMessage} />
            <hr />
            <ChatWindow chat={chat}/>
        </div>
    );
};

export default Chat;