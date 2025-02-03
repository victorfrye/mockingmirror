import Mirror from '@mockingmirror/components/Mirror';
import DarkModeProvider from '@mockingmirror/providers/DarkMode';
import ThemeProvider from '@mockingmirror/providers/Theme';

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
