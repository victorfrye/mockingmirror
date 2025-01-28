// import { useState } from 'react'
// import reactLogo from './assets/react.svg'
// import viteLogo from '/vite.svg'

import DarkModeProvider from './hooks/darkMode';
import ThemeProvider from './hooks/theme';
import Mirror from './components/Mirror';

const App = () => {
  return (
    <DarkModeProvider>
      <ThemeProvider>
        <Mirror />
      </ThemeProvider>
    </DarkModeProvider>
  )
}

export default App
