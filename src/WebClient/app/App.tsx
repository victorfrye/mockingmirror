import DarkModeProvider from './providers/DarkMode';
import ThemeProvider from './providers/Theme';
import Mirror from './components/Mirror';

const App = () => {
  return (
    <DarkModeProvider>
      <ThemeProvider>
        <Mirror />
      </ThemeProvider>
    </DarkModeProvider>
  );
};

export default App;
