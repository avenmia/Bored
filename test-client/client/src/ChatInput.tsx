import React, { useState } from "react";

interface ChatProps {
  sendMessage: (user: string, message: string) => Promise<void>
}

const ChatInput = (props: ChatProps) => {
  const [ user, setUser ] = useState('');
  const [ message, setMessage ] = useState('');

  const onSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    const isUserProvided = user && user !== '';
    const isMessageProvided = message && message !== '';
    if (isUserProvided && isMessageProvided) {
      props.sendMessage(user, message);
    } 
  else {
      alert('Please insert an user and a message.');
    }
  }

  const onUserUpdate = (e: React.ChangeEvent<HTMLInputElement>) => {
    setUser(e.target.value);
  }

  const onMessageUpdate = (e: React.ChangeEvent<HTMLInputElement>) => {
    setMessage(e.target.value);
  }

  return (
    <form 
        onSubmit={onSubmit}>
        <label htmlFor="user">User:</label>
        <br />
        <input 
            id="user" 
            name="user" 
            value={user}
            onChange={onUserUpdate} />
        <br/>
        <label htmlFor="message">Message:</label>
        <br />
        <input 
            type="text"
            id="message"
            name="message" 
            value={message}
            onChange={onMessageUpdate} />
        <br/><br/>
        <button>Submit</button>
    </form>
  )
};

export default ChatInput;