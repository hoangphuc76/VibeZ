import React from 'react'
import ReactDOM from 'react-dom/client'
import { BrowserRouter } from 'react-router-dom'
import App from './App.jsx'
import './index.css'
import PlayerContextProvider from './context/PlayerContext.jsx'
import LoginContextProvider from './context/LoginContext.jsx'
import { ListVisibilityProvider } from './context/VisibleContext.jsx'
ReactDOM.createRoot(document.getElementById('root')).render(
  <LoginContextProvider>
      <PlayerContextProvider>
        <ListVisibilityProvider>
          <App />
        </ListVisibilityProvider>
      </PlayerContextProvider>
  </LoginContextProvider>
)
