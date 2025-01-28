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
