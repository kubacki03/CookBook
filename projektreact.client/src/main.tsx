import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'

import LoginPage from './LoginPage.tsx'
import React from 'react'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
        <LoginPage/>
  </StrictMode>,
)
